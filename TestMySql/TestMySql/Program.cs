using System;
using MySql.Data.MySqlClient;

namespace TestMySql
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = "Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT height FROM pieces WHERE ref='Porte'"; //Sql Command
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["height"].ToString()); //Use the parameter of the sql command
            }
            Console.ReadLine();
        }
    }
}
