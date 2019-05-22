using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Materials
{
    public partial class PopUpDel : Form
    {
        private MySqlConnection conn;
        private string codedb;

        public PopUpDel(string codedb, string text)
        {
            InitializeComponent();
            code.Text = codedb;
            label1.Text = text;
        }

        /***************************************************************************************************************************************************************
         * Pre : values of the textboxes are not empty + receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *                                                                  
         * Post : delete a piece into the database                                                                                                                    *
         * Raise : label pop if there is an error in the textbox value or if the database is not connected                                                             *
         ***************************************************************************************************************************************************************/
        private void yes_Click(object sender, EventArgs e)
        {
            //Connection to the databse
            SKGridPage sk = new SKGridPage();
            sk.SqlConnection();
            //Creation of the Sql command
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = string.Format("DELETE FROM piece WHERE code = '{0}'", codedb);

            try
            {
                //Open the database connection and execute the command
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception x)
            {
                //Raise the error
                Console.WriteLine(x.Message);
            }
            //Close the database connection
            conn.Close();

            this.Close();
        }

        private void no_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
