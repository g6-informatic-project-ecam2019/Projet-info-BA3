using System;
using System.Windows.Forms;

namespace Materials
{
    public partial class BlockAmountPage : Form
    {
        public int number;
        
        ConfigurationPage configpage;
        public BlockAmountPage()
        {
            InitializeComponent();
            configpage = new ConfigurationPage();
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(OpenHomePage));
            monthread.Start();
            this.Close();
        }
        public static void OpenHomePage()
        {
            Application.Run(new HomePage()); //opens the Home form
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            configpage.ShowDialog();
        }

        private void BlocksTrackBar_Scroll(object sender, EventArgs e)
        {
            label2.Text = "" + blocksTrackBar.Value;
            number = blocksTrackBar.Value; 
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
