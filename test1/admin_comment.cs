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
    public partial class admin_comment : Form
    {
        public admin_comment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.DataSource = null;
            }
            if (comboBoxterm.Text == "" && textBoxclass.Text == "")
            {
                MessageBox.Show("请输入查询信息！");
            }
            else if (comboBoxterm.Text != "" && textBoxclass.Text == "")
            {
                SqlConnection conn = new SqlConnection(loginForm.connectionString);
                conn.Open();
                string sql = "select film_id as 电影id,film_name as 电影名,film_type as 类型,actor as 演员 from film where film_type = '" + comboBoxterm.SelectedItem.ToString() + "'";
                SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adp1.Fill(ds);
                //载入基本信息
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                conn.Close();
            }
            else if (textBoxclass.Text != "" && comboBoxterm.Text == "")
            {
                
                SqlConnection conn = new SqlConnection(loginForm.connectionString);
                conn.Open();
                //textBox1.Text.Trim()  textBox2.Text.Trim()
                string sql = "select film_id as 电影id,film_name as 电影名,film_type as 类型,actor as 演员 from film  where film_name = '" + textBoxclass.Text + "'";
                SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adp1.Fill(ds);
                //载入基本信息
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                conn.Close();


            }
            else if (textBoxclass.Text != "" && comboBoxterm.Text != "")
            {
               
                SqlConnection conn = new SqlConnection(loginForm.connectionString);
                conn.Open();
                //textBox1.Text.Trim()  textBox2.Text.Trim()
                string sql = "select film_id as 电影id,film_name as 电影名,film_type as 类型,actor as 演员 from film  where film_name = '" + textBoxclass.Text + "'and film_type ='" + comboBoxterm.SelectedItem.ToString() + "'";
                SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adp1.Fill(ds);
                //载入基本信息
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                conn.Close();

            }



        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0)
            {
                string film_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //Console.WriteLine("draw1开始取值");
                //Console.WriteLine(film_id);
                SqlConnection conn = new SqlConnection(loginForm.connectionString);
                conn.Open();
                //textBox1.Text.Trim()  textBox2.Text.Trim()
                //string sql = "select commenttime as 上课时间,location as 上课地点 from commenttime where film_id="+film_id;
                string sql = "select comment.film_id as 电影ID, film.film_name as 电影名称,comment.user_id as 用户ID,users.user_name as 用户名字,comment as 评论 from comment, users,film where comment.user_id=users.user_id and comment.film_id=film.film_id  and comment is not null and comment.film_id=" + film_id;
                Console.WriteLine(sql);
                SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adp1.Fill(ds);
                //载入基本信息
                dataGridView2.DataSource = ds.Tables[0].DefaultView;
                conn.Close();
            }
        }
        private void getRusult(string film_id)
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            //textBox1.Text.Trim()  textBox2.Text.Trim()
            //string sql = "select user_id as 用户id,name as 真实姓名,stuxuehao as 学号,stupasswd as 密码,stugrade as 年级,stumajor as 专业,stusex as 性别,stuborn as 出生日期,stuhometown as 籍贯 from users";
            string sql = "select comment.film_id as 电影ID, film.film_name as 电影名称,comment.user_id as 用户ID,users.user_name as 用户名字,comment as 评论 from comment, users,film where comment.user_id=users.user_id and comment.film_id=film.film_id  and comment is not null and comment.film_id=" + film_id;
            Console.WriteLine(sql);
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }
        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.SelectedCells.Count != 0)
            {
                //get the film ID
                textBoxid.Text= dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                //get the users ID
                textBox1.Text= dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                //Console.WriteLine(textBoxid.Text);
                //Console.WriteLine(textBox1.Text);
                //SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
                //DataSet ds = new DataSet();
                //adp1.Fill(ds);
                ////载入基本信息
                ////dataGridView2.DataSource = ds.Tables[0].DefaultView;
                //conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////get the film ID
            //int film_id;
            //string film_id_str = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            //int.TryParse(film_id_str, out film_id);
            ////get the users ID
            //int user_id;
            //string user_id_str = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            //int.TryParse(user_id_str, out user_id);
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            //textBox1.Text.Trim()  textBox2.Text.Trim()
            //string sql = "select commenttime as 上课时间,location as 上课地点 from commenttime where film_id="+film_id;
            //string sql = "select name as 电影名称,users.name as 用户名字,comment as 评论 from comment, users,film where comment.user_id=users.user_id and comment.film_id=film.film_id  and comment is not null and comment.film_id=" + film_id;
            string sql = "delete from comment where film_id=" + textBoxid.Text + " and user_id =" + textBox1.Text;
            Console.WriteLine(sql);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("删除成功！");
                this.getRusult(textBoxid.Text);
            }

            conn.Close();
        }
    }
}
