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
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openConfigurationPage));
            monthread.Start();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openSKLoginPage));
            monthread.Start();
            this.Close();
        }
        public static void openConfigurationPage()
        {
            Application.Run(new ConfigurationPage()); //opens the first Client form
        }

        public static void openSKLoginPage()
        {
            Application.Run(new SKLoginPage()); //opens the SKLoginPage
        }
    }
}
