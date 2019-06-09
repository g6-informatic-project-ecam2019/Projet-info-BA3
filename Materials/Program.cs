using System.Windows.Forms;

namespace Materials
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HomePage homepage = new HomePage();
            Application.Run(homepage);
        }
    }
}
