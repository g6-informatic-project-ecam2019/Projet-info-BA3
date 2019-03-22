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
   
    class Stock 
    {
        private string connString;
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public Stock(string connString)
        {
            this.connString = connString; //"Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd="
            connection = new MySqlConnection(this.connString);
            command = connection.CreateCommand();
            //command.CommandText = "SELECT * FROM pieces WHERE ref='Porte'"; //this is a "select" command, replace tableName and number
            connect();
            connection.Close();
        }

        private void connect()
        {
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
            connect();
            command.CommandText = String.Format("SELECT * FROM pieces WHERE ref={0}", piece.GetDescription()["ref"]);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    if (((int)reader[piece.GetDescription()["dim1"].ToString()] == (int)piece.GetDescription()["length"]) && (((int)reader[piece.GetDescription()["dim2"].ToString()] == (int)piece.GetDescription()["width"])))
                    {
                        try
                        {
                            if (reader["color"].ToString() == piece.GetDescription()["color"].ToString())
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
                            else if ((reader["color"].ToString() == "verre") && (piece is GlassDoor)) //ref is already checked as "porte" (door) so if color is "verre", we are sure that it is a glass door
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
                        catch (KeyNotFoundException) //no color specified in description
                        {
                            if ((reader["color"].ToString() == "verre") && (piece is GlassDoor))
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
                catch (KeyNotFoundException) //one dimension piece
                {
                    if ((int) reader[piece.GetDescription()["dim"].ToString()] == (int) piece.GetDescription()["length"])
                    {
                        try
                        {
                            if (reader["color"].ToString() == piece.GetDescription()["color"].ToString())
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
                        catch (KeyNotFoundException) //no color specified in description
                        {
                            if (reader["color"].ToString() == piece.GetDescription()["color"].ToString())
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
            }
            connection.Close();
            return false;
        }

        /*--------------------------------------------------------------------------*/
        public void orderPieces (Piece[] piece) //when the storekeeper refills the stock
        {
            connect();

            connection.Close();
        }

        /*--------------------------------------------------------------------------*/
        public void orderPiece (Piece piece)
        {
            connect();

            connection.Close();
        }

        /*****************************************************************************
         * Pre :                                                                     *
         * Post :                                                                    *
         * Raise :                                                                   *
         *****************************************************************************/
        public void makeOrder (Cupboard cupboard)
        {
            connect();
            for (int b = 0; b < cupboard.GetBloc().Length; b++)
            {
                Bloc bloc = cupboard.GetBloc()[b];
                for (int p = 0; p < bloc.GetPieces().Length; p++)
                {
                    Piece piece = bloc.GetPieces()[p]; 
                    orderPiece(piece);                //piece is counted as ordered 

                }

            }
            connection.Close();
        }

        /************************************************************************************************* 
         * Pre : - recieve a dictionnary containing a part of description and other necessary parameters *
         *       - the piece is not a Panel, nor a door                                                  *
         * Post : edit the dictionnary with informations found in the DB                                 *
         * Raise : - if the piece is a Panel or a Door                                                   *
         *         - 
         *************************************************************************************************/
        private void retrieveInfos (Dictionary <string, Object> dic, Piece piece, string dimension, string color)  //function only used internally, by getDescription() => private function
        {                                                                                                                       //dimension is the determining dimension in the DB (height depth or width)

            connect();
            string code = "";
            int length = (int) piece.GetDescription()["length"];
            dic.Add(dimension, length);
            if ((piece is Door) || (piece is Panel))
            {
                throw new System.ArgumentException("The piece cannot be a door nor a panel in this function");
            }
            this.command.CommandText = String.Format("SELECT * FROM pieces WHERE ref='{0}'", piece.GetDescription()["ref"].ToString());

            reader = command.ExecuteReader();
            while (reader.Read()) //retrieve all informations about the piece, and put them in the description dictionnary
            {
                if ((int)reader[dimension] == length)
                {
                    if  (color == "")
                    {
                        code = reader["code"].ToString();
                        dic.Add("code", code);
                        dic.Add("client price", (int)reader["client_price"]);
                    }
                    else if (reader["color"].ToString() == color)
                    {
                        code = reader["code"].ToString();
                        dic.Add("code", code);
                        dic.Add("client price", (int)reader["client_price"]);
                    }
                }
            }
            connection.Close();
            connect();
            this.command.CommandText = String.Format("SELECT * FROM prices WHERE code='{0}'", code);
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                i++;
                dic.Add(String.Format("supplier price {0}", i), (int)reader["supplier_price"]); //add different prices from each suppliers    
            }
            dic.Add("n suppliers", i);
            connection.Close();
        }

        /************************************************************************************************* 
         *                Same as retrieve info, but with 2 determining dimensions                       *
         *************************************************************************************************/
        private void retrieveInfos2D(Dictionary<string, Object> dic, Piece piece, string dimension1, string dimension2, string color) //dimension is the determining dimension in the DB (height depth or width)
        {

            connect();
            string code = "";
            int length = (int)piece.GetDescription()["length"];
            dic.Add(dimension1, length);
            int width = (int)piece.GetDescription()["width"];
            dic.Add(dimension2, width);
            command.CommandText = String.Format("SELECT * FROM pieces WHERE ref='{0}'", piece.GetDescription()["ref"].ToString());
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
                            dic.Add("code", code);
                            dic.Add("client price", (float)reader["client_price"]);
                        }
                        else if (reader["color"].ToString() == color)
                        {
                            code = reader["code"].ToString();
                            dic.Add("code", code);
                            dic.Add("client price", (float)reader["client_price"]);
                        }
                    }  
                }
            }
            connection.Close();
            connect();
            this.command.CommandText = String.Format("SELECT * FROM prices WHERE code='{0}'", code);
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                i++;
                dic.Add(String.Format("supplier price {0}", i), (float)reader["supplier_price"]); //add different prices from each suppliers    
            }
            dic.Add("n suppliers", i);
            connection.Close();
        }

        /*****************************************************************************
         * Pre : recieves a Piece as paramater                                        *
         * Post : - Returns the piece's complete description.                        *
         *          Dictionnary contains the following keys : heigth, depth, width   *
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
            if ((piece is ClassicDoor) || (piece is Panel) || (piece is Angle))
            {
                color = (string)piece.GetDescription()["color"];
                description.Add("color", color);
            }

            //since the determining dimension in DB differs following the type of piece, we have to make a different criterea for each type
            try
            {
                if (piece is GlassDoor)
                {
                    retrieveInfos2D(description, piece, piece.GetDescription()["dim1"].ToString(), piece.GetDescription()["dim2"].ToString(), "verre");
                }
                else
                {
                    retrieveInfos2D(description, piece, piece.GetDescription()["dim1"].ToString(), piece.GetDescription()["dim2"].ToString(), color);
                }
                
            }
            catch (KeyNotFoundException) //an error is raised if dim1-dim2 are not in piece description => there is only one dim
            {
                retrieveInfos(description, piece, piece.GetDescription()["dim"].ToString(), color);
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
            command.CommandText = String.Format("SELECT * FROM pieces WHERE ref='{0}'", determiningPiece); //height of a box is given by cleat's length + 4 cm
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
            connect();
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                try
                {
                    if (dim == "height")
                    {
                        dimensions[i] = (int)reader[dim] + 4;
                    }
                    else
                    {
                        dimensions[i] = (int)reader[dim];
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Uncorrect specified dimension");
                }
                i++;
            }
            connection.Close();
            return dimensions; 
         }
    }
}