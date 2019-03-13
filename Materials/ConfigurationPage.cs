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
    public partial class ConfigurationPage : Form
    {
        public ConfigurationPage()
        {
            InitializeComponent();
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openHomePage));
            monthread.Start();
            this.Close();
        }

        private void PrevBlocBtn_Click(object sender, EventArgs e)
        {

        }

        private void NextBlocBtn_Click(object sender, EventArgs e)
        {

        }

        private void MkOrdrBtn_Click(object sender, EventArgs e)
        {

        }
        public static void openHomePage()
        {
            Application.Run(new HomePage()); //opens the Home form
        }

        private void widthTrackBar_Scroll(object sender, EventArgs e)
        {
            widthLabel.Text = "" + widthTrackBar.Value;
        }

        private void depthTrackBar_Scroll(object sender, EventArgs e)
        {

        }

        private void heightTrackBar_Scroll(object sender, EventArgs e)
        {

        }
    }
}
