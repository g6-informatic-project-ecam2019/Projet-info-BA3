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
        public int number;
        
        ConfigurationPage configpage;
        public BlocAmountPage()
        {
            InitializeComponent();
            configpage = new ConfigurationPage();
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openHomePage));
            monthread.Start();
            this.Close();
        }
        public static void openHomePage()
        {
            Application.Run(new HomePage()); //opens the Home form
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            configpage.ShowDialog();

        }

        private void blocsTrackBar_Scroll(object sender, EventArgs e)
        {
            label2.Text = "" + blocsTrackBar.Value;
            number = blocsTrackBar.Value; 
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
