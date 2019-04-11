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
    public partial class SKGridPage : Form
    {
        SKOrdersPage page;
        private DataGridViewColumn name;
        private DataGridViewColumn lastname;
        private DataGridViewColumn firstname;

        public SKGridPage(SKOrdersPage page)
        {
            this.page = page;

            name = new DataGridViewColumn
            {
                Name = "name",
                CellTemplate = new DataGridViewTextBoxCell()
            };

            lastname = new DataGridViewColumn
            {
                Name = "lastname",
                CellTemplate = new DataGridViewTextBoxCell()
            };

            firstname = new DataGridViewColumn
            {
                Name = "firstname",
                CellTemplate = new DataGridViewTextBoxCell()
            };


            InitializeComponent();
        }

        private void SKGridPage_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'mykitboxDataSet3.piececommand'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.piececommandTableAdapter.Fill(this.mykitboxDataSet3.piececommand);
            // TODO: cette ligne de code charge les données dans la table 'mykitboxDataSet2.prices'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.pricesTableAdapter.Fill(this.mykitboxDataSet2.prices);
            // TODO: cette ligne de code charge les données dans la table 'mykitboxDataSet1.client_command'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.client_commandTableAdapter.Fill(this.mykitboxDataSet1.client_command);
            // TODO: cette ligne de code charge les données dans la table 'mykitboxDataSet.client'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.clientTableAdapter.Fill(this.mykitboxDataSet.client);


        }

        private void Client_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Contains("name"))
            {
                dataGridView1.Columns.Remove(name);
            }
            else if (dataGridView1.Columns.Contains("firstname"))
            {
                dataGridView1.Columns.Remove(firstname);
                dataGridView1.Columns.Remove(lastname);
            }

            dataGridView1.DataSource = clientBindingSource;
            dataGridView1.Columns["idclient"].Visible = false;
            
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == clientcommandBindingSource)
            {
                while (dataGridView1.Rows.Count > 1)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }
                string searchvalue = textBox1.Text;

            }

        }

        private void Prices_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Contains("name"))
            {
                dataGridView1.Columns.Remove(name);
            }
            else if (dataGridView1.Columns.Contains("firstname") && dataGridView1.DataSource != clientBindingSource)
            {
                dataGridView1.Columns.Remove(firstname);
                dataGridView1.Columns.Remove(lastname);
            }

            dataGridView1.DataSource = pricesBindingSource;
            dataGridView1.Columns["idsupp"].Visible = false;
            
            dataGridView1.Columns.Insert(2, name);

            SqlConnection("SELECT prices.idsupp, supplier.name FROM prices INNER JOIN supplier ON prices.idsupp = supplier.idsupp ORDER BY `prices`.`idsupp` ASC", "name");
           
        }

        private void ClientCommand_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Contains("name"))
            {
                dataGridView1.Columns.Remove(name);
            }

            dataGridView1.DataSource = clientcommandBindingSource;
            dataGridView1.Columns["idclient"].Visible = false;
            
            dataGridView1.Columns.Insert(2, lastname);
            
            dataGridView1.Columns.Insert(3, firstname);

            SqlConnection("SELECT client_command.idcom, client.lastname FROM client_command INNER JOIN client ON client_command.idclient = client.idclient ORDER BY `client_command`.`idcom` ASC","lastname");
            SqlConnection("SELECT client_command.idcom, client.firstname FROM client_command INNER JOIN client ON client_command.idclient = client.idclient ORDER BY `client_command`.`idcom` ASC","firstname");

        }

        private void PieceCommand_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Contains("name"))
            {
                dataGridView1.Columns.Remove(name);
            }
            else if (dataGridView1.Columns.Contains("firstname") && dataGridView1.DataSource != clientBindingSource)
            {
                dataGridView1.Columns.Remove(firstname);
                dataGridView1.Columns.Remove(lastname);
            }

            dataGridView1.DataSource = piececommandBindingSource;
            dataGridView1.Columns["idsupp"].Visible = false;
 
            dataGridView1.Columns.Insert(3, name);

            SqlConnection("SELECT piececommand.num, supplier.name FROM piececommand INNER JOIN supplier ON piececommand.idsupp = supplier.idsupp ORDER BY `piececommand`.`num` ASC", "name");
        }

        private void SqlConnection(string com, string col)
        {
            string connString = "Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=";
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = com;
            MySqlDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                dataGridView1.Rows[count].Cells[col].Value = reader[col].ToString();
                count += 1;
            }
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
