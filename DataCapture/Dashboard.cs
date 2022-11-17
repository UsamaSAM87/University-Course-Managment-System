using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace DataCapture
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-RL25L66\SQLEXPRESS;Initial Catalog=StudentModule;Integrated Security=True");
        String imageloc = "";
        public int studentID;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Students", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                imageloc = opnfd.FileName;
                pictureBox1.ImageLocation = imageloc;
            }
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (isvalid())
                {
                    byte[] images = null;
                    FileStream fstream = new FileStream(imageloc, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(fstream);
                    images = brs.ReadBytes((int)fstream.Length);

                    SqlCommand cmd = new SqlCommand("insert into Students values (@fname, @sname, @gender, @phone, @address, @dob, @image, @modules)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@fname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@sname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@gender", textBox5.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox3.Text);
                    cmd.Parameters.AddWithValue("@address", textBox4.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value);
                    cmd.Parameters.Add(new SqlParameter("@image", images));
                    cmd.Parameters.AddWithValue("@modules", textBox6.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ResetAll();
                    MessageBox.Show("Successfully Added");

                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Enter Details of Student", "select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private bool isvalid()
        {
            if (textBox1.Text=="" || textBox2.Text=="" ||textBox3.Text=="" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
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
            studentID = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            pictureBox1.Image = null;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            studentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[6].Value);
            byte[] img = (byte[])dataGridView1.SelectedRows[0].Cells[7].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (studentID > 0)
                {
                    byte[] images = null;
                    FileStream fstream = new FileStream(imageloc, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(fstream);
                    images = brs.ReadBytes((int)fstream.Length);

                    SqlCommand cmd = new SqlCommand("update Students set FirstName=@fname, Surname=@sname, Gender=@gender, Phone=@phone, Address=@address, DOB=@dob, Image=@image, Modules=@modules where StudentID=@id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@fname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@sname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@gender", textBox5.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox3.Text);
                    cmd.Parameters.AddWithValue("@address", textBox4.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value);
                    cmd.Parameters.Add(new SqlParameter("@image", images));
                    cmd.Parameters.AddWithValue("@modules", textBox6.Text);
                    cmd.Parameters.AddWithValue("@id", this.studentID);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully");
                    ResetAll();
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Select a student to update", "select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (studentID > 0)
                {
                    SqlCommand cmd = new SqlCommand("delete from Students where StudentID=@id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", this.studentID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully deleted");
                    ResetAll();
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Select a student to delete", "select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
  
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            SearchStudent searchstd = new SearchStudent();
            searchstd.Show();
        }

        private void GoModules_Click(object sender, EventArgs e)
        {
            ModulesDashboard md = new ModulesDashboard();
            md.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login lp = new Login();
            lp.Show();
        }
    }
}
