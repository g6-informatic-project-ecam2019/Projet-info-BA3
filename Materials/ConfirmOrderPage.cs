using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Materials
{
    public partial class ConfirmOrderPage : Form
    {
        private string Lname;
        private string Fname;
        ConfigurationPage configpage;
        
        public ConfirmOrderPage(ConfigurationPage configpage)
        {
            this.configpage = configpage;
            InitializeComponent();
        }
        private void ConfirmOrderPage_Load(object sender, EventArgs e)
        {
            panelOut.Visible = false;
        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (Stock())
            {
                panelOut.Visible = true;
            }
            else
            {
                
                MessageBox.Show("Confirmed order, go to checkout to pay. Thank you and see you soon !");
                System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openHomePage));
                monthread.Start();
                this.Close();
            }
            
        }

        private void buttonhome_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openHomePage));
            monthread.Start();
            this.Close();
        }
        public static void openHomePage()
        {
            Application.Run(new HomePage()); //opens the Home form
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.configpage.Show();
            this.Hide();
        }
        private bool Stock()
        {
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelOut.Visible = false;
            this.configpage.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lname = textBoxLname.Text;
            Fname = textBoxFname.Text;
            MessageBox.Show("Confirmed preorder, go to checkout to pay. Thank you "+Fname+" "+Lname+" and see you soon !");
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openHomePage));
            monthread.Start();
            this.Close();
        }
    }
}
