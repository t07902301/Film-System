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
    public partial class users_not_seen : Form
    {
        public users_not_seen()
        {
            InitializeComponent();
        }

        private void showkebiaoForm_Load(object sender, EventArgs e)
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
            //用到两个数据库的连接操作

            //sql = "select film.film_id as 电影id,film.name as 电影名称,film.teacher as 教师姓名,film.term as 学期,sctime.sctime as 上课时间,sctime.location as 上课地点 from sc,sctime,film where film.film_id=sc.film_id and sc.film_id=sctime.film_id and sc.user_id=" + user_id;
            sql = "select film.film_id as 电影ID,film_name  as 电影名称 from tag,film where tag.film_id=film.film_id and tagValue=1 and tag.user_id=" + user_id;
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxclass.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            string choosed_tag="0";
            //11
            if (radioButton2.Checked)
            {
                choosed_tag = "3";
            }
            //10
            else if (radioButton3.Checked)
            {
                choosed_tag = "2";
            }
            //00
            else if (radioButton4.Checked)
            {
                choosed_tag = "1";
            }
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string mail = loginForm.getStudent();
            int user_id;
            string sql = "select user_id from users where mail = '" + mail + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            String id1 = cmd.ExecuteScalar().ToString();
            int.TryParse(id1, out user_id);
            sql = "update tag set tagValue=" + choosed_tag + " where user_id = " + user_id + " and film_id =" + textBoxclass.Text;
            SqlCommand tagCMD = new SqlCommand(sql, conn);
            Console.WriteLine(sql);
            tagCMD.ExecuteScalar();
            if (tagCMD.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("tag操作成功！");
                this.getRusult();
            }
        }
        private void getRusult()
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            //textBox1.Text.Trim()  textBox2.Text.Trim()
            //string sql = "select stuid as 用户id,stuname as 真实姓名,stuxuehao as 学号,stupasswd as 密码,stugrade as 年级,stumajor as 专业,stusex as 性别,stuborn as 出生日期,stuhometown as 籍贯 from users";
            string mail = loginForm.getStudent();
            string sql = "select film.film_id as 电影ID,film_name  as 电影名称 from tag,film,users where tag.film_id=film.film_id and tagValue=1 and tag.user_id=users.user_id and mail='" + mail + "'";
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
