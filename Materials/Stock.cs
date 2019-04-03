/*-----------------------------------------------------------*/
/*                      Using tutorial :                     */ 
/*    https://www.youtube.com/watch?v=IcD9Jffstmw&t=184s     */
/*-----------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
namespace Materials
{
   
    public class Stock 
    {
        private string connString;
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public Stock(string connString)
        {
            this.connString = connString; //"Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd="
            //connection = new MySqlConnection(this.connString);
            //command = connection.CreateCommand();
            //command.CommandText = "SELECT * FROM pieces WHERE ref='Porte'"; //this is a "select" command, replace tableName and number
            connect();
            connection.Close();
        }

        private void connect()
        {
            connection = new MySqlConnection(this.connString);
            command = connection.CreateCommand();
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /***************************************************************************
         * Pre : recieve a piece as parameter                                      *
         * Post : returns true if the piece is available, false otherwise          *
         ***************************************************************************/
        public bool isAvailable (Piece piece)
        {
            string dimension1 = "";
            string dimension2 = "";
            string dimension = "";
            string color = "";
            int length = -1;
            int width = -1;
            try
            {
                dimension1 = piece.GetDescription()["dim1"].ToString();
                dimension2 = piece.GetDescription()["dim2"].ToString();
                length = (int)piece.GetDescription()["length"];
                width = (int)piece.GetDescription()["width"];
            }
            catch (KeyNotFoundException)
            {
                dimension = piece.GetDescription()["dim"].ToString();
                length = (int)piece.GetDescription()["length"];
            }
            try
            {
                color = piece.GetDescription()["color"].ToString();
            }
            catch (KeyNotFoundException)
            {
                if (piece is GlassDoor)
                {
                    color = "verre";
                }
            }
            connect();
            command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", piece.GetDescription()["ref"]);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (dimension1 != "")
                {
                    if (((int)reader[dimension1] == length) && ((int)reader[dimension2] == width))
                    {
                        if (color != "")
                        {
                            if (reader["color"].ToString() == color)
                            {
                                if ((int)reader["real_quantity"] != 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if ((int)reader["real_quantity"] != 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    if ((int)reader[dimension] == length)
                    {
                        if (color != "")
                        {
                            if(reader["color"].ToString() == color)
                            {
                                if ((int)reader["real_quantity"] != 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if ((int)reader["real_quantity"] != 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }   
            }
            connection.Close();
            return false;
        }

        /*--------------------------------------------------------------------------*/
        public void refillStock (Piece[] piece) //when the storekeeper refills the stock
        {
            connect();

            connection.Close();
        }

        /*--------------------------------------------------------------------------*/
        public void orderPiece(Piece piece)
        {
            string code;
            int quantity = 0;
            int newQuantity = 0;
            try
            {
                string dimension1 = piece.GetDescription()["dim1"].ToString();
                string dimension2 = piece.GetDescription()["dim2"].ToString();
                if (piece is GlassDoor)
                {
                    code = selectPiece2D(piece, dimension1, dimension2, "verre");
                }
                else
                {
                    code = selectPiece2D(piece, dimension1, dimension2, piece.GetDescription()["color"].ToString());
                }
            }
            catch (KeyNotFoundException) //an error is raised if dim1-dim2 are not in piece description => there is only one dim
            {
                string dimension = piece.GetDescription()["dim"].ToString();
                code = selectPiece(piece, piece.GetDescription()["dim"].ToString(), piece.GetDescription()["color"].ToString());
            }
            connect();
            this.command.CommandText = String.Format("SELECT * FROM piece WHERE code='{0}'", code);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                quantity = (int)reader["virual_quantity"];
            }
            newQuantity = quantity - 1; //NOTE : newQuantity CAN BE NEGATIVE
            
            connection.Close();
            connect();
            this.command.CommandText = String.Format("UPDATE piece SET virtual_quantity='{0}' WHERE code='{1}'",  newQuantity, code); //virtual quantity can be negative
            int l = command.ExecuteNonQuery(); 
            if (l != 1) 
            {
                Console.WriteLine(String.Format("WARNING : database was not edited correctly : {0} lines were modified instead of one.", l));
            }
            connection.Close();
            
        }

        /*****************************************************************************
         * Pre :                                                                     *
         * Post :                                                                    *
         * Raise :                                                                   *
         *****************************************************************************/
        public Dictionary<string, int>  makeOrder(Cupboard cupboard)
        {
            
            //maximum number of pieces in a cupboard is (7 boxes * 15 pieces) + 4 angles = 109
            Dictionary<string, int> quantities = new Dictionary<string, int>();
            for (int b = 0; b < cupboard.GetBloc().Length; b++)
            {
                Bloc bloc = cupboard.GetBloc()[b];
                for (int p = 0; p < bloc.GetPieces().Length; p++)
                {
                    Piece piece = bloc.GetPieces()[p];
                    string code = piece.GetDescription()["code"].ToString();
                    orderPiece(piece);                //piece is counted as ordered
                    if (quantities.ContainsKey(code))
                    {
                        quantities[code] += 1;
                    }
                    else
                    {
                        quantities.Add(code, 1);
                    }  
                } 
            }
            return quantities;
            

        }

        /*****************************************************************************
         * Pre :                                                                     *
         * Post :                                                                    *
         * Raise :                                                                   *
         *****************************************************************************/
        public string findClient (string clientFirstName, string clientLastName)
        {
            string idClient = "";
            connect();
            command.CommandText = String.Format("SELECT * FROM client WHERE lastname={0}", clientLastName);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["firstname"].ToString() == clientFirstName)
                {
                    idClient = reader["idclient"].ToString();
                }
            }
            connection.Close();
            return idClient;
        }

        /*****************************************************************************
         * Pre :                                                                     *
         * Post :                                                                    *
         * Raise :                                                                   *
         *****************************************************************************/
        public void confirmOrder (string clientFirstName, string clientLastName, string adress, string zip, string phoneNumber, Cupboard cupboard)
        {
            connect();
            this.command.CommandText = "SELECT MAX(idcom) FROM client_piecescommand";
            int idCom = (int)command.ExecuteScalar() + 1;   //id of the command
            connection.Close();
            string idClient = findClient(clientFirstName, clientLastName);
            if (idClient =="")      //if the client was not found in the db, that means he has to be created
            {
                command.CommandText = "SELECT MAX(idclient) FROM client";
                connect();
                idClient = ((int)command.ExecuteScalar() + 1).ToString();
                connection.Close();
                command.CommandText = String.Format("INSERT INTO client(idclient, firstname, lastname, adress, zip, phonenumber) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", idClient, clientFirstName, clientLastName, adress, zip, phoneNumber);
                connect();
                command.ExecuteNonQuery();
                connection.Close();
            }
            command.CommandText = String.Format("INSERT INTO client_command(idcom, idclient, description) VALUES ('{0}', '{1}', '{2}');", idCom, idClient, cupboard.GetDescription());

            Dictionary<string, int> pieces = makeOrder(cupboard);
            int c = 1;
            foreach (string code in pieces.Keys)
            {
                if (c < pieces.Count)
                {
                    command.CommandText += String.Format("INSERT INTO client_piecescommand VALUES ('{0}', '{1}', '{2}');", idCom, code, pieces[code]);
                }
                else
                {
                    command.CommandText += String.Format("INSERT INTO client_piecescommand VALUES ('{0}', '{1}', '{2}')", idCom, code, pieces[code]); //no ; if it is the last piece => the end of the querry
                }
            }
            connect();
            command.ExecuteNonQuery();
            connection.Close();
        }




        /*****************************************************************************
         * Pre : recieve a piece, its main dimension and color                       *
         * Post : returns the unique code of the wanted piece                        *
         * Raise : raises an error if the piece has is a door or a panel             *
         *****************************************************************************/
        private string selectPiece(Piece piece, string dimension, string color)
        {
            connect();
            string code = "";
            int length = (int)piece.GetDescription()["length"];
            if ((piece is Door) || (piece is Panel))
            {
                throw new System.ArgumentException("The piece cannot be a door nor a panel in this function");
            }
            this.command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", piece.GetDescription()["ref"].ToString());

            reader = command.ExecuteReader();
            while (reader.Read()) //retrieve all informations about the piece, and put them in the description dictionnary
            {
                if ((int)reader[dimension] == length)
                {
                    if (color == "")
                    {
                        code = reader["code"].ToString();
                    }
                    else if (reader["color"].ToString() == color)
                    {
                        code = reader["code"].ToString();
                    }
                }
            }
            if (code == "")
            {
                Console.WriteLine("No fitting piece found.");
                Console.WriteLine(piece.GetDescription()["ref"].ToString());
                Console.WriteLine(piece.GetDescription()["color"].ToString());
                Console.WriteLine(piece.GetDescription()["length"].ToString());
                Console.WriteLine(piece.GetDescription()["width"].ToString());
                return null;
            }
            connection.Close();
            return code;
        }

        /*same as selectPiece(), but with 2 main dimensions*/
        private string selectPiece2D (Piece piece, string dimension1, string dimension2, string color)
        {
            connect();
            string code = "";
            int length = (int)piece.GetDescription()["length"];
            int width = (int)piece.GetDescription()["width"];
            command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", piece.GetDescription()["ref"].ToString());
            reader = command.ExecuteReader();
            while (reader.Read()) //retrieve all informations about the piece, and put them in the description dictionnary
            {
                if ((int)reader[dimension1] == length)
                {
                    if ((int)reader[dimension2] == width)
                    {
                        if (color == "") //piece has no color specified
                        {
                            code = reader["code"].ToString();
                        }
                        else if (reader["color"].ToString() == color)
                        {
                            code = reader["code"].ToString();
                        }
                    }
                }
            }
            if (code == "")
            {
                Console.WriteLine("No fitting piece found.");
                Console.WriteLine(piece.GetDescription()["ref"].ToString());
                Console.WriteLine(piece.GetDescription()["color"].ToString());
                Console.WriteLine(piece.GetDescription()["length"].ToString());
                Console.WriteLine(piece.GetDescription()["width"].ToString());
                return null;
            }
            connection.Close();
            return code;
        }

        /*****************************************************************************
         * Pre : recieves a Piece as paramater                                        *
         * Post : - Returns the piece's complete description.                        *
         *          Dictionnary contains the following keys : height, depth, width   *
         *          color, code, client price, supplier price  (there may be more    *
         *          than one) and n suppliers, the number of suppliers that offer    *
         *          the piece. The dimension(s) are present -or not- following the   *
         *          piece's type                                                     *
         * Uses the piece characteristics to find the piece in the DB                *
         *****************************************************************************/
        public Dictionary <string, Object> getPieceDescription (Piece piece)
        {
            
            Dictionary<string, Object> description = new Dictionary<string, Object>();
            string color="";
            string code;
            int width = -1;
            int length = (int)piece.GetDescription()["length"];
            try
            {
                width = (int)piece.GetDescription()["width"];
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("");
            }
            
            if ((piece is ClassicDoor) || (piece is Panel) || (piece is Angle))
            {
                color = piece.GetDescription()["color"].ToString();
                description.Add("color", color);
            }

            //since the determining dimension in DB differs following the type of piece, we have to make a different criterea for each type
            try
            {
                string dimension1 = piece.GetDescription()["dim1"].ToString();
                string dimension2 = piece.GetDescription()["dim2"].ToString();

                if (piece is GlassDoor)
                {
                    code = selectPiece2D (piece, dimension1, dimension2, "Verre");
                }
                else
                {
                    code = selectPiece2D(piece, dimension1, dimension2, color);
                }
                description.Add("code", code);
                //description.Add(dimension1, length);
                if (width >0)
                {
                    //description.Add(dimension2, width);
                }
                //description.Add(dimension2, width);
                connect();
                command.CommandText = String.Format("SELECT * FROM piece INNER JOIN prices ON piece.code=prices.code WHERE piece.code='{0}'", code);
                //Console.WriteLine(String.Format("code of this piece is {0}", code));
                reader = command.ExecuteReader();
                int i = 0;
                while (reader.Read()) //retrieve all informations about the piece, and put them in the description dictionnary
                {
                    if (i==0)
                    {
                        description.Add("client price", (float)reader["client_price"]); //code is a unique identifier
                    }
                    i++;
                    description.Add(String.Format("supplier price {0}", i), (float)reader["supplier_price"]);
                }
                description.Add("n suppliers", i);
                connection.Close();
            }
            catch (KeyNotFoundException) //an error is raised if dim1-dim2 are not in piece description => there is only one dim
            {
                string dimension = piece.GetDescription()["dim"].ToString();
                code = selectPiece(piece, piece.GetDescription()["dim"].ToString(), color);
                //Console.WriteLine(String.Format("code of this piece is {0}", code));
                description.Add("code", code);
                connect();
                this.command.CommandText = String.Format("SELECT * FROM piece INNER JOIN prices ON piece.code=prices.code WHERE piece.code='{0}'", code);
                reader = command.ExecuteReader();
                int i = 0;
                while (reader.Read()) //retrieve all informations about the piece, and put them in the description dictionnary
                {
                    if (i == 0)
                    {
                        description.Add("client price", (float)reader["client_price"]); //code is a unique identifier
                    }
                    i++;
                    description.Add(String.Format("supplier price {0}", i), (float)reader["supplier_price"]);
                }
                description.Add("n suppliers", i);
                connection.Close();
            }
            foreach (string key in description.Keys)
            {
                Console.WriteLine(String.Format("value of {0} is : {1}",key.ToString(), description[key].ToString()));
            }
            return description;
        }

        /************************************************************************
         * Pre : recieve the dimension (string) to list and the piece (string)  *
         *       that sets it                                                   *
         * Post : returns available heights for boxes                           *
         * Raise : uncorrect dimension name will raise an error                 *
         *  Function will not raise any error if determiningPiece is not correct*
         *  but a log will appear                                               *
         ************************************************************************/
         public int[] existingDimension(string dim, string determiningPiece)
         {
            connect();
            command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", determiningPiece); //height of a box is given by cleat's length + 4 cm
            reader = command.ExecuteReader();
            int counter = 0;
            while (reader.Read())
            {
                counter++;
            }
            connection.Close();
            if (counter == 0)
            {
                Console.WriteLine(String.Format("No piece of that name found. Is this a correct name ? {0}", determiningPiece));
            }
            int[] dimensions = new int[counter];
            List<int> references = new List<int>();
            connect();
            command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", determiningPiece);
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                if (!(references.Contains((int)reader[dim])))
                {
                    try
                    {
                        if (dim == "height")
                        {
                            dimensions[i] = (int)reader[dim];//+4
                        }
                        else
                        {
                            dimensions[i] = (int)reader[dim];
                        }
                    }
                    catch (KeyNotFoundException)
                    {
                        Console.WriteLine("Uncorrect specified dimension");
                    }
                    i++;
                }
                references.Add((int)reader[dim]);
            }
            connection.Close();
            return dimensions; 
         }

        /************************************************************************
         * Pre : recieve the piece as a parameter                               *
         * Post : returns all available colors for that piece                   *
         * Raise :                                                              *
         *  Function should be used once for each type of piece ; once for panel*
         *  color, once for door color *
         ************************************************************************/
        public List<string> getExistingcolors (Piece piece)
         {
            connect();
            string reference = piece.GetDescription()["ref"].ToString();
            command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", reference); 
            reader = command.ExecuteReader();
            int counter = 0;
            while (reader.Read())
            {
                counter++;
            }
            connection.Close();
            if (counter == 0)
            {
                Console.WriteLine(String.Format("No piece of that name found. Is this a correct name ? {0}", reference));
            }
            List<string> colors = new List<string>();
            connect();
            command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", reference);
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                if (!(colors.Contains(reader["color"].ToString())))
                {
                    colors.Add(reader["color"].ToString()); //each color can appear only once in the list
                }
                
            }
            connection.Close();
            return colors;
        }
    }
}