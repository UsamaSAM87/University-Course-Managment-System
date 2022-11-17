using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DataCapture
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string pass = textBox2.Text;
            string Line = "";
            using (StreamWriter w = File.AppendText("C:/Users/usama/source/repos/DataCapture/DataCapture/LoginData/login.txt"))
            {
                w.WriteLine(Line + username + "|" + pass);
            }
            MessageBox.Show("Successfully registered");
            this.Close();

        }
    }
}
