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
        public PopUpAdd()
        {
            InitializeComponent();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            string connString = "Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = string.Format("INSERT INTO `piece`(`code`, `ref`, `dimension`, `height`, `depth`, `width`, `color`, `min_stock`, `real_quantity`, `virtual_quantity`, `client_price`, `box_number`) VALUES('{0}','{1}','{2}',{3},{4},{5},'{6}',{7},{8},{9},{10},{11})", code.Text, reference.Text, dimension.Text, height.Text, depth.Text, width.Text, color.Text, min_stock.Text, quantity.Text, quantity.Text, price.Text.Replace(",", "."), box_number.Text);
            Console.WriteLine(command.CommandText);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            conn.Close();
            this.Close();
            SKGridPage init = new SKGridPage();
            init.InitDatagrid();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
