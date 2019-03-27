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
        Cupboard cupboard;
        
        public ConfirmOrderPage(ConfigurationPage configpage)
        {
            
            this.configpage = configpage;
            this.cupboard = this.configpage.EnvoieCup();
            InitializeComponent();
        }
        private string detailheight()
        {
            Dictionary<string, Object> Description = cupboard.GetDescription();
            return ""+Description["height"];
        }
        private string detailwidth()
        {
            Dictionary<string, Object> Description = cupboard.GetDescription();
            return "" + Description["width"];
        }
        private string detaildepth()
        {
            Dictionary<string, Object> Description = cupboard.GetDescription();
            return "" + Description["depth"];
        }
        private string detailprice()
        {
            Dictionary<string, Object> Description = cupboard.GetDescription();
            return "" + Description["price"];
        }
        private string detailpriceBloc(int num)
        {
            Bloc[] bloc = cupboard.GetBloc();
            if (num < bloc.Length)
            {
                Dictionary<string, Object> Description = bloc[num - 1].GetDescription();
                if (cupboard.BlocStock(num))
                {
                    return "In stock" + "    Price: " + Description["price"] + "$";
                }
                else
                {
                    return "Out of stock" + "    Price: " + Description["price"] + "$";
                }
                
            }
            else return "";
        }
        private string detailbloc(int num)
        {
            
            Bloc[] bloc = cupboard.GetBloc();
            if (num < bloc.Length)
            {
                Dictionary<string, Object> Description = bloc[num - 1].GetDescription();
                return "Bloc " + num + ":                            height: " + Description["height"] + " cm, door: " + Description["door"] + ", color panel: " + Description["panel"];
            }
            else return "";
        }
               
        private string detailpriceBlocNoStock(int num)
        {
            
            Bloc[] bloc = cupboard.GetBloc();
            if (num < bloc.Length)
            {
                Dictionary<string, Object> Description = bloc[num - 1].GetDescription();
                if (cupboard.BlocStock(num))
                {
                    return "Box " + num + " " + "in stock" + " Price: " + Description["price"] + "$";
                }
                else
                {
                    return "Box " + num + " " + "out of stock" + " Price: " + Description["price"] + "$";
                }
                
            }
            else return "";
            
        }
        private void ConfirmOrderPage_Load(object sender, EventArgs e)
        {
            panelOut.Visible = false;
            textBox1.Text = detailbloc(1);
            textBox2.Text = detailbloc(2);
            textBox3.Text = detailbloc(3);
            textBox4.Text = detailbloc(4);
            textBox5.Text = detailbloc(5);
            textBox6.Text = detailbloc(6);
            textBox7.Text = detailbloc(7);
            PrixBox1.Text = detailpriceBloc(1);
            prixBox2.Text = detailpriceBloc(2);
            PrixBox3.Text = detailpriceBloc(3);
            PrixBox4.Text = detailpriceBloc(4);
            PrixBox5.Text = detailpriceBloc(5);
            PrixBox6.Text = detailpriceBloc(6);
            PrixBox7.Text = detailpriceBloc(7);
            BoxTotalheight.Text = detailheight();
            BoxWidth.Text = detailwidth();
            BoxDepth.Text = detaildepth();
            BoxTotalPrice.Text = detailprice();
            textBox1Out.Text = detailpriceBlocNoStock(1);
            textBox2Out.Text = detailpriceBlocNoStock(2);
            textBox3Out.Text = detailpriceBlocNoStock(3);
            textBox4Out.Text = detailpriceBlocNoStock(4);
            textBox5Out.Text = detailpriceBlocNoStock(5);
            textBox6Out.Text = detailpriceBlocNoStock(6);
            textBox7Out.Text = detailpriceBlocNoStock(7);
            textBoxTotalPayment.Text = detailprice();
            textBoxAdvance.Text = Convert.ToString(Convert.ToInt32(detailprice())*0.30);
            textBoxAdvance.Text = Convert.ToString(Convert.ToInt32(detailprice()) * 0.30);
            textBox8.Text = Convert.ToString(Convert.ToInt32(detailprice())-(Convert.ToInt32(detailprice()) * 0.30));
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
