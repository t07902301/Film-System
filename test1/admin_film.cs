using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace test1
{
    public partial class modifystuForm : Form
    {
        public modifystuForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void modifystuForm_Load(object sender, EventArgs e)
        {
            this.getRusult();
        }


        private void getRusult()
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            //textBox1.Text.Trim()  textBox2.Text.Trim()
            //string sql = "select stuid as 用户id,stuname as 真实姓名,stuxuehao as 学号,stupasswd as 密码,stugrade as 年级,stumajor as 专业,stusex as 性别,stuborn as 出生日期,stuhometown as 籍贯 from users";
            string sql = " select  film_id as 电影,film_name as 电影名称,actor as 演员,score as 评分,show_time as 上映时间,film_type as 类型 from film";
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }

        private void mos_click(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxname.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();//actor
            textBoxborn.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//film_name
            comboBoxgrade.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();//film_type
            textBoxhometown.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();//show_time
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();//score
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//ID
            //this.comboBoxrole.SelectedItem.ToString()
            //string grade = comboBoxgrade.SelectedItem.ToString();
            

        }

        private void textBoxname_TextChanged(object sender, EventArgs e)
        {

        }
        //修改
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            int id = 0;
            int.TryParse(textBox1.Text, out id);
            string sql = "update film set film_name='" + textBoxborn.Text + "',actor='" + textBoxname.Text + "',score=" + textBox2.Text + ",show_time='" + textBoxhometown.Text + "',film_type='" + comboBoxgrade.SelectedItem.ToString() + "' where film_id=" + textBox1.Text;
            //Console.WriteLine(sql);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("更改成功！");
                this.getRusult();
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            int id = 0;
            int.TryParse(textBox1.Text, out id);
            string sql = "delete from  film  where  film_id = " + id;
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("删除成功！");
                this.getRusult();
            }
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            try
            {
                int id = 0;
                int.TryParse(textBox1.Text, out id);
                string sql = "insert into film(film_name,actor,score,show_time,film_type) values('" + textBoxborn.Text + "','" + textBoxname.Text + "',5 ,'" + textBoxhometown.Text + "','" + comboBoxgrade.SelectedItem.ToString() + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("添加成功！");
                    this.getRusult();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("由于该电影已存在，导致插入异常");
            }
            finally
            {
                conn.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

  