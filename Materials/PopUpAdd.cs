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
    public partial class PopUpAdd : Form
    {
        private MySqlConnection conn;

        public PopUpAdd()
        {
            InitializeComponent();
        }

        /***************************************************************************************************************************************************************
         * Pre : values of the textboxes are not empty + receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *                                                                  
         * Post : add a new part into the database                                                                                                                    *
         * Raise : label pop if there is an error in the textbox value or if the database is not connected                                                             *
         ***************************************************************************************************************************************************************/
        private void Apply_Click(object sender, EventArgs e)
        {
            //Connection to the databse
            SKGridPage sk = new SKGridPage();
            sk.SqlConnection();
            
            try
            {
                //Creation of the Sql command
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = string.Format("INSERT INTO `part`(`code`, `ref`, `dimension`, `height`, `depth`, `width`, `color`, `min_stock`, `real_quantity`, `virtual_quantity`, `client_price`, `box_number`) VALUES('{0}','{1}','{2}',{3},{4},{5},'{6}',{7},{8},{9},{10},{11})", code.Text, reference.Text, dimension.Text, height.Text, depth.Text, width.Text, color.Text, min_stock.Text, quantity.Text, quantity.Text, price.Text.Replace(",", "."), box_number.Text);
                //Open the database connection and execute the command
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                this.Close();
            }
            catch (Exception)
            {
                //Raise the error
                MessageBox.Show("Incorrect values, please do enter correct values", "Error", MessageBoxButtons.OK);
            }
        }
        
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
