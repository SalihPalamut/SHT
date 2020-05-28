using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Reflection;
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
            string Link = e.LinkText;
            if (Link.Contains("@")) Link += "?subject=Simple Hid Terminal";
            System.Diagnostics.Process.Start(Link);
        }

        private void About_Load(object sender, EventArgs e)
        {
            string b = ApplicationDeployment.IsNetworkDeployed
               ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
               : Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string a = "Simple Hid Terminal\n" +
                "\nVersion: " + b +
                "\n\nSLHTech®" +
                "\nCopy © 2020\n" +
                "\nmailto:palamutsalih@gmail.com" +
                "\nhttps://github.com/SalihPalamut/SHT";
            about_txt.Text = a;

        }
    }
}
