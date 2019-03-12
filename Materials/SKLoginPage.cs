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
    public partial class SKLoginPage : Form
    {
        public SKLoginPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openHomePage));
            monthread.Start();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "12345")
            {
                System.Threading.Thread monthread = new System.Threading.Thread(new System.Threading.ThreadStart(openSKOrdersPage));
                monthread.Start();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Password");
            }
        }

        public static void openHomePage()
        {
            Application.Run(new HomePage()); //opens the Home form
        }
        public static void openSKOrdersPage()
        {
            Application.Run(new SKOrdersPage()); //opens the Orders form
        }
    }
}