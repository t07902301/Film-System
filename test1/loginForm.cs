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
    public partial class loginForm : Form
    {
        public static string connectionString = "uid=sa;pwd=123456;initial catalog=student;data source=localhost;Connect Timeout=900";
        public static  string name;
        public static string role;
        public static int flag;
        public loginForm()
        {
            

            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBoxname.Text.Trim();
            role = this.comboBoxrole.SelectedItem.ToString();
            if (name == "" || textBoxpasswd.Text.Trim() == "" || role == "")
            {
                MessageBox.Show("请将信息输入完整！");
            }
            else
            {
                SqlConnection conn = new SqlConnection(loginForm.connectionString);
                conn.Open();
                if (role == "管理员")
                {
                    string sql = "select manname,manpasswd from manager where role='管理员'and manname='" + name +
                     "' and manpasswd='" + textBoxpasswd.Text.Trim() + "'";
                    //Console.WriteLine(sql);
                    SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        conn.Close();
                        //登录成功跳转到管理员界面
                        Form1 mainframe = new Form1();
                        mainframe.BringToFront();
                        mainframe.Show();
                        this.Hide();
                        
                    }
                    else
                    {
                        MessageBox.Show("管理员名或者密码错误！");
                    }
                }
                else if (role == "用户")
                {
                    string sql1 = "select mail,password from users where role='用户'and mail='" + name +
                     "' and password='" + textBoxpasswd.Text.Trim() + "'";
                    //Console.WriteLine(sql1);

                    SqlDataAdapter adp = new SqlDataAdapter(sql1, conn);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //this.Close();
                        conn.Close();
                        Form3 mainframe = new Form3();
                        mainframe.BringToFront();
                        mainframe.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误！");
                    }
                }
               
            }
          


        }






        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //引入全局变量flag，判断选择框的点击状态
            if (flag == 0)
            {
                MessageBox.Show("请先选择角色");

            }
            else
            {
                string role=comboBoxrole.SelectedItem.ToString();
                if (role == "管理员")
                {
                    add_manager mainframe = new add_manager();
                    mainframe.BringToFront();
                    mainframe.Show();
                    this.Hide();
                }
                else if (role == "用户")
                {
                    add_users mainframe = new add_users();
                    mainframe.BringToFront();
                    mainframe.Show();
                    this.Hide();
                }
            }
            //name = textBoxname.Text.Trim();
            //string role;

            //if (role == null)
            //{
            //    MessageBox.Show("请先选择角色");
            //}
            //else
            //{
            //    if (role == "管理员")
            //    {
            //        addmanForm mainframe = new addmanForm();
            //        mainframe.BringToFront();
            //        mainframe.Show();
            //        this.Hide();
            //    }
            //    else if (role == "用户")
            //    {
            //        addstuForm mainframe = new addstuForm();
            //        mainframe.BringToFront();
            //        mainframe.Show();
            //        this.Hide();
            //    }
            //}

        }


        private void loginForm_Load(object sender, EventArgs e)
        {
            flag = 0;
            
        }

        private void comboBoxrole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public static  String getStudent()
        {
            String mail = "";
            mail = loginForm.name;
            return mail;
        }

        public static String getRole()
        {
            String role1 = "";
            role1 = role;
            return role1;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        //private void button2_Click_1(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxrole_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (comboBoxrole.SelectedItem.ToString()) //获取选择的内容
            {

                case "管理员":flag=1; break;

                case "用户": flag=1; break;

                //case "USB3": MessageBox.Show("C"); break;
                default:; break;

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
