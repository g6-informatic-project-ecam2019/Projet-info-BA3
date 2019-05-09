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
        private DataTable dt_client;
        private DataTable dt_prices;
        private DataTable dt_clientcom;
        private DataTable dt_com;
        private DataTable dt_clientpieces;
        private List<int> RowChanged = new List<int>();

        public SKGridPage(SKOrdersPage page)
        {
            this.page = page;

            InitializeComponent();

            BindingSource bs_client = clientBindingSource;
            dt_client = ((DataSet)bs_client.DataSource).Tables[bs_client.DataMember];

            BindingSource bs_prices = pricesBindingSource;
            dt_prices = ((DataSet)bs_prices.DataSource).Tables[bs_prices.DataMember];
            dt_prices.Columns.Add("name", typeof(String)).SetOrdinal(2);

            BindingSource bs_clientcom = clientcommandBindingSource;
            dt_clientcom = ((DataSet)bs_clientcom.DataSource).Tables[bs_clientcom.DataMember];
            dt_clientcom.Columns.Add("lastname", typeof(String)).SetOrdinal(2);
            dt_clientcom.Columns.Add("firstname", typeof(String)).SetOrdinal(3);

            BindingSource bs_com = piececommandBindingSource;
            dt_com = ((DataSet)bs_com.DataSource).Tables[bs_com.DataMember];
            dt_com.Columns.Add("name", typeof(String)).SetOrdinal(3);

            BindingSource bs_clientpieces = clientpiecescommandBindingSource;
            dt_clientpieces = ((DataSet)bs_clientpieces.DataSource).Tables[bs_clientpieces.DataMember];
            dt_clientpieces.Columns.Add("ref", typeof(String)).SetOrdinal(3);
            dt_clientpieces.Columns.Add("dimension", typeof(String)).SetOrdinal(4);
            dt_clientpieces.Columns.Add("height", typeof(String)).SetOrdinal(5);
            dt_clientpieces.Columns.Add("depth", typeof(String)).SetOrdinal(6);
            dt_clientpieces.Columns.Add("width", typeof(String)).SetOrdinal(7);
            dt_clientpieces.Columns.Add("color", typeof(String)).SetOrdinal(8);
            dt_clientpieces.Columns.Add("client_price", typeof(String)).SetOrdinal(9);

            CancelMod.Visible = false;
            ApplyMod.Visible = false;
            dataGridView2.Visible = false;
            textBox1.Enabled = false;
        }

        private void SKGridPage_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'mykitboxDataSet4.client_piecescommand'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.client_piecescommandTableAdapter.Fill(this.mykitboxDataSet4.client_piecescommand);
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
            textBox1.Enabled = true;
            SearchLabel.Text = "Search by idcom :";
            textBox1.Text = "";
            dataGridView2.Visible = false;

            dataGridView1.DataSource = dt_clientpieces;
            dataGridView1.Columns["code"].Visible = false;

            SqlConnection("SELECT piece.ref FROM client_piecescommand INNER JOIN piece ON client_piecescommand.code = piece.code", "ref");
            SqlConnection("SELECT piece.dimension FROM client_piecescommand INNER JOIN piece ON client_piecescommand.code = piece.code", "dimension");
            SqlConnection("SELECT piece.height FROM client_piecescommand INNER JOIN piece ON client_piecescommand.code = piece.code", "height");
            SqlConnection("SELECT piece.depth FROM client_piecescommand INNER JOIN piece ON client_piecescommand.code = piece.code", "depth");
            SqlConnection("SELECT piece.width FROM client_piecescommand INNER JOIN piece ON client_piecescommand.code = piece.code", "width");
            SqlConnection("SELECT piece.color FROM client_piecescommand INNER JOIN piece ON client_piecescommand.code = piece.code", "color");
            SqlConnection("SELECT piece.client_price FROM client_piecescommand INNER JOIN piece ON client_piecescommand.code = piece.code", "client_price");


        }

        private void Prices_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            SearchLabel.Text = "Search by code :";
            dataGridView2.Visible = false;

            dataGridView1.DataSource = dt_prices;
            dataGridView1.Columns["idsupp"].Visible = false;
            
            SqlConnection("SELECT prices.idsupp, supplier.name FROM prices INNER JOIN supplier ON prices.idsupp = supplier.idsupp ORDER BY `prices`.`idsupp` ASC", "name");
           
        }

        private void ClientCommand_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            SearchLabel.Text = "Search by lastname :";
            dataGridView2.Visible = false;
  
            dataGridView1.DataSource = dt_clientcom;
            dataGridView1.Columns["idclient"].Visible = false;

            SqlConnection("SELECT client_command.idcom, client.lastname FROM client_command INNER JOIN client ON client_command.idclient = client.idclient ORDER BY `client_command`.`idcom` ASC","lastname");
            SqlConnection("SELECT client_command.idcom, client.firstname FROM client_command INNER JOIN client ON client_command.idclient = client.idclient ORDER BY `client_command`.`idcom` ASC","firstname");

        }

        private void PieceCommand_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            SearchLabel.Text = "Search by code :";
            dataGridView2.Visible = false;

            dataGridView1.DataSource = dt_com;
            dataGridView1.Columns["idsupp"].Visible = false;
 

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
        private void Prevbtn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openSKOrderPage));
            monthread.Start();
            this.Close();
        }
        public static void openSKOrderPage()
        {
            Application.Run(new SKOrdersPage()); //opens the SK's Order Page form
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int count = ((DataTable)dataGridView1.DataSource).Columns.Count;
            if (count == 8)
            {
                dataGridView2.Visible = true;
                dataGridView2.DataSource = dt_client;
                dataGridView2.Columns["idclient"].Visible = false;
                //Search for dgv2
                LoadTableChange(dataGridView2, "lastname", dt_client);
                //Search for dgv1
                LoadTableChange(dataGridView1, "lastname",dt_clientcom);
            }
            else if (count == 5)
            {
                LoadTableChange(dataGridView1, "code",dt_prices);
            }
            else if (count == 7)
            {
                LoadTableChange(dataGridView1, "code",dt_com);
            }
            else if (count == 10)
            {
                LoadTableChange(dataGridView1, "idcom", dt_clientpieces);
            }
        }

        private void LoadTableChange(DataGridView grid, string col, DataTable dt)
        {
            DataView dv = new DataView(dt)
            {
                RowFilter = string.Format("Convert({0}, System.String) LIKE '{1}*'", col, textBox1.Text)
            };
            grid.DataSource = dv.ToTable();
        }

        private void Modifie_Click(object sender, EventArgs e)
        {
            CancelMod.Visible = true;
            ApplyMod.Visible = true;
            Modifie.Visible = false;
            Prevbtn.Visible = false;
            ClientCommand.Enabled = false;
            Client_Pieces.Enabled = false;
            Prices.Enabled = false;
            PieceCommand.Enabled = false;
            textBox1.Enabled = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;
        }

        private void ApplyMod_Click(object sender, EventArgs e)
        {
            int count = ((DataTable)dataGridView1.DataSource).Columns.Count;
            if (count == 8)
            {
               //client command
            }
            else if (count == 5)
            {
                string connString = "Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=";
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand command = conn.CreateCommand();
                foreach (int row in RowChanged)
                {
                    command.CommandText = string.Format("UPDATE prices SET supplier_price = {0}, reprieve = {1} WHERE idsupp = {2} AND code = '{3}'",dataGridView1.Rows[row].Cells["supplier_price"].FormattedValue.ToString().Replace(",","."), dataGridView1.Rows[row].Cells["reprieve"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["idsupp"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["code"].FormattedValue.ToString());
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
                }
            }
            else if (count == 7)
            {
                //
            }
            else if (count == 10)
            {
                //Client pieces
            }

            InitDatagrid();
        }

        private void CancelMod_Click(object sender, EventArgs e)
        {
            InitDatagrid();
        }
        private void InitDatagrid()
        {
            CancelMod.Visible = false;
            ApplyMod.Visible = false;
            Modifie.Visible = true;
            Prevbtn.Visible = true;
            ClientCommand.Enabled = true;
            Client_Pieces.Enabled = true;
            Prices.Enabled = true;
            PieceCommand.Enabled = true;
            textBox1.Enabled = true;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (CancelMod.Visible == true)
            {
                if (!RowChanged.Contains(e.RowIndex))
                {
                    RowChanged.Add(e.RowIndex);
                }
            }
            
        }
    }
}
