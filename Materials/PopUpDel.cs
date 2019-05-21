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
        private string codedb;

        public PopUpDel(string codedb)
        {
            this.codedb = codedb;
            InitializeComponent();
            code.Text = codedb;
        }

        private void yes_Click(object sender, EventArgs e)
        {
            //string connString = "Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=";
            //MySqlConnection conn = new MySqlConnection(connString);
            //MySqlCommand command = conn.CreateCommand();
            //command.CommandText = string.Format("DELETE FROM piece WHERE code = '{0}'", this.codedb);
            //Console.WriteLine(command.CommandText);
            //try
            //{
            //    conn.Open();
            //    command.ExecuteNonQuery();
            //}
            //catch (Exception x)
            //{
            //    Console.WriteLine(x.Message);
            //}
            //conn.Close();

            this.Close();
            SKGridPage sk = new SKGridPage();
            sk.InitDatagrid();
        }

        private void no_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
