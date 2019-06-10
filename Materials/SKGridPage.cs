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
        private DataTable dt_clientparts;
        private DataTable dt_parts;
        private List<int> RowChanged = new List<int>();
        private List<int> RowChanged2 = new List<int>();
        private MySqlConnection conn;

        /***************************************************************************
         * Pre : /                                                                 *
         * Post : initialize the datatables to put into the datagridview           *
         ***************************************************************************/
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

            BindingSource bs_com = partcommandBindingSource;
            dt_com = ((DataSet)bs_com.DataSource).Tables[bs_com.DataMember];
            dt_com.Columns.Add("name", typeof(String)).SetOrdinal(3);

            BindingSource bs_clientparts = clientpartscommandBindingSource;
            dt_clientparts = ((DataSet)bs_clientparts.DataSource).Tables[bs_clientparts.DataMember];

            string[] names = {"ref","dimension","height","depth","width","color","client_price"};
            for (var i = 3; i < 10; i++)
            {
                dt_clientparts.Columns.Add(names[i-3], typeof(String)).SetOrdinal(i);
            }

            BindingSource bs_parts = partBindingSource;
            dt_parts = ((DataSet)bs_parts.DataSource).Tables[bs_parts.DataMember];
        }

        /***********************************************************************************************************************
         * Pre : receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *         
         * Post : Fill the DataSet with the db's data                                                                          *                                                      
         ***********************************************************************************************************************/      
        private void LoadData()
        {
            //Fill data into 'mykitboxDataSet5.part'
            this.partTableAdapter.Fill(this.mykitboxDataSet5.part);
            //Fill data into 'mykitboxDataSet4.client_partscommand'
            this.client_partscommandTableAdapter.Fill(this.mykitboxDataSet4.client_partscommand);
            //Fill data into 'mykitboxDataSet3.partcommand'
            this.partcommandTableAdapter.Fill(this.mykitboxDataSet3.partcommand);
            //Fill data into 'mykitboxDataSet2.prices'
            this.pricesTableAdapter.Fill(this.mykitboxDataSet2.prices);
            //Fill data into 'mykitboxDataSet1.client_command'.
            this.client_commandTableAdapter.Fill(this.mykitboxDataSet1.client_command);
            //Fill data into 'mykitboxDataSet.client'.
            this.clientTableAdapter.Fill(this.mykitboxDataSet.client);
        }
        private void SKGridPage_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /***********************************************************************************************************************
         * Pre : receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *         
         * Post : Call SqlSelect to put values into the right column                                                       *                                                      
         ***********************************************************************************************************************/
        private void ClientCommand_Click(object sender, EventArgs e)
        {
            //Initialize the Winform for this button
            textBox1.Enabled = true;
            textBox1.Visible = true;
            textBox1.Text = "";
            SearchLabel.Text = "Search by lastname :";
            dataGridView2.Visible = true;
            Modifie.Visible = true;

            dataGridView1.DataSource = dt_clientcom;
            dataGridView2.DataSource = dt_client;
            dataGridView1.Columns["idclient"].Visible = false;
            dataGridView2.Columns["idclient"].Visible = false;
            LoadData();
            //Ask to take values from the db and put them into the datagrid column
            SqlSelect("SELECT client_command.idcom, client.lastname FROM client_command INNER JOIN client ON client_command.idclient = client.idclient ORDER BY `client_command`.`idcom` ASC", "lastname");
            SqlSelect("SELECT client_command.idcom, client.firstname FROM client_command INNER JOIN client ON client_command.idclient = client.idclient ORDER BY `client_command`.`idcom` ASC", "firstname");
        }
        private void Client_Click(object sender, EventArgs e)
        {
            //Initialize the Winform for this button
            textBox1.Visible = false;
            SearchLabel.Text = "";
            dataGridView2.Visible = false;
            Modifie.Visible = false;
            dataGridView1.DataSource = dt_clientparts;
            LoadData();
            //Ask to take values from the db and put them into the datagrid column
            SqlSelect("SELECT part.ref FROM client_partscommand INNER JOIN part ON client_partscommand.code = part.code", "ref");
            SqlSelect("SELECT part.dimension FROM client_partscommand INNER JOIN part ON client_partscommand.code = part.code", "dimension");
            SqlSelect("SELECT part.height FROM client_partscommand INNER JOIN part ON client_partscommand.code = part.code", "height");
            SqlSelect("SELECT part.depth FROM client_partscommand INNER JOIN part ON client_partscommand.code = part.code", "depth");
            SqlSelect("SELECT part.width FROM client_partscommand INNER JOIN part ON client_partscommand.code = part.code", "width");
            SqlSelect("SELECT part.color FROM client_partscommand INNER JOIN part ON client_partscommand.code = part.code", "color");
            SqlSelect("SELECT part.client_price FROM client_partscommand INNER JOIN part ON client_partscommand.code = part.code", "client_price");
        }
        private void Prices_Click(object sender, EventArgs e)
        {
            //Initialize the Winform for this button
            textBox1.Enabled = true;
            textBox1.Visible = true;
            textBox1.Text = "";
            SearchLabel.Text = "Search by code :";
            dataGridView2.Visible = false;
            Modifie.Visible = true;
            dataGridView1.DataSource = dt_prices;
            dataGridView1.Columns["idsupp"].Visible = false;
            LoadData();
            //Ask to take values from the db and put them into the datagrid column
            SqlSelect("SELECT prices.idsupp, supplier.name FROM prices INNER JOIN supplier ON prices.idsupp = supplier.idsupp ORDER BY `prices`.`idsupp` ASC", "name");
        }
        private void Parts_Click(object sender, EventArgs e)
        {
            //Initialize the Winform for this button
            textBox1.Enabled = true;
            textBox1.Visible = true;
            textBox1.Text = "";
            SearchLabel.Text = "Search by code :";
            dataGridView2.Visible = false;
            Modifie.Visible = true;
            dataGridView1.DataSource = dt_parts;

            LoadData();
        }
        private void PartCommand_Click(object sender, EventArgs e)
        {
            //Initialize the Winform for this button
            textBox1.Enabled = false;
            textBox1.Visible = false;
            SearchLabel.Text = "";
            dataGridView2.Visible = false;
            Modifie.Visible = false;
            dataGridView1.DataSource = dt_com;
            dataGridView1.Columns["idsupp"].Visible = false;
            LoadData();
            //Ask to take values from the db and put them into the datagrid column
            SqlSelect("SELECT partcommand.num, supplier.name FROM partcommand INNER JOIN supplier ON partcommand.idsupp = supplier.idsupp ORDER BY `partcommand`.`num` ASC", "name");
        }

        /***********************************************************************************************************************
         * Pre : receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *         
         * Post : Call LoadTableChange to load the datagridview with the research values                                       *                                                                    
         ***********************************************************************************************************************/
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Count the number of column to make the difference
            int count = ((DataTable)dataGridView1.DataSource).Columns.Count;
            if (count == 8)
            {
                LoadTableChange(dataGridView1, "lastname", dt_clientcom);
                LoadTableChange(dataGridView2, "lastname", dt_client);
            }
            else if (count == 5)
            {
                LoadTableChange(dataGridView1, "code", dt_prices);
            }
            else if (count == 13)
            {
                LoadTableChange(dataGridView1, "code", dt_parts);
            }
        }
        /***********************************************************************************************************************
         * Pre : receive the datagridview, the name of the column to add the values and the current datatable as parameter     *         
         * Post : Put the dataview as new datasource for the datagridview                                                      *                                                                    
         ***********************************************************************************************************************/
        private void LoadTableChange(DataGridView grid, string col, DataTable dt)
        {
            //Creating the dataview from the datatable and search the values from the textbox value
            DataView dv = new DataView(dt)
            {
                RowFilter = string.Format("Convert({0}, System.String) LIKE '{1}*'", col, textBox1.Text)
            };

            grid.DataSource = dv.ToTable();
        }

        /***********************************************************************************************************************
         * Pre : receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *         
         * Post : Initialize the winform and the datagridview in the 'Modifie' mode                                            *                                                                             
         ***********************************************************************************************************************/
        private void Modifie_Click(object sender, EventArgs e)
        {
            //Initialize the 'Modifie' mode
            CancelMod.Visible = true;
            ApplyMod.Visible = true;
            Modifie.Visible = false;
            Prevbtn.Visible = false;
            ClientCommand.Enabled = false;
            Client_Parts.Enabled = false;
            Prices.Enabled = false;
            PartCommand.Enabled = false;
            Parts.Enabled = false;
            textBox1.Enabled = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;
            //List of name of the columns that we want to change values
            List<string> ColumnNotChanged = new List<string>();

            //Count the number of column to make the difference
            int count = ((DataTable)dataGridView1.DataSource).Columns.Count;
            if (count == 8)
            {
                dataGridView2.EditMode = DataGridViewEditMode.EditOnKeystroke;
                DeleteCommand.Visible = true;
                textBoxDel2.Visible = true;
                labelDel2.Visible = true;

                ColumnNotChanged.Add("command_status");
                ColumnNotChanged.Add("payment_status");

                //There is columns that we want to change values but not all 
                //Adding the ReadOnly on Cells that contains the value that the user could not change
                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
                {
                    //Column = payment_status
                    if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "Payed")
                    {
                        dataGridView1.Rows[i].Cells[7].ReadOnly = true;
                        dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.LightGray;
                    }
                    //Column = command_status
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
                //Initialize the 'Modifie' mode for this datatable
                RowAdd.Visible = true;
                RowDelete.Visible = true;
                textboxDel.Visible = true;
                textboxDel.Text = "";
                labelDel.Visible = true;
                //Adding the column to the list but we don't want to modifie [code,real_quantity,virtual_quantity,command_quantity]
                for (var i = 1; i < ((DataTable)dataGridView1.DataSource).Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i].Name != "real_quantity" & dataGridView1.Columns[i].Name != "virtual_quantity" & dataGridView1.Columns[i].Name != "command_quantity")
                    {
                        ColumnNotChanged.Add(dataGridView1.Columns[i].Name);
                    }
                }
            }

            //Put the column that we don't want to change as ReadOnly and the color as LightGray
            for (var i = 0; i < ((DataTable)dataGridView1.DataSource).Columns.Count; i++)
            {
                if (!ColumnNotChanged.Contains(dataGridView1.Columns[i].Name))
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
        private void CancelMod_Click(object sender, EventArgs e)
        {
            InitDatagrid();
        }

        /***********************************************************************************************************************
         * Pre : receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *         
         * Post : if a the value of a cell is changed, add the index of the row into the list(RowChanged)                      *                                                                                                         
         ***********************************************************************************************************************/
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Put this if to not fill the list during the first filling
            if (CancelMod.Visible == true)
            {
                if (!RowChanged.Contains(e.RowIndex))
                {
                    RowChanged.Add(e.RowIndex);
                }
            }
        }
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Put this if to not fill the list during the first filling
            if (CancelMod.Visible == true)
            {
                if (!RowChanged2.Contains(e.RowIndex))
                {
                    RowChanged2.Add(e.RowIndex);
                }
            }
        }

        /***********************************************************************************************************************
         * Pre : receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *         
         * Post : Call SqlUpdateStatement to update the values into the db and call the Initial mode after that                *                                                                                                         
         ***********************************************************************************************************************/
        private void ApplyMod_Click(object sender, EventArgs e)
        {
            //Count the number of column to make the difference
            int count = ((DataTable)dataGridView1.DataSource).Columns.Count;
            if (count == 8)
            {
                foreach (int row in RowChanged)
                {
                    SqlUpdateStatement(string.Format("UPDATE client_command SET command_status = '{0}', payment_status = '{1}' WHERE idcom = {2} ", dataGridView1.Rows[row].Cells["command_status"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["payment_status"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["idcom"].FormattedValue.ToString()));
                }
                foreach (int row in RowChanged2)
                {
                    SqlUpdateStatement(string.Format("UPDATE client SET firstname ='{0}', lastname ='{1}', adress ='{2}', zip ={3}, phonenumber ={4} WHERE idclient ={5}", dataGridView2.Rows[row].Cells["firstname"].FormattedValue.ToString(), dataGridView2.Rows[row].Cells["lastname"].FormattedValue.ToString(), dataGridView2.Rows[row].Cells["adress"].FormattedValue.ToString(), dataGridView2.Rows[row].Cells["zip"].FormattedValue.ToString(), dataGridView2.Rows[row].Cells["phonenumber"].FormattedValue.ToString(), dataGridView2.Rows[row].Cells["idclient"].FormattedValue.ToString()));
                }
            }
            else if (count == 5)
            {
                foreach (int row in RowChanged)
                {
                    SqlUpdateStatement(string.Format("UPDATE prices SET supplier_price = {0}, reprieve = {1} WHERE idsupp = {2} AND code = '{3}'", dataGridView1.Rows[row].Cells["supplier_price"].FormattedValue.ToString().Replace(",", "."), dataGridView1.Rows[row].Cells["reprieve"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["idsupp"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["code"].FormattedValue.ToString()));
                }
            }
            else if (count == 13)
            {
                foreach (int row in RowChanged)
                {
                    SqlUpdateStatement(string.Format("UPDATE part SET ref = '{0}', dimension = '{1}', height = {2} , depth = {3}, width = {4}, color = '{5}', min_stock = {6}, client_price = {7}, box_number = {8} WHERE code = '{9}'", dataGridView1.Rows[row].Cells["ref"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["dimension"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["height"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["depth"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["width"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["color"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["min_stock"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["client_price"].FormattedValue.ToString().Replace(",", "."), dataGridView1.Rows[row].Cells["box_number"].FormattedValue.ToString(), dataGridView1.Rows[row].Cells["code"].FormattedValue.ToString()));
                }
            }

            InitDatagrid();
        }
        /***********************************************************************************************************************
         * Pre : /                                                                                                             *         
         * Post : return into the Intial mode                                                                                  *                                                                                                         
         ***********************************************************************************************************************/
        public void InitDatagrid()
        {
            //Initialize the tools
            CancelMod.Visible = false;
            ApplyMod.Visible = false;
            Modifie.Visible = true;
            Prevbtn.Visible = true;
            ClientCommand.Enabled = true;
            Client_Parts.Enabled = true;
            Prices.Enabled = true;
            PartCommand.Enabled = true;
            Parts.Enabled = true;
            textBox1.Enabled = true;
            RowAdd.Visible = false;
            RowDelete.Visible = false;
            textboxDel.Visible = false;
            labelDel.Visible = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            textBox1.Text = "";
            this.partTableAdapter.Fill(this.mykitboxDataSet5.part);
            //Put the color of the columns as white (initial color)
            for (var i = 0; i < ((DataTable)dataGridView1.DataSource).Columns.Count; i++)
            {
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.White;
            }

            int count = ((DataTable)dataGridView1.DataSource).Columns.Count;
            //Put the changed cells color as white (initial color) 
            if (count == 8)
            {
                dataGridView2.EditMode = DataGridViewEditMode.EditProgrammatically;
                DeleteCommand.Visible = false;
                textBoxDel2.Visible = false;
                labelDel2.Visible = false;
                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count.ToString()); i++)
                {
                    //Column = payment_status and command_status
                    dataGridView1.Rows[i].Cells[6].Style.BackColor = Color.White;
                    dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.White;
                }
            }
        }

        /***********************************************************************************************************************
         * Pre : receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *         
         * Post : Call the Add Pop Up and call the initial datagrid when the popup is closed                                   *                                                                                                         
         ***********************************************************************************************************************/
        private void RowAdd_Click(object sender, EventArgs e)
        {
            PopUpAdd popup = new PopUpAdd();
            popup.ShowDialog();
            this.InitDatagrid();
        }
        /***********************************************************************************************************************
         * Pre : receive the type of the winform sender (button,label,...) and the event apply to this sender as parameter     *         
         * Post : Call the Delete Pop Up and call the initial datagrid when the popup is closed                                *                                                                                                         
         ***********************************************************************************************************************/
        private void RowDelete_Click(object sender, EventArgs e)
        {
            PopUpDel popUpDel = new PopUpDel(textboxDel.Text,"Do you want to delete the part :");
            popUpDel.ShowDialog();
            this.InitDatagrid();
        }
        private void DeleteCommand_Click(object sender, EventArgs e)
        {
            PopUpDel popUpDel2 = new PopUpDel(textBoxDel2.Text, "Do you want to delete the command :");
            popUpDel2.ShowDialog();
            this.InitDatagrid();
        }

        /***********************************************************************************************************************
         * Pre : /                                                                                                             *         
         * Post : make the connection to the database                                                                          *                                                                                                               
         ***********************************************************************************************************************/
        public void SqlConnection()
        {
            string connString = "Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=";
            conn = new MySqlConnection(connString);
            try
            {
                //Open the database connection
                conn.Open();
            }
            catch (Exception e)
            {
                //Raise the error
                Console.WriteLine(e.Message);
            }
        }
        /***********************************************************************************************************************
         * Pre : get the select sql command and the name of the column as parameter                                            *         
         * Post : fill the column with the values of the database                                                              *                                                                                                               
         ***********************************************************************************************************************/
        private void SqlSelect(string com, string col)
        {
            SqlConnection();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = com;

            //Read the command et put the values into the column
            MySqlDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                dataGridView1.Rows[count].Cells[col].Value = reader[col].ToString();
                count += 1;
            }
            //Close the databsse connection
            conn.Close();
        }
        /***********************************************************************************************************************
         * Pre : get the select sql command as parameter                                                                       *         
         * Post : update the database with de modified values                                                                  *                                                                                                               
         ***********************************************************************************************************************/
        private void SqlUpdateStatement(string com)
        {
            SqlConnection();
            MySqlCommand command = conn.CreateCommand();
            //Update the database for each modified row
            command.CommandText = com;
            command.ExecuteNonQuery();
            //Close the databsse connection
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


    }
}
