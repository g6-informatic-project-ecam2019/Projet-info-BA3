﻿using System;
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
            this.connString = connString; //connection data = server adress, port and password
            connection = new MySqlConnection(this.connString);
            command = connection.CreateCommand();
            //command.CommandText = "Select text from tableName where id=number"; //this is a "select" command, replace tableName and number
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

        public bool isAvailable (Piece piece)
        {
            connect();
            connection.Close();
            return false;
        }

        public void orderPieces (Piece[] piece) //when the storekeeper refills the stock
        {
            connect();

            connection.Close();
        }

        public void removePiece (Piece piece)
        {
            connect();
            connection.Close();
        }

        public Dictionary <string, Object> getPieceDescription (Piece piece)
        {
            /* Pre : recieve a Piece as paramater
             * Post : Returns the piece's complete description
             * Uses the piece characteristics to find the piece in the DB 
             */
            Dictionary<string, Object> description = new Dictionary<string, Object>();
            int length = (int)piece.GetDescription()["length"];
            int width;
            string color;
            if ((piece.GetType()==Panel)||(piece.GetType()==Door))
                {
                    width = (int)piece.GetDescription()["width"];
                }
            if ((piece.GetType() == ClassicDoor) || (piece.GetType() == Panel))
                {
                    color = (string)piece.GetDescription()["color"];
                }
            connect();

            for (int i = 0; i < 400; i++) //A CHANGER =nbre de pièces différentes
                {   
                    if ((piece.GetType() != Panel) || (piece.GetType() != Door))
                    {
                        this.command.CommandText = String.Format("Select dimension from Piece where ref={}", piece.GetType());
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if ((int)reader["dimension"] == length)
                            {
                                //retrieve all informations about the piece, and put them in the description dictionnary
                            }
                        }
                    }
                    else if ((piece.GetType() == ClassicDoor) || (piece.GetType() == Panel))
                    {
                        //find all pieces with same color and width
                    }
                    else
                    {
                        //find all pieces with same width
                    }

                }

            connection.Close();
            return description;
        }
    }
}
