using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PiSharp.GpioSimulator
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void Credit_LinkClicked(object sender, MouseEventArgs e)
        {
            Process.Start(((LinkLabel)sender).Tag.ToString());
        }

        private void CmdOkay_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
