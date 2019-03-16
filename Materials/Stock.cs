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

        /*--------------------------------------------------------------------------*/
        public bool isAvailable (Piece piece)
        {
            connect();
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
        public void removePiece (Piece piece)
        {
            connect();
            connection.Close();
        }

        /************************************************************************************************* 
         * Pre :  recieve a Piece as parameter                                                           *
         * Post : returns the correct name, that matches the field in the database                       *
         *************************************************************************************************/
        private string translateInfos (Piece piece)
        {
            string infos = "";
            if (piece is Panel)
            {
                if (piece.GetDescription()["pos"]=="GD")
                {
                    infos = "Panneau GD";
                }
                else if (piece.GetDescription()["pos"]=="HB")
                {
                    infos = "Panneau HB";
                }
                else
                {
                    infos = "Panneau Ar";
                }
            }
            else if (piece is Breadth)
            {
                if (piece.GetDescription()["pos"]=="Av")
                {
                    infos = "Traverse Av";
                }
                else if (piece.GetDescription()["pos"] == "Ar")
                {
                    infos ="Traverse Ar";
                }
                else 
                {
                    infos = "Traverse GD";
                }
            }
            else if (piece is Cleat)
            {
                infos = "Tasseau";
            }
            else if (piece is Angle)
            {
                infos = "Cornieres";
            }
            else if (piece is Door)
            {
                infos = "Porte";
            }
            return infos;
        }
        

        /************************************************************************************************* 
         * Pre : - recieve a dictionnary containing a part of description and other necessary parameters *
         *       - the piece is not a Panel, nor a door                                                  *
         * Post : edit the dictionnary with informations found in the DB                                 *
         *************************************************************************************************/
        private void retrieveInfos (Dictionary <string, Object> dic, Piece piece, string dimension, string color, string code)  //function only used internally, by getDescription() => private function
        {                                                                                                                       //dimension is the determining dimension in the DB (height depth or width)

            connect();
            int length = (int)piece.GetDescription()["length"];
            dic.Add(dimension, length);
            if ((piece is Door) || (piece is Panel))
            {
                throw new System.ArgumentException("The piece cannot be a door nor a panel in this function");
            }
            this.command.CommandText = String.Format("SELECT * FROM pieces WHERE ref='{0}'", translateInfos(piece));

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
        private void retrieveInfos2D(Dictionary<string, Object> dic, Piece piece, string dimension1, string dimension2, string color, string code) //dimension is the determining dimension in the DB (height depth or width)
        {

            connect();
            int length = (int)piece.GetDescription()["length"];
            dic.Add(dimension1, length);
            int width = (int)piece.GetDescription()["width"];
            dic.Add(dimension2, width);
            command.CommandText = String.Format("SELECT * FROM pieces WHERE ref='{0}'", translateInfos(piece));
            reader = command.ExecuteReader();
            while (reader.Read()) //retrieve all informations about the piece, and put them in the description dictionnary
            {
                if ((int)reader[dimension1] == length)
                {
                    if ((int)reader[dimension2] == width)
                    {
                        if (color == "")
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
         * Pre : recieve a Piece as paramater                                        *
         * Post : - Returns the piece's complete description.                        *
         *          Dictionnary contains the following keys : heigth, depth, width   *
         *          color, code, client price, supplier price  (there may be more    *
         *          than one) and n suppliers, the number of suppliers that offer    *
         *          the piece. The dimension(s) are present or not following the     *
         *          piece's type                                                     *
         * Uses the piece characteristics to find the piece in the DB                *
         *****************************************************************************/
        public Dictionary <string, Object> getPieceDescription (Piece piece)
        {
            
            Dictionary<string, Object> description = new Dictionary<string, Object>();
            string code="";
            string color="";
            if ((piece is ClassicDoor) || (piece is Panel) || (piece is Angle))
            {
                color = (string)piece.GetDescription()["color"];
                description.Add("color", color);
            }

            //since the determining dimension in DB is different following the type of piece, we have to make a different criterea for each type
            if (piece is Angle) //criteria is heigth
            {
                retrieveInfos(description, piece, "height", color, code);
            }

            else if (piece is Breadth)
            {
                if ((piece.GetDescription()["pos"].ToString() == "Av") || (piece.GetDescription()["pos"].ToString() == "Ar"))
                {
                    retrieveInfos(description, piece, "width", color, code);
                }
                else
                {
                    retrieveInfos(description, piece, "depth", color, code);
                }
            }
            else if (piece is Cleat)
            {
                retrieveInfos(description, piece, "heigth", color, code);
            }
            else if (piece is Panel) 
            {
                if (piece.GetDescription()["pos"].ToString() == "GD") //criteria is heigth and depth
                {
                    retrieveInfos2D(description, piece, "heigth", "depth", color, code);
                }
                else if (piece.GetDescription()["pos"].ToString() == "HB")
                {
                    retrieveInfos2D(description, piece, "depth", "width", color, code);
                }
                else
                {
                    retrieveInfos2D(description, piece, "heigth", "width", color, code);
                }
            }
            else if (piece is Door)
            {
                if (piece is GlassDoor)
                {
                    retrieveInfos2D(description, piece, "heigth", "width", "Verre", code); //in the DB, glass door are marked as glass colored
                }
                else
                {
                    retrieveInfos2D(description, piece, "heigth", "width", color, code);
                }
            }

            return description;
        }

        /************************************************************************
         * Pre : 
         * Post : returns existing dimensions  
         */
         public int[] existingHeights()
         {
            
            connect();
            command.CommandText = String.Format("SELECT * FROM pieces WHERE ref='Tasseau'"); //height of a box is given by cleat's length + 4 cm
            reader = command.ExecuteReader();
            int counter = 0;
            while (reader.Read())
            {
                counter++;
            }
            connection.Close();
            int[] heights = new int[counter];
            connect();
            reader = command.ExecuteReader();
            counter = 0;
            while (reader.Read())
            {
                heights[counter] = (int) reader["height"];
                counter++;
            }
            return heights;
         }
    }
}