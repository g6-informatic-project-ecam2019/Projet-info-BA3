using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
namespace Materials
{
    /*Using tutorial : 
     * https://www.youtube.com/watch?v=IcD9Jffstmw&t=184s
     */
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

        /*--------------------------------------------------------------------------*/
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

        /*----------------------------------------------------------------------------*/
        private void retrieveInfos (Dictionary <string, Object> dic, Piece piece, string dimension, int length, string color, string code) //dimension is the determining dimension in the DB (height depth or width)
        {
            connect();
            this.command.CommandText = String.Format("SELECT * FROM pieces WHERE ref='{0}'", translateInfos(piece));

            reader = command.ExecuteReader();
            while (reader.Read()) //retrieve all informations about the piece, and put them in the description dictionnary
            {
                if ((int)reader[dimension] == length)
                {
                    if (reader["color"].ToString() == color)
                    {
                        code = reader["code"].ToString();
                        dic.Add("code", code);
                        dic.Add("client price", (int)reader["client_price"]);
                    }
                }
            }
            connection.Close();
            connect();
            this.command.CommandText = String.Format("SELECT * FROM prices WHERE code={0}", code);
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

        /*----------------------------------------------------------------------------*/
        public Dictionary <string, Object> getPieceDescription (Piece piece)
        {
            /* Pre : recieve a Piece as paramater
             * Post : Returns the piece's complete description
             * Uses the piece characteristics to find the piece in the DB 
             */
            Dictionary<string, Object> description = new Dictionary<string, Object>();
            int length = (int)piece.GetDescription()["length"];
            int width;
            string code="";
            string color="";
            if ((piece is Panel)||(piece is Door))
            {
                width = (int)piece.GetDescription()["width"];
                description.Add("width", width);
            }
            if ((piece is ClassicDoor) || (piece is Panel) || (piece is Angle))
            {
                color = (string)piece.GetDescription()["color"];
                description.Add("color", color);
            }

            //since the determining dimension in DB is different following the type of piece, we have to make a different criterea for each type
            if (piece is Angle)
            {
                retrieveInfos(description, piece, "height", length, color, code);
            }

            if (piece is Breadth)
            {
                if ((piece.GetDescription()["pos"].ToString() == "Av") || (piece.GetDescription()["pos"].ToString() == "Ar"))
                {
                    retrieveInfos(description, piece, "width", length, color, code);
                }
                else
                {
                    retrieveInfos(description, piece, "depth", length, color, code);
                }
                
            }
            connection.Close();
            return description;
        }
    }
}
