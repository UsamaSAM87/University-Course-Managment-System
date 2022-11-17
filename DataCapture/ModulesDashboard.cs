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
    public partial class ModulesDashboard : Form
    {
        public ModulesDashboard()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-RL25L66\SQLEXPRESS;Initial Catalog=StudentModule;Integrated Security=True");
        public int moduleID;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Modules", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (isvalid())
                {
                    SqlCommand cmd = new SqlCommand("insert into Modules values (@mname, @descript, @resource)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@mname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@descript", textBox2.Text);
                    cmd.Parameters.AddWithValue("@resource", textBox3.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully");
                    ResetAll();
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Module Added", "select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isvalid()
        {
            if (textBox1.Text=="" || textBox2.Text == "" || textBox3.Text == "")
            {
                return false;
            }
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void ResetAll()
        {
            moduleID = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            moduleID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (moduleID > 0)
                {
                    SqlCommand cmd = new SqlCommand("update Modules set ModulesName=@mname, ModulesDescription=@descript, OnlineResources=@resource where ModulesCode=@id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@mname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@descript", textBox2.Text);
                    cmd.Parameters.AddWithValue("@resource", textBox3.Text);
                    cmd.Parameters.AddWithValue("@id", this.moduleID);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully performed");
                    ResetAll();
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Select a module to update", "select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (moduleID > 0)
                {
                    SqlCommand cmd = new SqlCommand("delete from Modules where ModulesCode=@id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", this.moduleID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ResetAll();
                    MessageBox.Show("Successfully deleted");
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Select a module to delete", "select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            SearchModule smdl = new SearchModule();
            smdl.Show();
        }
    }
}
