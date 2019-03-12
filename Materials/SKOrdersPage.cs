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
    public partial class SKOrdersPage : Form
    {
        public SKOrdersPage()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openHomePage));
            monthread.Start();
            this.Close();
        }
        public static void openHomePage()
        {
            Application.Run(new HomePage()); //opens the Home form
        }
    }
}
