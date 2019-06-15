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
        private Stock stock = new Stock("Server=localhost;Port=3306;Database=mykitbox;Uid=root;Pwd=");
        ConfigurationPage configpage;
        Cupboard cupboard;
        float price=0;
        
        public ConfirmOrderPage(ConfigurationPage configpage, Cupboard cup)
        {
            
            this.configpage = configpage;
            this.cupboard = cup as Cupboard;
            InitializeComponent();
        }
        private string detailheight()
        {
            Dictionary<string, Object> Description = cupboard.GetDescription();
            Console.WriteLine(String.Format("total height is {0}",((int)Description["height"]).ToString()));
            return ((int)Description["height"]).ToString();
        }
        private string detailwidth()
        {
            Dictionary<string, Object> Description = cupboard.GetDescription();
            return ((int)Description["width"]).ToString();
        }
        private string detaildepth()
        {
            Dictionary<string, Object> Description = cupboard.GetDescription();
            return ((int)Description["depth"]).ToString();
        }
        private void detailprice()
        {
            Dictionary<string, Object> Description = cupboard.GetDescription();
            this.price = (float)Description["price"];
        }
        private string detailpriceBlock(int num)
        {
            Block[] block = cupboard.GetBlock();
            if (num <= block.Length)
            {
                Dictionary<string, Object> Description = block[num - 1].GetDescription();
                if (cupboard.BlockStock(num, stock))
                {
                    return "In stock" + "    Price: " + Description["price"] + "€";
                }
                else
                {
                    return "Out of stock" + "    Price: " + Description["price"] + "€";
                }
                
            }
            else return "";
        }
        private string detailblock(int num)
        {
            
            Block[] blocks = cupboard.GetBlock();
            if (num <= blocks.Length)
            {
                Dictionary<string, Object> Description = blocks[num - 1].GetDescription();
                return "Block " + num + ":                            height: " 
                               + Description["height"] + " cm, door: " 
                               + Description["door"] + ", panel\'s color: " 
                               + Description["panel"];
            }
            else return "";
        }
               
        private string detailpriceBlockNoStock(int num)
        {
            Block[] block = cupboard.GetBlock();
            if (num <= block.Length)
            {
                Dictionary<string, Object> Description = block[num - 1].GetDescription();
                if (cupboard.BlockStock((num),stock))
                {
                    return "Box " + num + " " + "in stock" + " Price: " + Description["price"] + "€";
                }
                else
                {
                    return "Box " + num + " " + "out of stock" + " Price: " + Description["price"] + "€";
                }
                
            }
            else return "";
            
        }
        private void ConfirmOrderPage_Load(object sender, EventArgs e)
        {
            detailprice();
            panelOut.Visible = false;
            textBox1.Text = detailblock(1);
            textBox2.Text = detailblock(2);
            textBox3.Text = detailblock(3);
            textBox4.Text = detailblock(4);
            textBox5.Text = detailblock(5);
            textBox6.Text = detailblock(6);
            textBox7.Text = detailblock(7);
            PrixBox1.Text = detailpriceBlock(1);
            prixBox2.Text = detailpriceBlock(2);
            PrixBox3.Text = detailpriceBlock(3);
            PrixBox4.Text = detailpriceBlock(4);
            PrixBox5.Text = detailpriceBlock(5);
            PrixBox6.Text = detailpriceBlock(6);
            PrixBox7.Text = detailpriceBlock(7);
            BoxTotalheight.Text = detailheight();
            BoxWidth.Text = detailwidth();
            BoxDepth.Text = detaildepth();
            BoxTotalPrice.Text = price.ToString();
            textBox1Out.Text = detailpriceBlockNoStock(1);
            textBox2Out.Text = detailpriceBlockNoStock(2);
            textBox3Out.Text = detailpriceBlockNoStock(3);
            textBox4Out.Text = detailpriceBlockNoStock(4);
            textBox5Out.Text = detailpriceBlockNoStock(5);
            textBox6Out.Text = detailpriceBlockNoStock(6);
            textBox7Out.Text = detailpriceBlockNoStock(7);
            textBoxTotalPayment.Text = Convert.ToString(this.price);
            textBoxAdvance.Text = Convert.ToString(this.price *0.30);
            textBox8.Text = Convert.ToString((this.price)-(this.price * 0.30));
        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (!(Stock(stock)))
            {
                panelOut.Visible = true;
            }
            else
            {
                stock.ConfirmOrder("X","X", "X", "0","0", this.cupboard);
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
            this.cupboard = null; //Tentative de correction de bug quand on back
            this.Close();
            //this.Hide();
        }
        private bool Stock(Stock stock)
        {
            Block[] block = cupboard.GetBlock();
            for (int i = 0; i < block.Length; i++)
            {
                if (!(cupboard.BlockStock((i + 1), stock)))
                {
                    return false;
                }
            }
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
            stock.ConfirmOrder(Fname, Lname, "X", "0", "0", this.cupboard);
            MessageBox.Show("Confirmed preorder, go to checkout to pay. Thank you "+Fname+" "+Lname+" and see you soon !");
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openHomePage));
            monthread.Start();
            this.Close();
        }
    }
}
