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
        ConfigurationPage configpage;
        
        public ConfirmOrderPage(ConfigurationPage configpage)
        {
            this.configpage = configpage;
            InitializeComponent();
        }
        private void ConfirmOrderPage_Load(object sender, EventArgs e)
        {

        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Confirmed order, go to checkout to pay. Thank you and see you soon !");
            this.Close();
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
    }
}
