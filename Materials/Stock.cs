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
            this.connString = connString; //server adress, port and password
            connection = new MySqlConnection(this.connString);
            command = connection.CreateCommand();
            command.CommandText = "Select text from tableName where id=number"; //this is a "select" command, replace tableName and number
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            reader = command.ExecuteReader();
        }

        public bool isAvailable (Piece piece)
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return false;
        }

        public void orderPieces (Piece[] piece) //when the storekeeper refills the stock
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            connection.Close();
        }

        public void removePiece (Piece piece)
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
        }

        public string getPieceDescription (Piece piece)
        {
            /* Pre : recieve a Piece as paramater
             * Post : Uses the piece dimensions to select the correct piece from DB and return its complete description
             * 
             */
            string description="";
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            while (reader.Read())
            {
                description += reader["text"].ToString();
            }
            connection.Close();
            return description;
        }
    }
}
