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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int lineNumber = 0;
                var lineCount = File.ReadLines("C:/Users/usama/source/repos/DataCapture/DataCapture/LoginData/login.txt").Count();
                Console.WriteLine(lineCount);
                while (lineNumber < lineCount)
                {
                    string username, password = "\0", id = "\0";
                    TextReader tr;
                    char[] array1 = id.ToCharArray();
                    array1 = new char[5];
                    char[] array2 = password.ToCharArray();
                    array2 = new char[3];
                    string dummy = "\0";
                    string dummy2 = "\0";
                    using (tr = new StreamReader("C:/Users/usama/source/repos/DataCapture/DataCapture/LoginData/login.txt"))
                    {
                        username = File.ReadLines("C:/Users/usama/source/repos/DataCapture/DataCapture/LoginData/login.txt").Skip(lineNumber).Take(1).First();

                        Console.WriteLine(username);
                        int size = username.Length;
                        for (int i = 0; i < size; i++)
                        {
                            if (username[i] == '|')
                            {
                                int k = 0;
                                int d = i;
                                dummy = username.Substring(0, d);
                                dummy2 = username.Substring(d + 1);
                            }
                        }
                    }
                    username = dummy;
                    password = dummy2;
                    if (textBox1.Text == username && textBox2.Text == password)
                    {
                        Dashboard dbd = new Dashboard();
                        dbd.Show();
                        this.Hide();
                    }
                    lineNumber++;
                }
            }
            catch
            {
                MessageBox.Show("Enter ID and Password");
            }

           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Signup sp = new Signup();
            sp.Show();
        }
    }
}
