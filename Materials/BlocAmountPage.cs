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
    public partial class BlocAmountPage : Form
    {
        public BlocAmountPage()
        {
            InitializeComponent();
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openHomePage));
            monthread.Start();
            this.Close();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openConfigurationPage));
            monthread.Start();
            this.Close();
        }

        private void blocsTrackBar_Scroll(object sender, EventArgs e)
        {
            label2.Text = "" + blocsTrackBar.Value;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        public static void openHomePage()
        {
            Application.Run(new HomePage()); //opens the Home form
        }

        public static void openConfigurationPage()
        {
            Application.Run(new ConfigurationPage()); //opens the first Client form
        }
    }
}
