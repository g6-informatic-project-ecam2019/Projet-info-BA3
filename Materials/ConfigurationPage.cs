﻿using System;
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
        private int depth;
        private int width;
        private int totalheight;
        private int[] height = new int[] { 0, 0, 0, 0, 0, 0 ,0};
        private string angleColor;
        private string[] panelsColor = new string[7];
        private string[] door = new string[7];
        public Cupboard cupboard1;
        
        List<System.Windows.Forms.Panel> listPanel = new List<System.Windows.Forms.Panel>();
        int index=0;
        private ConfirmOrderPage configpage;
        private Stock stock=new Stock("Server = localhost; Port = 3306; Database = mykitbox; Uid = root; Pwd =; Connect Timeout=60");
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
        private void PrevBlockBtn_Click(object sender, EventArgs e)
        {
            Refrechmesure();
            Number();
            if (index >0)
            {
                for (int i = 0; i < 7; i++)
                {
                    listPanel[i].Visible = false;
                }
                listPanel[--index].Visible = true;
            }
            StaticMesure();
            TextBox(Number());
        }
        
        private void NextBlockBtn_Click(object sender, EventArgs e)
        {
            Refrechmesure();
            Number();
            StaticMesure();
            TextBox(Number());
            switch (index)
            {
                case 0:
                    if ((height[index] != 0) && (PanelColorBox1.Text != "") && (DoorBox1.Text != "") && (angleColor != ""))
                    {
                        if (index < 6)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                listPanel[i].Visible = false;
                            }
                            listPanel[++index].Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The previous block is not configured");
                    }
                    break;
                case 1:
                    if ((height[index] != 0) && (PanelColorBox2.Text != "") && (DoorBox2.Text != ""))
                    {
                        if (index < 6)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                listPanel[i].Visible = false;
                            }
                            listPanel[++index].Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The previous block is not configured");
                    }
                    break;
                case 2:
                    if ((height[index] != 0) && (PanelColorBox3.Text != "") && (DoorBox3.Text != ""))
                    {
                        if (index < 6)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                listPanel[i].Visible = false;
                            }
                            listPanel[++index].Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The previous block is not configured");
                    }
                    break;
                case 3:
                    if ((height[index] != 0) && (PanelColorBox4.Text != "") && (DoorBox4.Text != ""))
                    {
                        if (index < 6)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                listPanel[i].Visible = false;
                            }
                            listPanel[++index].Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The previous block is not configured");
                    }
                    break;
                case 4:
                    if ((height[index] != 0) && (PanelColorBox5.Text != "") && (DoorBox5.Text != ""))
                    {
                        if (index < 6)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                listPanel[i].Visible = false;
                            }
                            listPanel[++index].Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The previous block is not configured");
                    }
                    break;
                case 5:
                    if ((height[index] != 0) && (PanelColorBox6.Text != "") && (DoorBox6.Text != ""))
                    {
                        if (index < 6)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                listPanel[i].Visible = false;
                            }
                            listPanel[++index].Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The previous block is not configured");
                    }
                    break;
                case 6:
                    if ((height[index] != 0) && (PanelColorBox7.Text != "") && (DoorBox7.Text != ""))
                    {
                        if (index < 6)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                listPanel[i].Visible = false;
                            }
                            listPanel[++index].Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The previous block is not configured");
                    }
                    break;
            }
            
        }

        private void MkOrdrBtn_Click(object sender, EventArgs e)
        {
            if (height[0] != 0)
            {
                if (this.cupboard1 != null)
                {
                    for(int x = 0; x < 7; x++)
                    {
                        cupboard1.RemoveBlock(x);
                    }
                    this.cupboard1 = null;//Tentative de correction de bug quand on back
                    this.configpage = null;//Tentative de correction de bug quand on back
                    System.GC.Collect();//Tentative de correction de bug quand on back
                    System.GC.WaitForPendingFinalizers(); 
                }
                cupboard1 = new Cupboard(depth, width, angleColor, Number());
                //Door();
                for (int i = 0; i < Number(); i++)
                {
                    Console.WriteLine(String.Format("height of box {0} is {1}", i, height[i]));
                    cupboard1.AddBlock(new Box(height[i], panelsColor[i], cupboard1, door[i]));//hasdoor[i], doorcolor[i]
                }
                cupboard1.AddAngles(stock);
                Console.WriteLine("\n cupboard built");
                configpage = new ConfirmOrderPage(this, cupboard1);
                configpage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You have not set up the cupboard");
            }
        }
        private void TotalHeight()
        {
            totalheight = 0;
            int i;
            for (i = 0; i <7; i++)
            {
                totalheight += height[i];
            }
        }
        //private void Door()
        //{
        //    string glass = "GlassDoor";
        //    string classic = "ClassisDoor";
        //    int i;
        //    for (i=0; i < Number(); i++)
        //    {
        //        hasdoor[i] = true;
        //        if (door[i] == "No door")
        //        {
        //            hasdoor[i] = false;
        //            typedoor[i] = null;
        //            doorcolor[i] = null;
        //        }
        //        else if (door[i] == "Glass")
        //        {
        //            typedoor[i] = glass;
        //            doorcolor[i] = null;
        //        }
        //        else
        //        {
        //            typedoor[i] = classic;
        //            doorcolor[i] = door[i];
        //        }
        //    }
        //}
        private int Number()
        {
            int i;
            int h=0;
            for (i = 0; i < height.Length; i++)
            {
                if (height[i] != 0)
                {
                    h++;
                }
                
            }
            return h;
        }
        private void Refrechmesure()
        {
            if (index == 0)
            {
                height[0] = Convert.ToInt32(heightBox1.Text);
                panelsColor[0] = PanelColorBox1.Text;
                door[0] = DoorBox1.Text;
            }
            if (index == 1)
            {
                height[1] = Convert.ToInt32(heightBox2.Text);
                panelsColor[1] = PanelColorBox2.Text;
                door[1] = DoorBox2.Text;
            }
            if (index == 2)
            {
                height[2] = Convert.ToInt32(heightBox3.Text);
                panelsColor[2] = PanelColorBox3.Text;
                door[2] = DoorBox3.Text;
            }
            if (index == 3)
            {
                height[3] = Convert.ToInt32(heightBox4.Text);
                panelsColor[3] = PanelColorBox4.Text;
                door[3] = DoorBox4.Text;
            }
            if (index == 4)
            {
                height[4] = Convert.ToInt32(heightBox5.Text);
                panelsColor[4] = PanelColorBox5.Text;
                door[4] = DoorBox5.Text;
            }
            if (index == 5)
            {
                height[5] = Convert.ToInt32(heightBox6.Text);
                panelsColor[5] = PanelColorBox6.Text;
                door[5] = DoorBox6.Text;
            }
            if (index == 6)
            {
                height[6] = Convert.ToInt32(heightBox7.Text);
                panelsColor[6] = PanelColorBox7.Text;
                door[6] = DoorBox7.Text;
            }
        }
        private void StaticMesure()
        {
            widthBox2.Text = Convert.ToString(this.width);
            DepthBox2.Text = Convert.ToString(this.depth);
            AnglesColorBox2.Text = this.angleColor;
            widthBox5.Text = Convert.ToString(this.width);
            DepthBox5.Text = Convert.ToString(this.depth);
            AnglesColorBox5.Text = this.angleColor;
            widthBox3.Text = Convert.ToString(this.width);
            DepthBox3.Text = Convert.ToString(this.depth);
            AnglesColorBox3.Text = this.angleColor;
            widthBox4.Text = Convert.ToString(this.width);
            DepthBox4.Text = Convert.ToString(this.depth);
            AnglesColorBox4.Text = this.angleColor;
            widthBox6.Text = Convert.ToString(this.width);
            DepthBox6.Text = Convert.ToString(this.depth);
            AnglesColorBox6.Text = this.angleColor;
            widthBox7.Text = Convert.ToString(this.width);
            DepthBox7.Text = Convert.ToString(this.depth);
            AnglesColorBox7.Text = this.angleColor;
        }
        public static void openHomePage()
        {
            Application.Run(new HomePage()); //opens the Home form
        }
        private void heightBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            height[0] = Convert.ToInt32(heightBox1.Text);
        }
        private void widthBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            width = Convert.ToInt32(widthBox1.Text);
        }
        private void DepthBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            depth = Convert.ToInt32(DepthBox1.Text);
        }
        private void PanelColorBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelsColor[0]=(PanelColorBox1.Text);
        }
        private void AnglesColorBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            angleColor = AnglesColorBox1.Text;
        }
        private void DoorBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            door[0]=(DoorBox1.Text);
        }
        private void heightBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            height[1] = Convert.ToInt32(heightBox2.Text);
        }
        private void PanelColorBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelsColor[1] = (PanelColorBox2.Text);
        }
        private void DoorBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            door[1] = (DoorBox2.Text);
        }
        private void heightBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            height[2] = Convert.ToInt32(heightBox3.Text);
        }
        private void PanelColorBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelsColor[2] = (PanelColorBox3.Text);
        }
        private void DoorBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            door[2] = (DoorBox3.Text);
        }
        private void heightBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            height[3] = Convert.ToInt32(heightBox4.Text);
        }
        private void PanelColorBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelsColor[3] = (PanelColorBox4.Text);
        }
        private void DoorBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            door[3] = (DoorBox4.Text);
        }
        private void heightBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            height[4] = Convert.ToInt32(heightBox5.Text);
        }
        private void PanelColorBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelsColor[4] = (PanelColorBox5.Text);
        }
        private void DoorBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            door[4] = (DoorBox5.Text);
        }
        private void heightBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            height[5] = Convert.ToInt32(heightBox6.Text);
        }
        private void PanelColorBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelsColor[5] = (PanelColorBox6.Text);
        }
        private void DoorBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            door[5] = (DoorBox6.Text);
        }
        private void heightBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            height[6] = Convert.ToInt32(heightBox7.Text);
        }
        private void PanelColorBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelsColor[6] = (PanelColorBox7.Text);
        }
        private void DoorBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            door[6] = (DoorBox7.Text);
        }
        private void ConfigurationPage_Load(object sender, EventArgs e)
        {

            listPanel.Add(panel1);
            listPanel.Add(panel2);
            listPanel.Add(panel3);
            listPanel.Add(panel4);
            listPanel.Add(panel5);
            listPanel.Add(panel6);
            listPanel.Add(panel7);
            for (int i = 0;i<listPanel.Count;i++)
            {
                listPanel[i].Parent = panel1.Parent;
                listPanel[i].Top=panel1.Top;
                listPanel[i].Left = panel1.Left;
                listPanel[i].Visible = false;
            }
            listPanel[index].Visible=true;
            heightBox1.Text = "0";
            heightBox2.Text = "0";
            heightBox3.Text = "0";
            heightBox4.Text = "0";
            heightBox5.Text = "0";
            heightBox6.Text = "0";
            heightBox7.Text = "0";

            DataDim();
            
            //cupboard1.GetPrice();
            


        }
        private void DataDim()
        {

            
                        //height
            List<int> dimheightok = stock.ExistingDimension("height", "Tasseau");
            string[] dimheightokstring = new string[dimheightok.Count];
            for (int i = 0; i < dimheightok.Count; i++)
            {
                dimheightokstring[i] = Convert.ToString(dimheightok[i]);
            }
            this.heightBox1.BeginUpdate();
            this.heightBox1.Items.Clear();
            this.heightBox1.Items.AddRange(dimheightokstring);
            this.heightBox1.EndUpdate();

            this.heightBox2.BeginUpdate();
            this.heightBox2.Items.Clear();
            this.heightBox2.Items.AddRange(dimheightokstring);
            this.heightBox2.EndUpdate();

            this.heightBox3.BeginUpdate();
            this.heightBox3.Items.Clear();
            this.heightBox3.Items.AddRange(dimheightokstring);
            this.heightBox3.EndUpdate();

            this.heightBox4.BeginUpdate();
            this.heightBox4.Items.Clear();
            this.heightBox4.Items.AddRange(dimheightokstring);
            this.heightBox4.EndUpdate();

            this.heightBox5.BeginUpdate();
            this.heightBox5.Items.Clear();
            this.heightBox5.Items.AddRange(dimheightokstring);
            this.heightBox5.EndUpdate();

            this.heightBox6.BeginUpdate();
            this.heightBox6.Items.Clear();
            this.heightBox6.Items.AddRange(dimheightokstring);
            this.heightBox6.EndUpdate();

            this.heightBox7.BeginUpdate();
            this.heightBox7.Items.Clear();
            this.heightBox7.Items.AddRange(dimheightokstring);
            this.heightBox7.EndUpdate();

            //Width
            List<int> dimwidthok = stock.ExistingDimension("width", "Traverse Ar");
            string[] dimwidthokstring = new string[dimwidthok.Count];
            for (int i = 0; i < dimwidthok.Count; i++)
            {
                dimwidthokstring[i] = Convert.ToString(dimwidthok[i]);
            }
            this.widthBox1.BeginUpdate();
            this.widthBox1.Items.Clear();
            this.widthBox1.Items.AddRange(dimwidthokstring);
            this.widthBox1.EndUpdate();

            //Depth
            List<int> dimdepthok = stock.ExistingDimension("depth", "Traverse GD");
            string[] dimdepthokstring = new string[dimdepthok.Count];
            for (int i = 0; i < dimdepthok.Count; i++)
            {
                dimdepthokstring[i] = Convert.ToString(dimdepthok[i]);
            }
            this.DepthBox1.BeginUpdate();
            this.DepthBox1.Items.Clear();
            this.DepthBox1.Items.AddRange(dimdepthokstring);
            this.DepthBox1.EndUpdate();
        }
        private void ConfigurationPage_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        
        private void ModifBlock1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < listPanel.Count; i++)
            {
                listPanel[i].Visible = false;
            }
            listPanel[0].Visible = true;
            index = 0;
        }

        private void ModifBlock2_Click(object sender, EventArgs e)
        {
            if((height[0]!=0) && (PanelColorBox2.Text != "") && (DoorBox2.Text != ""))
            {
                for (int i = 0; i < listPanel.Count; i++)
                {
                    listPanel[i].Visible = false;
                }
                listPanel[1].Visible = true;
                index = 1;
            }
            else
            {
                MessageBox.Show("The previous block is not configured");
            }
        }

        private void ModifBlock3_Click(object sender, EventArgs e)
        {
            if ((height[1] != 0) && (PanelColorBox3.Text != "") && (DoorBox3.Text != ""))
            {
                for (int i = 0; i < listPanel.Count; i++)
                {
                    listPanel[i].Visible = false;
                }
                listPanel[2].Visible = true;
                index = 2;
            }
            else
            {
                MessageBox.Show("The previous block is not configured");
            }
            
        }

        private void ModifBlock4_Click(object sender, EventArgs e)
        {
            if ((height[2] != 0) && (PanelColorBox4.Text != "") && (DoorBox4.Text != ""))
            {
                for (int i = 0; i < listPanel.Count; i++)
                {
                    listPanel[i].Visible = false;
                }
                listPanel[3].Visible = true;
                index = 3;
            }
            else
            {
                MessageBox.Show("The previous block is not configured");
            }
        }

        private void ModifBlock5_Click(object sender, EventArgs e)
        {
            if ((height[3] != 0) && (PanelColorBox5.Text != "") && (DoorBox5.Text != ""))
            {
                for (int i = 0; i < listPanel.Count; i++)
                {
                    listPanel[i].Visible = false;
                }
                listPanel[4].Visible = true;
                index = 4;
            }
            else
            {
                MessageBox.Show("The previous block is not configured");
            }

        }
        private void ModifBlock6_Click(object sender, EventArgs e)
        {
            if ((height[4] != 0) && (PanelColorBox6.Text != "") && (DoorBox6.Text != ""))
            {
                for (int i = 0; i < listPanel.Count; i++)
                {
                    listPanel[i].Visible = false;
                }
                listPanel[5].Visible = true;
                index = 5;
            }
            else
            {
                MessageBox.Show("The previous block is not configured");
            }
        }

        private void ModifBlock7_Click(object sender, EventArgs e)
        {
            if ((height[5] != 0) && (PanelColorBox7.Text != "") && (DoorBox7.Text != ""))
            {
                for (int i = 0; i < listPanel.Count; i++)
                {
                    listPanel[i].Visible = false;
                }
                listPanel[6].Visible = true;
                index = 6;
            }
            else
            {
                MessageBox.Show("The previous block is not configured");
            }
        }
        private void TextBox(int i)
        {
            if (i == 0)
            {
                textBoxx1.Text = "";
                textBoxx2.Text = "";
                textBoxx3.Text = "";
                textBoxx4.Text = "";
                textBoxx5.Text = "";
                textBoxx6.Text = "";
                textBoxx7.Text = "";
            }
            if (i == 1)
            {
                textBoxx1.Text ="Block 1: "+this.height[0]+"cm";
                textBoxx2.Text = "";
                textBoxx3.Text = "";
                textBoxx4.Text = "";
                textBoxx5.Text = "";
                textBoxx6.Text = "";
                textBoxx7.Text = "";
            }
            if (i == 2)
            {
                textBoxx1.Text = "Block 1: " + this.height[0] + "cm";
                textBoxx2.Text = "Block 2: " + this.height[1] + "cm";
                textBoxx3.Text = "";
                textBoxx4.Text = "";
                textBoxx5.Text = "";
                textBoxx6.Text = "";
                textBoxx7.Text = "";
            }
            if (i == 3)
            {
                textBoxx1.Text = "Block 1: " + this.height[0] + "cm";
                textBoxx2.Text = "Block 2: " + this.height[1] + "cm";
                textBoxx3.Text = "Block 3: " + this.height[2] + "cm";
                textBoxx4.Text = "";
                textBoxx5.Text = "";
                textBoxx6.Text = "";
                textBoxx7.Text = "";
            }
            if (i == 4)
            {
                textBoxx1.Text = "Block 1: " + this.height[0] + "cm";
                textBoxx2.Text = "Block 2: " + this.height[1] + "cm";
                textBoxx3.Text = "Block 3: " + this.height[2] + "cm";
                textBoxx4.Text = "Block 4: " + this.height[3] + "cm";
                textBoxx5.Text = "";
                textBoxx6.Text = "";
                textBoxx7.Text = "";
            }
            if (i == 5)
            {
                textBoxx1.Text = "Block 1: " + this.height[0] + "cm";
                textBoxx2.Text = "Block 2: " + this.height[1] + "cm";
                textBoxx3.Text = "Block 3: " + this.height[2] + "cm";
                textBoxx4.Text = "Block 4: " + this.height[3] + "cm";
                textBoxx5.Text = "Block 5: " + this.height[4] + "cm";
                textBoxx6.Text = "";
                textBoxx7.Text = "";
            }
            if (i == 6)
            {
                textBoxx1.Text = "Block 1: " + this.height[0] + "cm";
                textBoxx2.Text = "Block 2: " + this.height[1] + "cm";
                textBoxx3.Text = "Block 3: " + this.height[2] + "cm";
                textBoxx4.Text = "Block 4: " + this.height[3] + "cm";
                textBoxx5.Text = "Block 5: " + this.height[4] + "cm";
                textBoxx6.Text = "Block 6: " + this.height[5] + "cm";
                textBoxx7.Text = "";
            }
            if (i == 7)
            {
                textBoxx1.Text = "Block 1: " + this.height[0] + "cm";
                textBoxx2.Text = "Block 2: " + this.height[1] + "cm";
                textBoxx3.Text = "Block 3: " + this.height[2] + "cm";
                textBoxx4.Text = "Block 4: " + this.height[3] + "cm";
                textBoxx5.Text = "Block 5: " + this.height[4] + "cm";
                textBoxx6.Text = "Block 6: " + this.height[5] + "cm";
                textBoxx7.Text = "Block 7: " + this.height[6] + "cm";
            }
            TotalHeight();
            texttotalheigth.Text = Convert.ToString(totalheight);
        }
        private void Refrechforcopy(int i)
        {
            
            if (i == 1)
            {
                this.height[i-1] = Convert.ToInt32(heightBox1.Text);
                this.panelsColor[i-1] = PanelColorBox1.Text;
                this.door[i-1] = DoorBox1.Text;
                heightBox2.Text = Convert.ToString(this.height[i]);
                PanelColorBox2.Text = this.panelsColor[i];
                DoorBox2.Text = this.door[i];

            }
            else if (i == 2)
            {
                heightBox3.Text = Convert.ToString(this.height[i]);
                PanelColorBox3.Text = this.panelsColor[i];
                DoorBox3.Text = this.door[i];
            }
            else if (i == 3)
            {
                heightBox4.Text = Convert.ToString(this.height[i]);
                PanelColorBox4.Text = this.panelsColor[i];
                DoorBox4.Text = this.door[i];
            }
            else if (i == 4)
            {
                heightBox5.Text = Convert.ToString(this.height[i]);
                PanelColorBox5.Text = this.panelsColor[i];
                DoorBox5.Text = this.door[i];
            }
            else if (i == 5)
            {
                heightBox6.Text = Convert.ToString(height[i]);
                PanelColorBox6.Text = panelsColor[i];
                DoorBox6.Text = door[i];
            }
            else if (i == 6)
            {
                heightBox7.Text = Convert.ToString(this.height[i]);
                PanelColorBox7.Text = this.panelsColor[i];
                DoorBox7.Text = this.door[i];
            }
        }
        private void CopyboxClick(int i)
        {
            if (Number() >i &Number()<7)
            {
                this.height[Number()] = this.height[i];
                this.panelsColor[Number() - 1] = this.panelsColor[i];
                this.door[Number() - 1] = this.door[i];
                Refrechforcopy(Number()-1);
                StaticMesure();
                TextBox(Number());
    }
            else if (Number() == 7)
            {
                MessageBox.Show("You have configured all blocks");
            }
            else
            {
                MessageBox.Show("Block "+(i+1)+" is not configured");
            }
        }
        private void CopyBox1_Click(object sender, EventArgs e)
        {

            CopyboxClick(0);
        }

        private void CopyBox2_Click(object sender, EventArgs e)
        {
            CopyboxClick(1);
        }

        private void CopyBox3_Click(object sender, EventArgs e)
        {
            CopyboxClick(2);
        }

        private void CopyBox4_Click(object sender, EventArgs e)
        {
            CopyboxClick(3);
        }

        private void CopyBox5_Click(object sender, EventArgs e)
        {
            CopyboxClick(4);
        }

        private void CopyBox6_Click(object sender, EventArgs e)
        {
            CopyboxClick(5);
        }

        private void CopyBox7_Click(object sender, EventArgs e)
        {
            CopyboxClick(6);
        }
        private void DeleteClick(int i)
        {

            for (int h=i; h < 7; h++)
            {
                this.height[h] = 0;
                this.panelsColor[h] = "";
                this.door[h] = "";
            }
            if (i == 0)
            {
                heightBox1.Text = "";
                heightBox2.Text = "";
                heightBox3.Text = "";
                heightBox4.Text = "";
                heightBox5.Text = "";
                heightBox6.Text = "";
                heightBox7.Text = "";
                PanelColorBox1.Text = "";
                PanelColorBox2.Text = "";
                PanelColorBox3.Text = "";
                PanelColorBox4.Text = "";
                PanelColorBox5.Text = "";
                PanelColorBox6.Text = "";
                PanelColorBox7.Text = "";
                DoorBox1.Text = "";
                DoorBox2.Text = "";
                DoorBox3.Text = "";
                DoorBox4.Text = "";
                DoorBox5.Text = "";
                DoorBox6.Text = "";
                DoorBox7.Text = "";
                this.width = 0;
                this.depth = 0;
                this.angleColor = "";
                DepthBox1.Text = "";
                widthBox1.Text = "";
                AnglesColorBox1.Text = "";
                StaticMesure();

            }
            if (i == 1)
            {
                heightBox2.Text = "";
                heightBox3.Text = "";
                heightBox4.Text = "";
                heightBox5.Text = "";
                heightBox6.Text = "";
                heightBox7.Text = "";
                PanelColorBox2.Text = "";
                PanelColorBox3.Text = "";
                PanelColorBox4.Text = "";
                PanelColorBox5.Text = "";
                PanelColorBox6.Text = "";
                PanelColorBox7.Text = "";
                DoorBox2.Text = "";
                DoorBox3.Text = "";
                DoorBox4.Text = "";
                DoorBox5.Text = "";
                DoorBox6.Text = "";
                DoorBox7.Text = "";
            }
            if (i == 2)
            {
                heightBox3.Text = "";
                heightBox4.Text = "";
                heightBox5.Text = "";
                heightBox6.Text = "";
                heightBox7.Text = "";
                PanelColorBox3.Text = "";
                PanelColorBox4.Text = "";
                PanelColorBox5.Text = "";
                PanelColorBox6.Text = "";
                PanelColorBox7.Text = "";
                DoorBox3.Text = "";
                DoorBox4.Text = "";
                DoorBox5.Text = "";
                DoorBox6.Text = "";
                DoorBox7.Text = "";
            }
            if (i == 3)
            {
                heightBox4.Text = "";
                heightBox5.Text = "";
                heightBox6.Text = "";
                heightBox7.Text = "";
                PanelColorBox4.Text = "";
                PanelColorBox5.Text = "";
                PanelColorBox6.Text = "";
                PanelColorBox7.Text = "";
                DoorBox4.Text = "";
                DoorBox5.Text = "";
                DoorBox6.Text = "";
                DoorBox7.Text = "";
            }
            if (i == 4)
            {
                heightBox5.Text = "";
                heightBox6.Text = "";
                heightBox7.Text = "";
                PanelColorBox5.Text = "";
                PanelColorBox6.Text = "";
                PanelColorBox7.Text = "";
                DoorBox5.Text = "";
                DoorBox6.Text = "";
                DoorBox7.Text = "";
            }
            if (i == 5)
            {
                heightBox6.Text = "";
                heightBox7.Text = "";
                PanelColorBox6.Text = "";
                PanelColorBox7.Text = "";
                DoorBox6.Text = "";
                DoorBox7.Text = "";
            }
            if (i == 6)
            {
                heightBox7.Text = "";
                PanelColorBox7.Text = "";
                DoorBox7.Text = "";
            }
            TextBox(Number());
        }
        private void DeleteBox1_Click(object sender, EventArgs e)
        {
            DeleteClick(0);
        }

        private void DeleteBox2_Click(object sender, EventArgs e)
        {
            DeleteClick(1);
        }

        private void DeleteBox3_Click(object sender, EventArgs e)
        {
            DeleteClick(2);
        }

        private void DeleteBox4_Click(object sender, EventArgs e)
        {
            DeleteClick(3);
        }

        private void DeleteBox5_Click(object sender, EventArgs e)
        {
            DeleteClick(4);
        }

        private void DeleteBox6_Click(object sender, EventArgs e)
        {
            DeleteClick(5);
        }

        private void DeleteBox7_Click(object sender, EventArgs e)
        {
            DeleteClick(6);
        }
    }
}
