using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataCapture
{
    public partial class SearchStudent : Form
    {
        public SearchStudent()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-RL25L66\SQLEXPRESS;Initial Catalog=StudentModule;Integrated Security=True");
        public int studentID;

        private void button1_Click(object sender, EventArgs e)
        {
            string cmd = "select * from Students where StudentID=@id";
            SqlDataAdapter sdr = new SqlDataAdapter(cmd, con);
            
            int sdID = Convert.ToInt32(textBox1.Text);
            sdr.SelectCommand.Parameters.AddWithValue("@id", sdID);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
