using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SHT
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Icon = Properties.Resources.AppIcon;
        }

        private void about_txt_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
