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
        private DataTable dt_client;
        private DataTable dt_prices;
        private DataTable dt_clientcom;
        private DataTable dt_com;
        private DataTable dt_clientpieces;
        private DataTable dt_pieces;
        private List<int> RowChanged = new List<int>();
        private int row;
        private PopUpAdd popup = new PopUpAdd();

        public SKGridPage()
        {

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

            BindingSource bs_pieces = pieceBindingSource;
            dt_pieces = ((DataSet)bs_pieces.DataSource).Tables[bs_pieces.DataMember];

        }

        private void SKGridPage_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'mykitboxDataSet5.piece'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.pieceTableAdapter.Fill(this.mykitboxDataSet5.piece);
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
            Modifie.Visible = false;

            dataGridView1.DataSource = dt_clientpieces;

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
            Modifie.Visible = true;

            dataGridView1.DataSource = dt_prices;
            dataGridView1.Columns["idsupp"].Visible = false;

            SqlConnection("SELECT prices.idsupp, supplier.name FROM prices INNER JOIN supplier ON prices.idsupp = supplier.idsupp ORDER BY `prices`.`idsupp` ASC", "name");
           
        }

        private void ClientCommand_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            SearchLabel.Text = "Search by lastname :";
            dataGridView2.Visible = true;
            Modifie.Visible = true;

            dataGridView1.DataSource = dt_clientcom;
            dataGridView2.DataSource = dt_client;
            dataGridView1.Columns["idclient"].Visible = false;
            dataGridView2.Columns["idclient"].Visible = false;

            SqlConnection("SELECT client_command.idcom, client.lastname FROM client_command INNER JOIN client ON client_command.idclient = client.idclient ORDER BY `client_command`.`idcom` ASC","lastname");
            SqlConnection("SELECT client_command.idcom, client.firstname FROM client_command INNER JOIN client ON client_command.idclient = client.idclient ORDER BY `client_command`.`idcom` ASC","firstname");

        }

        private void Pieces_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            SearchLabel.Text = "Search by code :";
            dataGridView2.Visible = false;
            Modifie.Visible = true;

            dataGridView1.DataSource = dt_pieces;
        }

        private void PieceCommand_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            SearchLabel.Text = "Search by code :";
            dataGridView2.Visible = false;
            Modifie.Visible = false;

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
            else if (count == 13)
            {
                LoadTableChange(dataGridView1, "code", dt_pieces);
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
            Pieces.Enabled = false;
            textBox1.Enabled = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;

            int count = ((DataTable)dataGridView1.DataSource).Columns.Count;
            List<string> ColumnNotChanged = new List<string>();
            if (count == 8)
            {
                ColumnNotChanged.Add("command_status");
                ColumnNotChanged.Add("payment_status");
                
                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()) ; i++)
                {
                    if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "Payed")
                    {
                        dataGridView1.Rows[i].Cells[7].ReadOnly = true;
                        dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.LightGray;
                    }
                    if (dataGridView1.Rows[i].Cells[6].Value.ToString() == "Close")
                    {
                        dataGridView1.Rows[i].Cells[6].ReadOnly = true;
                        dataGridView1.Rows[i].Cells[6].Style.BackColor = Color.LightGray;
                    }

                }
            }
            else if (count == 5)
            {
                ColumnNotChanged.Add("supplier_price");
                ColumnNotChanged.Add("reprieve");
            }
            else if (count == 13)
            {

                for (var i = 1; i < ((DataTable)dataGridView1.DataSource).Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i].Name != "real_quantity" & dataGridView1.Columns[i].Name != "virtual_quantity" & dataGridView1.Columns[i].Name != "command_quantity")
                    {
                        ColumnNotChanged.Add(dataGridView1.Columns[i].Name);
                    }
                }
                RowAdd.Visible = true;
                RowDelete.Visible = true;
                textboxDel.Visible = true;
                labelDel.Visible = true;
            }


            for (var i = 0; i < ((DataTable)dataGridView1.DataSource).Columns.Count; i++)
            {
                if (!ColumnNotChanged.Contains(dataGridView1.Columns[i].Name))
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
            
        }

        private void ApplyMod_Click(object sender, EventArgs e)
        {
            int count = ((DataTable)dataGridView1.DataSource).Columns.Count;
            if (count == 8)
            {            
                SqlUpdtateStatement(string.Format("UPDATE client_command SET command_status = '{0}', payment_status = '{1}' WHERE idcom = {2} ", dataGridView1.Rows[row].Cells["command_status"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["payment_status"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["idcom"].FormattedValue.ToString()));

            }
            else if (count == 5)
            {
                SqlUpdtateStatement (string.Format("UPDATE prices SET supplier_price = {0}, reprieve = {1} WHERE idsupp = {2} AND code = '{3}'", dataGridView1.Rows[row].Cells["supplier_price"].FormattedValue.ToString().Replace(",", "."), dataGridView1.Rows[row].Cells["reprieve"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["idsupp"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["code"].FormattedValue.ToString()));
            }
            else if (count == 13)
            {
                SqlUpdtateStatement(string.Format("UPDATE piece SET ref = '{0}', dimension = '{1}', height = {2} , depth = {3}, width = {4}, color = '{5}', min_stock = {6}, client_price = {7}, box_number = {8} WHERE code = '{9}'", dataGridView1.Rows[row].Cells["ref"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["dimension"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["height"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["depth"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["width"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["color"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["min_stock"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["client_price"].FormattedValue.ToString().Replace(",", "."), dataGridView1.Rows[row].Cells["box_number"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["code"].FormattedValue.ToString()));

            }

            InitDatagrid();
        }

        private void CancelMod_Click(object sender, EventArgs e)
        {
            InitDatagrid();
        }
        public void InitDatagrid()
        {
            CancelMod.Visible = false;
            ApplyMod.Visible = false;
            Modifie.Visible = true;
            Prevbtn.Visible = true;
            ClientCommand.Enabled = true;
            Client_Pieces.Enabled = true;
            Prices.Enabled = true;
            PieceCommand.Enabled = true;
            Pieces.Enabled = true;
            textBox1.Enabled = true;
            RowAdd.Visible = false;
            RowDelete.Visible = false;
            textboxDel.Visible = false;
            labelDel.Visible = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

            for (var i = 0; i < ((DataTable)dataGridView1.DataSource).Columns.Count; i++)
            {
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.White;
            }
            if (dataGridView1.DataSource == dt_clientcom)
            {
                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
                {
                        dataGridView1.Rows[i].Cells[6].Style.BackColor = Color.White;
                        dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.White;
                }
            }

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
        private void SqlUpdtateStatement (string com)
        {
            string connString = "Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            foreach (int row in RowChanged)
            {
                command.CommandText = com;
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

        private void RowAdd_Click(object sender, EventArgs e)
        {
            popup.ShowDialog();
        }

        private void RowDelete_Click(object sender, EventArgs e)
        {
            string connString = "Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = string.Format("DELETE FROM piece WHERE code = '{0}'", textboxDel.Text);
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
            InitDatagrid();

        }
    }
}
