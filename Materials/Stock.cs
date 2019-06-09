/*-----------------------------------------------------------*/
/*                      Using tutorial :                     */
/*    https://www.youtube.com/watch?v=IcD9Jffstmw&t=184s     */
/*-----------------------------------------------------------*/

using System;
using System.Collections.Generic;
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
            Connect();
            connection.Close();
        }

        private void Connect()
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
         * Pre : receives a part as parameter                                      *
         * Post : returns true if the part is available, false otherwise          *
         ***************************************************************************/
        public bool IsAvailable (Piece piece)
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
                dimension = piece.GetDescription()["dimension"].ToString();
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
            Connect();
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
        /*When the storekeeper refills the stock*/
        public void RefillStock (Piece[] piece)
        {
            Connect();
            connection.Close();
        }

        /*--------------------------------------------------------------------------*/
        public void OrderPiece(Piece piece)
        {
            string code ="";
            int quantity = 0;
            int newQuantity = 0;
            try
            {
                string dimension1 = piece.GetDescription()["dim1"].ToString();
                string dimension2 = piece.GetDescription()["dim2"].ToString();
                if (piece is GlassDoor)
                {
                    try
                    {
                        code = SelectPiece2D(piece, dimension1, dimension2, "verre");
                    }
                    catch(KeyNotFoundException e)
                    {
                        //Console.WriteLine(piece.GetType());     
                        //Console.WriteLine(e.Source);
                    }
                }
                else
                {
                    try
                    {
                        code = SelectPiece2D(piece, dimension1, dimension2, piece.GetDescription()["color"].ToString());
                    }
                    catch (KeyNotFoundException e)
                    {
                        //Console.WriteLine(piece.GetType());
                        //Console.WriteLine(e.Source);
                    }
                }
            }
            catch (KeyNotFoundException) //an error is raised if dim1-dim2 are not in piece description => there is only one dim
            {

                try
                {
                    string dimension = piece.GetDescription()["dimension"].ToString();
                }
                catch(KeyNotFoundException)
                {
                    Console.WriteLine(piece.GetType());
                }
                try
                {
                    code = SelectPiece(piece, piece.GetDescription()["dimension"].ToString(), piece.GetDescription()["color"].ToString());
                }
                catch (KeyNotFoundException)
                {
                    code = code = SelectPiece(piece, piece.GetDescription()["dimension"].ToString(), "");
                }
            }
            Connect();
            this.command.CommandText = String.Format("SELECT * FROM piece WHERE code='{0}'", code);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                quantity = (int)reader["virtual_quantity"];
            }
            //NOTE : newQuantity CAN BE NEGATIVE
            newQuantity = quantity - 1;
            
            connection.Close();
            Connect();

            //NOTE : virtual quantity CAN BE NEGATIVE
            this.command.CommandText = String.Format("UPDATE piece SET virtual_quantity='{0}' WHERE code='{1}'",  newQuantity, code);
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
        public Dictionary<string, int>  MakeOrder(Cupboard cupboard)
        {
            
            /*Maximum number of arts in a cupboard is (7 boxes * 15 pieces) + 4 angles = 109*/
            Dictionary<string, int> quantities = new Dictionary<string, int>();
            for (int b = 0; b < cupboard.GetBloc().Length; b++)
            {
                Bloc bloc = cupboard.GetBloc()[b];
                for (int p = 0; p < bloc.GetPieces().Length; p++)
                {
                    Piece piece = bloc.GetPieces()[p];
                    if (piece != null)
                    {
                        string code = piece.GetDescription()["code"].ToString();
                        /*Piece is counted as ordered*/
                        OrderPiece(piece);
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
            }
            Angle[] angles = cupboard.GetAngles();
            if (angles.Length == 4)
            {
                quantities.Add(angles[0].GetDescription()["code"].ToString(), 4);
            }
            else
            {
                Console.WriteLine("There are not 4 angles in the cupboard, there must have been an issue with addAngles function.");
            }
            return quantities;
        }

        /*****************************************************************************
         * Pre :                                                                     *
         * Post :                                                                    *
         * Raise :                                                                   *
         *****************************************************************************/
        public string FindClient (string clientFirstName, string clientLastName)
        {
            string idClient = "";
            Connect();
            command.CommandText = String.Format("SELECT * FROM client WHERE lastname='{0}'", clientLastName);
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["firstname"].ToString() == clientFirstName)
                    {
                        idClient = reader["idclient"].ToString();
                        Console.WriteLine("Client found ! :)");
                    }
                }
            }
            catch (MySqlException)
            {
                idClient = "";
                Console.WriteLine("Fail in sql command (trying to find a client. Perhaps the client did not exist");
            }
            connection.Close();
            return idClient;
        }

        /*****************************************************************************
         * Pre :                                                                     *
         * Post :                                                                    *
         * Raise :                                                                   *
         *****************************************************************************/
        public void ConfirmOrder (string clientFirstName, string clientLastName, string adress, string zip, string phoneNumber, Cupboard cupboard)
        {
            /*First handles the order's identifiers (idcom and idclient)*/
            int idCom = 0;
            Connect();
            this.command.CommandText = "SELECT MAX(idcom) FROM client_piecescommand";
            try
            {
                string stepIDCom = command.ExecuteScalar().ToString();
                Console.WriteLine("Command ID is :");
                Console.WriteLine(stepIDCom);
                /* Command ID*/
                idCom = Int32.Parse(stepIDCom) + 1;
            }
            /*Means that there is no command yet in database*/
            catch (FormatException ex)
            {
                idCom = 1;
                Console.WriteLine("no command in the database yet.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            connection.Close();
            string idClient = FindClient(clientFirstName, clientLastName);
            /*If the client was not found in the database, that means he has to be created*/
            if (idClient =="")
            {
                Connect();
                command.CommandText = "SELECT MAX(idclient) FROM client";
                try
                {
                    idClient = ((int)command.ExecuteScalar() + 1).ToString();
                }
                catch(InvalidCastException)
                {
                    idClient = "1";
                    Console.WriteLine("No client in the database yet.");
                }
                connection.Close();
                Connect();
                command.CommandText = String.Format("INSERT INTO client(idclient, firstname, lastname, adress, zip, phonenumber) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", idClient, clientFirstName, clientLastName, adress, zip, phoneNumber);
                command.ExecuteNonQuery();
                connection.Close();
            }
            //Orders the parts
            Dictionary<string, int> pieces = MakeOrder(cupboard);
            Connect();
            command.CommandText = String.Format("INSERT INTO client_command(idcom, idclient, description) VALUES ('{0}', '{1}', '{2}');", idCom, idClient, cupboard.GetDescription().ToString());
            foreach (string code in pieces.Keys)
            {
                command.CommandText += String.Format("INSERT INTO client_piecescommand VALUES ('{0}', '{1}', '{2}');", idCom, code, pieces[code]);
                command.CommandText += String.Format("UPDATE piece SET real_quantity= real_quantity-{0} WHERE code='{1}';", pieces[code], code);
            }
            command.ExecuteNonQuery();
            connection.Close();
        }




        /*****************************************************************************
         * Pre : recieves a part, its main dimension and color                       *
         * Post : returns the unique code of the wanted part                         *
         * Raise : raises an error if the part has is a door or a panel              *
         *****************************************************************************/
        private string SelectPiece(Piece piece, string dimension, string color)
        {
            Connect();
            string code = "";
            int length = (int)piece.GetDescription()["length"];
            if ((piece is Door) || (piece is Panel))
            {
                throw new ArgumentException("The part cannot be a door nor a panel in this function");
            }
            this.command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", piece.GetDescription()["ref"].ToString());

            reader = command.ExecuteReader();
            /*Retrieves all information about the part, and puts them in the description dictionnary*/
            while (reader.Read())
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
                List<int> possibleHeights = ExistingDimension("height", "Cornieres");
                Console.WriteLine("No fitting part found.1D");
                Console.WriteLine(piece.GetDescription()["ref"].ToString());
                Console.WriteLine(piece.GetDescription()["color"].ToString());
                Console.WriteLine(piece.GetDescription()["length"].ToString());
                return null;
            }
            connection.Close();
            return code;
        }

        /*Same as SelectPiece() but with 2 main dimensions*/
        private string SelectPiece2D (Piece piece, string dimension1, string dimension2, string color)
        {
            Connect();
            string code = "";
            int length = (int)piece.GetDescription()["length"];
            int width = (int)piece.GetDescription()["width"];
            command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", piece.GetDescription()["ref"].ToString());
            reader = command.ExecuteReader();
            //Retrieves all information about the part, and puts them in the description dictionnary
            while (reader.Read()) 
            {
                if ((int)reader[dimension1] == length)
                {
                    if ((int)reader[dimension2] == width)
                    {
                        /*Part has no color specified*/
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
            }
            if (code == "")
            {
                Console.WriteLine(length);
                Console.WriteLine(width);
                Console.WriteLine("No fitting part found. 2D");
                Console.WriteLine(piece.GetDescription()["ref"].ToString());
                Console.WriteLine(piece.GetDescription()["color"].ToString());
                return null;
            }
            connection.Close();
            return code;
        }

        /*****************************************************************************
         * Pre : recieves a part as paramater                                        *
         * Post : - Returns the part's complete description.                        *
         *          Dictionnary contains the following keys : height, depth, width   *
         *          color, code, client price, supplier price  (there may be more    *
         *          than one) and n suppliers, the number of suppliers that offer    *
         *          the piece. The dimension(s) are present -or not- following the   *
         *          piece's type                                                     *
         * Uses the piece characteristics to find the piece in the DB                *
         *****************************************************************************/
        public Dictionary <string, Object> GetPieceDescription (Piece piece)
        {
            
            Dictionary<string, Object> description = new Dictionary<string, Object>();
            string color="";
            string code;
            int width = -1;
            int length = (int)piece.GetDescription()["length"];
            /*Manages angles' length*/
            if (piece is Angle) 
            {
                List<int> possibleHeights = ExistingDimension("height", "Cornieres");
                int prevCandidateHeight = length;
                bool firstIter = true;
                /*There is no angle with the right length => takes a bigger angle and cuts it afterwards*/
                if (!(possibleHeights.Contains(length)))
                {
                    foreach (int h in possibleHeights)
                    {
                        if ((h > length) && firstIter)
                        {
                            prevCandidateHeight = h;
                            firstIter = false;
                        }
                        /*The angle must be bigger, but as close as possible from the cupboard's height (the smallest value of lenght possible)*/
                        else if ((h > length) && (h <= prevCandidateHeight) && !firstIter)
                        {
                            prevCandidateHeight = h;
                        }
                    }
                    length = prevCandidateHeight;
                    /*Effectivly changes angle's length (but keep cupboard height identical)*/
                    piece.SetLength(prevCandidateHeight);
                }
            }
            try
            {
                width = (int)piece.GetDescription()["width"];
            }
            catch (KeyNotFoundException){}
            
            if ((piece is ClassicDoor) || (piece is Panel) || (piece is Angle))
            {
                color = piece.GetDescription()["color"].ToString();
                description.Add("color", color);
            }

            //Since the determining dimension in DB differs following the type of piece, we have to make a different criterea for each type
            try
            {
                string dimension1 = piece.GetDescription()["dim1"].ToString();
                string dimension2 = piece.GetDescription()["dim2"].ToString();

                if (piece is GlassDoor)
                {
                    code = SelectPiece2D (piece, dimension1, dimension2, "Verre");
                }
                else
                {
                    code = SelectPiece2D(piece, dimension1, dimension2, color);
                }
                description.Add("code", code);
                Connect();
                command.CommandText = String.Format("SELECT * FROM piece INNER JOIN prices ON piece.code=prices.code WHERE piece.code='{0}'", code);
                //Console.WriteLine(String.Format("code of this piece is {0}", code));
                reader = command.ExecuteReader();
                int i = 0;
                /*Retrieves all information about the part and puts them in the description dictionnary*/
                while (reader.Read())
                {
                    if (i==0)
                    {
                        /*Code is a unique identifier*/
                        description.Add("client price", (float)reader["client_price"]);
                    }
                    i++;
                    description.Add(String.Format("supplier price {0}", i), (float)reader["supplier_price"]);
                }
                description.Add("n suppliers", i);
                connection.Close();
            }
            /*An error is raised if dim1-dim2 are not in piece description => there is only one dimension*/
            catch (KeyNotFoundException)
            {
                string dimension = piece.GetDescription()["dim"].ToString();
                code = SelectPiece(piece, piece.GetDescription()["dim"].ToString(), color);
                //Console.WriteLine(String.Format("code of this piece is {0}", code));
                description.Add("code", code);
                Connect();
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
            //foreach (string key in description.Keys)
            //{
            //    Console.WriteLine(String.Format("value of {0} is : {1}",key.ToString(), description[key].ToString()));
            //}
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
         public List<int> ExistingDimension(string dim, string determiningPiece)
         {
         //   connect();
         //   command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", determiningPiece); //height of a box is given by cleat's length + 4 cm
         //   reader = command.ExecuteReader();
         //   int counter = 0;
         //   while (reader.Read())
         //   {
         //       counter++;
         //   }
         //   connection.Close();
         //   if (counter == 0)
         //   {
         //       Console.WriteLine(String.Format("No piece of that name found. Is this a correct name ? {0}", determiningPiece));
         //   }
            List<int> dimensions = new List<int>();
            List<int> references = new List<int>();
            Connect();
            command.CommandText = String.Format("SELECT * FROM piece WHERE ref='{0}'", determiningPiece);
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                if (!(references.Contains((int)reader[dim])))
                {
                    try
                    {
                        if ((dim == "height") && (determiningPiece != "Cornieres"))
                        {
                            dimensions.Add((int)reader[dim] + 4);
                        }
                        else
                        {
                            dimensions.Add((int)reader[dim]);
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
        public List<string> GetExistingcolors (Piece piece)
        {
            Connect();
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
            Connect();
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