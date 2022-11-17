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
    public partial class SearchModule : Form
    {
        public SearchModule()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-RL25L66\SQLEXPRESS;Initial Catalog=StudentModule;Integrated Security=True");
        public int moduleID;

        private void button1_Click(object sender, EventArgs e)
        {
            string cmd = "select * from Modules where ModulesCode=@id";
            SqlDataAdapter sdr = new SqlDataAdapter(cmd, con);
            int mdID = Convert.ToInt32(textBox1.Text);
            sdr.SelectCommand.Parameters.AddWithValue("@id", mdID);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
