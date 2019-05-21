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

        public PopUpDel(string codedb, string text, DataGridView ds)
        {
            this.codedb = codedb;
            InitializeComponent();
            code.Text = codedb;
            label1.Text = text;
            Console.WriteLine(ds);
        }

        private void yes_Click(object sender, EventArgs e)
        {
            SKGridPage sk = new SKGridPage();

            sk.SqlConnection();
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
            sk.InitDatagrid();
        }

        private void no_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
