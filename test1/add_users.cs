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
    public partial class add_users : Form
    {
        public add_users()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                string gender = radioButton1.Checked ? "男" : "女";
                string sql = "insert into users(user_name,mail,password,sex,role) values('" + textBoxname.Text + "','" + textBoxpname.Text + "','" + textBoxpasswd.Text + "','" + gender + "','"+"用户"+"')";
                Console.WriteLine(sql);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show("添加用户成功！");
                Form3 mainframe = new Form3();
                mainframe.BringToFront();
                mainframe.Show();
                this.Hide();
            }
            catch(Exception error)
            {
                MessageBox.Show("已经存在的用户账号！" + "具体信息：\n" + error.Message);
                //MessageBox.Show("已经存在该用户账号！");

            }
            finally
            {
                conn.Close();
            }
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //     SqlConnection conn = new SqlConnection(loginForm.connectionString);
        //            conn.Open();
        //            string sql = "select * from users where mail='" + textBoxpname.Text.Trim() + "'";
        //            SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
        //            DataSet ds = new DataSet();
        //            adp.Fill(ds);
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                MessageBox.Show("已经存在的用户账号！");
        //            }

        //            else
        //            {
        //                SqlCommand cmd = new SqlCommand();
        //                cmd.Connection = conn;
        //                string gender = radioButton1.Checked ? "男" : "女";
        //                Console.WriteLine(radioButton1.Checked);
        //                Console.WriteLine(radioButton2.Checked);
        //        sql = "insert into users(user_name,mail,password,sex) values('"+ textBoxname.Text+"','"+ textBoxpname.Text+"','"+ textBoxpasswd+"','"+ gender+"')";
        //                cmd.CommandText = sql;
        //                cmd.ExecuteNonQuery();
        //                MessageBox.Show("添加用户成功！");
        //                    }
        //                    conn.Close();
        //        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Close();
            loginForm mainframe = new loginForm();
            mainframe.BringToFront();
            mainframe.Show();
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxgrade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxname_TextChanged(object sender, EventArgs e)
        {

        }
        }
    }

