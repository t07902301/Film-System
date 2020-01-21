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
    public partial class user_comment : Form
    {
        public user_comment()
        {

            InitializeComponent();
        }
        private void getRusult()
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string mail = loginForm.getStudent();
            string sql = "select user_id from users where mail = '" + mail + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            String id1 = cmd.ExecuteScalar().ToString();
            int user_id = 0;
            int.TryParse(id1, out user_id);
            //用到两个数据库的连接操作
            //只有看过的电影才可以显示以及对其评论
            sql = "select film.film_id as 电影ID,film.film_name as 电影名称,film.film_type as 类型,film.score as 总评分 from tag,film where film.film_id=tag.film_id and (tagValue=2 or tagValue=3) and tag.user_id=" + user_id;
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0)
            {
                //清空
                textBoxgrade.Text = null;
                textBox3.Text = null;
                //首先得到用户的id
                string mail = loginForm.getStudent();
                SqlConnection conn = new SqlConnection(loginForm.connectionString);
                conn.Open();
                string sql = "select user_id from users where mail = '" + mail + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                String id1 = cmd.ExecuteScalar().ToString();
                int user_id = 0;
                int.TryParse(id1, out user_id);
                //得到电影ID
                string film_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = film_id;
                //textBox1.Text.Trim()  textBox2.Text.Trim()
                //string sql = "select sctime as 上课时间,location as 上课地点 from sctime where film_id="+film_id;
                sql = "select comment,score from comment where user_id="+ user_id+" and film_id=" + film_id;
                Console.WriteLine(sql);
                SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adp1.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                    //载入基本信息
                    textBoxgrade.Text = ds.Tables[0].Rows[0][0].ToString();
                    textBox3.Text = ds.Tables[0].Rows[0][1].ToString();
                }

                
                //cmd = new SqlCommand(sql, conn);
                //if (cmd.ExecuteNonQuery() != -1)
                //{
                //textBoxgrade.Text = cmd.ExecuteScalar().ToString();

                //}


                conn.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //更新
        private void button2_Click(object sender, EventArgs e)
        {
            //首先得到用户的id
            string mail = loginForm.getStudent();
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string sql = "select user_id from users where mail = '" + mail + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            String id1 = cmd.ExecuteScalar().ToString();
            int user_id = 0;
            int.TryParse(id1, out user_id);
            //评论以及分数操作
            //先检查是否存在
            sql = "select * from comment where user_id="+ user_id+" and film_id="+ textBox2.Text;
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //存在即更新
            if (ds.Tables[0].Rows.Count > 0)
            {
                    sql = "update comment set comment='" + textBoxgrade.Text
               + "',score=" + textBox3.Text + " where user_id=" + user_id + " and film_id=" + textBox2.Text;
                    Console.WriteLine(sql);
                    cmd = new SqlCommand(sql, conn);
                //针对用户评分更改
                //原始分数为爬取的评分
                //增加update触发器
                if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("更新成功！");

                    //针对用户评分更改
                    //原始分数为爬取的评分
                    //sql = "update film set score=tb.分数 from(select AVG(score) as 分数 from comment group by film_id having film_id=" + textBox2.Text + ") as tb where film_id=" + textBox2.Text;
                    //    cmd = new SqlCommand(sql, conn);
                    //    //Console.WriteLine(sql);
                    //    if (cmd.ExecuteNonQuery() > 0)
                    //    {
                    //        MessageBox.Show("更新成功！");
                    //        this.getRusult();
                    //    }
                    }
            }
            //若不存在则插入
            else
            {

                    sql = "insert into comment(user_id,film_id,score,comment) values(" + user_id + "," + textBox2.Text + "," + textBox3.Text + ",'" + textBoxgrade.Text + "')";
                    cmd = new SqlCommand(sql, conn);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("更新成功");
                        this.getRusult();
                    }
                    else
                    {
                        MessageBox.Show("更新失败,请再试一遍");
                    }
            }

            this.getRusult();
               

            conn.Close();
        }
        //选定类型进行查询
        private void button1_Click(object sender, EventArgs e)
        {
            //string film_type = comboBox1.SelectedItem.ToString();
            //首先得到用户的id
            string mail = loginForm.getStudent();
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string sql = "select user_id from users where mail = '" + mail + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            String id1 = cmd.ExecuteScalar().ToString();
            int user_id = 0;
            int.TryParse(id1, out user_id);
            //用到两个数据库的连接操作
            sql = "select film.film_id as 电影ID,film.film_name as 电影名称,film.film_type as 类型,film.score as 总评分 from tag,film where film.film_id=tag.film_id and (tagValue=2 or tagValue=3) and tag.user_id="+user_id+" and film_type='"+ comboBox1.SelectedItem.ToString()+"'" ;
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();




        }

        private void groupbox5_Enter(object sender, EventArgs e)
        {

        }

        private void searchgradeForm_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string mail = loginForm.getStudent();
            string sql = "select user_id from users where mail = '" + mail + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            String id1 = cmd.ExecuteScalar().ToString();
            int user_id = 0;
            int.TryParse(id1, out user_id);
            //用到两个数据库的连接操作
            sql = "select film.film_id as 电影ID,film.film_name as 电影名称,film.film_type as 类型,film.score as 总评分 from tag,film where film.film_id=tag.film_id and (tagValue=2 or tagValue=3) and tag.user_id=" + user_id;
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
