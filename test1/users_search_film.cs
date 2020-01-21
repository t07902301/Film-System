using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace test1
{
    public partial class 查询电影 : Form
    {
        int user_id = 0;
        public 查询电影()
        {
            InitializeComponent();
        }



        private void chooseForm_Load(object sender, EventArgs e)
        {
           textBox1.Text=loginForm.getStudent();
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string sql = "select film_id as 电影id,film_name as 电影名称,score as 电影评分 from film  ";
            //Console.WriteLine("when loading: "+sql);
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            //下方信息填充
            //textBoxid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            //textBoxclass.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            conn.Close();
        }
        //根据评分或类型查询特定的电影
        private void button6_Click(object sender, EventArgs e)
        {
            while (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.DataSource = null;
            }
            //没有输入任何查询信息
            if (comboBoxterm.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("请输入查询信息！");
            }
            //输入类型
            else if (comboBoxterm.Text != "" && textBox3.Text == "")
            {
                SqlConnection conn = new SqlConnection(loginForm.connectionString);
                conn.Open();
                string sql = "select film_id as 电影id,film_name as 电影名,film_type as 类型,actor as 演员,score as 电影评分,film_type as 电影类型 from film  where film_type = '" + comboBoxterm.SelectedItem.ToString() + "'";
                SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adp1.Fill(ds);
                //载入基本信息
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                conn.Close();
            }
            //输入评分
            else if (textBox3.Text != "" && comboBoxterm.Text == "")
            {
                SqlConnection conn = new SqlConnection(loginForm.connectionString);
                conn.Open();
                //textBox1.Text.Trim()  textBox2.Text.Trim()
                string sql = "select film_id as 电影id,film_name as 电影名,film_type as 类型,actor as 演员,score as 电影评分 from film  where score > " + textBox3.Text ;
                SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adp1.Fill(ds);
                //载入基本信息
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                conn.Close();


            }
            //输入评分和类型
            else if (textBox3.Text != "" && comboBoxterm.Text != "")
            {

                SqlConnection conn = new SqlConnection(loginForm.connectionString);
                conn.Open();
                //textBox1.Text.Trim()  textBox2.Text.Trim()
                string sql = "select film_id as 电影id,film_name as 电影名,film_type as 类型,actor as 演员 ,score as 电影评分 from film  where film_type='" + comboBoxterm.SelectedItem.ToString() + "' and score>"+ textBox3.Text;
                SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adp1.Fill(ds);
                //载入基本信息
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                conn.Close();

            }



        }
        //获取 tag 并判断 相应限制
        private void button5_Click(object sender,EventArgs e)
        {
            string choosed_tag="0";
            //01
            if (radioButton1.Checked)
            {
                Console.WriteLine("radio 1 checked");
  
                choosed_tag = "1";
            }
            //11
            else if (radioButton2.Checked)
            {
                Console.WriteLine("radio 2 checked");

                choosed_tag = "2";
            }
            //10
            else if (radioButton3.Checked)
            {
                Console.WriteLine("radio 3 checked");

                choosed_tag = "3";
            }
            //00
            else if (radioButton4.Checked)
            {
                Console.WriteLine("radio 4 checked");

                choosed_tag = "4";
            }
            Console.WriteLine("choosed_tag is " + choosed_tag);
            string mail = textBox1.Text;
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string sql = "select user_id from users where mail = '" + mail + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            String id1 = cmd.ExecuteScalar().ToString();
            int.TryParse(id1, out user_id);
            //得到user_id
            int film_id = 0;
            int.TryParse(textBoxid.Text, out film_id);
            //得到电影的id
            Console.WriteLine("users: "+user_id + " film: " + film_id);

            //查询是否已有标签
            sql = "select tagValue from tag where user_id = " + user_id + " and film_id="+ film_id;
            SqlCommand objCMD = new SqlCommand(sql, conn);
            //object objResult_tmp = objCMD.ExecuteScalar();
            Console.WriteLine("查询语句：" + sql);
            SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            int tagValue;
            int.TryParse(choosed_tag, out tagValue);
            int flag = 0;
            if (ds.Tables[0].Rows.Count <= 0)
            {
               
                sql = "insert into tag (user_id,film_id,tagValue) values (" + user_id + "," + film_id + "," + tagValue + ")";
            }
            else
            {
                object objResult_tmp = objCMD.ExecuteScalar();
                int tmp=Convert.ToInt32(objResult_tmp);

                Console.WriteLine("查询标签结果：" + tmp);
                if (tmp == 2 || tmp == 3)
                {
                    if (choosed_tag == "1" || choosed_tag == "4")
                    {
                        flag = 1;
                        MessageBox.Show("您已看过不可修改了！");

                    }
                    else
                    {
                        sql = "update tag set tagValue=" + tagValue + " where user_id = " + user_id + " and film_id =" + film_id;
                        MessageBox.Show("标签修改成功");

                    }
                }
                else//未看过
                {
                    sql = "update tag set tagValue=" + tagValue + " where user_id = " + user_id + " and film_id =" + film_id;
                    MessageBox.Show("标签修改成功");

                }
            }
            Debug.WriteLine("查询之后需要进行的sql语句为 " + sql);
            if (flag == 0)//可以操作
            {
                SqlCommand tagCMD = new SqlCommand(sql, conn);
                //tagCMD.ExecuteScalar();
                if (tagCMD.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("tag操作成功！");
                }
            }

            //保留
            //sql = "select film.film_name as 电影名 , film.film_id as 电影ID from tag,film where tag.film_id = film.film_id and user_id=" + user_id + " and (tagValue=2 or tagValue=3) and tag.film_id=" + film_id;
            //SqlConnection conn = new SqlConnection(loginForm.connectionString);
            //conn.Open();
            //string sql = " select  comment as 评论 from comment where film_id= "+ film_id;
            //Console.WriteLine("显示已看列表的查询" + sql);
            //SqlDataAdapter adp2 = new SqlDataAdapter(sql, conn);
            //DataSet ds2 = new DataSet();
            //adp2.Fill(ds2);
            //dataGridView2.DataSource = ds2.Tables[0].DefaultView;
            //conn.Close();



            //if (ds2.Tables[0].Rows.Count == 0) { MessageBox.Show("用户未看过任何电影"); }
            //else
            //{
            //    foreach (DataRow row in ds2.Tables[0].Rows)
            //    {
            //        listBox1.Items.Add(row[0].ToString());
            //    }
            //}
            //SqlCommand tagRecordCMD = new SqlCommand(sql, conn);
            //tagCMD.ExecuteScalar();
            //if (tagRecordCMD.ExecuteNonQuery()<0)
            //{
            //    MessageBox.Show("用户未看过任何电影");
            //}
            //else {
            //    SqlDataAdapter adp2 = new SqlDataAdapter(sql, conn);
            //    DataSet ds2 = new DataSet();
            //    adp2.Fill(ds2);
            //    if (ds2.Tables[0].Rows.Count == 0) { MessageBox.Show("用户未看过任何电影"); }
            //    else
            //    {
            //        foreach (DataRow row in ds2.Tables[0].Rows)
            //        {
            //            listBox1.Items.Add(row[0].ToString());
            //        }
            //    }
            //}


        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string flags = "1";
        //    //得到user_id
        //    string mail = textBox1.Text;
        //    SqlConnection conn = new SqlConnection(loginForm.connectionString);
        //    conn.Open();
        //    string sql = "select user_id from users where mail = '" + mail + "'";
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    String id1 = cmd.ExecuteScalar().ToString();
        //    Console.WriteLine(sql);
        //    int.TryParse(id1, out user_id);
        //    //得到电影的id
        //    int film_id = 0;
        //    int.TryParse(textBoxid.Text, out film_id);
        //    //查询你在该时间是否有课
        //    sql = "select sctime from sctime where film_id =" + film_id;
        //    SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        DataRow dr = ds.Tables[0].Rows[i];
        //        //time=sctime
        //        string time = dr[0].ToString();//第一列
        //        sql = "select * from comment,sctime,film where film.film_id = comment.film_id and film.film_id = sctime.film_id and sctime = '" + time + "' and comment.user_id =" + user_id;
        //        SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
        //        DataSet ds1 = new DataSet();
        //        adp1.Fill(ds1);
        //        //对应时间表上有安排则不能选择其他电影

        //        if (ds1.Tables[0].Rows.Count > 0)
        //        {
        //            flags = "2";
        //            MessageBox.Show("电影上课时间冲突！");
        //            break;
        //        }
        //    }
        //    if (flags == "1")
        //    {
        //        sql = "insert into comment(film_id,user_id) values(" + film_id + "," + user_id + ")";
        //        cmd.CommandText = sql;
        //        Console.WriteLine(sql);

        //        if (cmd.ExecuteNonQuery() > 0)
        //        {
        //            MessageBox.Show("选课成功！");

        //        }

        //    }
        //    if (listBox1.Items.Count > 0)
        //    {//清空所有项
        //        listBox1.Items.Clear();
        //    }
        //    sql = "select film.film_name  from comment,film where comment.film_id = film.film_id and user_id=" + user_id;
        //    SqlDataAdapter adp2 = new SqlDataAdapter(sql, conn);
        //    DataSet ds2 = new DataSet();
        //    adp2.Fill(ds2);
        //    foreach (DataRow row in ds2.Tables[0].Rows)
        //    {
        //        listBox1.Items.Add(row[0].ToString());
        //    }            
        //  conn.Close();
        //}

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    while (dataGridView1.Rows.Count != 0)
        //    {
        //        dataGridView1.DataSource = null;
        //    }
        //    string film_type = comboBox1.SelectedItem.ToString();
        //    Console.WriteLine(film_type);
        //    SqlConnection conn = new SqlConnection(loginForm.connectionString);
        //    conn.Open();
        //    //string sql = "select film_id as 电影id,film_name as 电影 from film where film_type='" + film_type + "'";
        //    string sql = "select film_id as 电影id,film_name as 电影 from film  ";
        //    SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
        //    DataSet ds = new DataSet();
        //    adp1.Fill(ds);
        //    Console.WriteLine(sql);

        //    //载入基本信息
        //    dataGridView1.DataSource = ds.Tables[0].DefaultView;
        //    conn.Close();
        //}

        //private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        //{

        //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mos_click(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBoxclass.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            int film_id = 0;
            int.TryParse(textBoxid.Text, out film_id);
            //Console.WriteLine(textBoxid.Text);
            //Console.WriteLine(textBoxclass.Text);
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string sql = " select  comment as 评论,user_name as 评论者 from comment,users where users.user_id=comment.user_id and film_id=" + film_id;
            Console.WriteLine("显示已看列表的查询" + sql);
            SqlDataAdapter adp2 = new SqlDataAdapter(sql, conn);
            DataSet ds2 = new DataSet();
            adp2.Fill(ds2);
            dataGridView2.DataSource = ds2.Tables[0].DefaultView;
            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //     string film_name = listBox1.SelectedItem.ToString();
        //     SqlConnection conn = new SqlConnection(loginForm.connectionString);
        //     conn.Open();
        //     string sql = "select film_id from film where film_name = '"+film_name+"'";
        //     SqlCommand cmd = new SqlCommand(sql, conn);
        //     String id1 = cmd.ExecuteScalar().ToString();
        //     int film_id = 0;
        //     int.TryParse(id1, out film_id);
        //     sql = "delete from  comment  where  film_id = " + film_id+" and user_id = "+user_id;
        //     cmd.CommandText = sql;
        //     if (cmd.ExecuteNonQuery() > 0)
        //     { 
        //         MessageBox.Show("删除成功！");
        //         if (listBox1.Items.Count > 0)
        //         {//清空所有项
        //             listBox1.Items.Clear();
        //         }
        //         sql = "select film.film_name  from comment,film where comment.film_id = film.film_id and user_id=" + user_id;
        //         SqlDataAdapter adp2 = new SqlDataAdapter(sql, conn);
        //         DataSet ds2 = new DataSet();
        //         adp2.Fill(ds2);
        //         foreach (DataRow row in ds2.Tables[0].Rows)
        //         {
        //             listBox1.Items.Add(row[0].ToString());
        //         }
                 
        //     }

        //     conn.Close();

        //}

        //private void getKecheng()
        //{
        //    SqlConnection conn = new SqlConnection(loginForm.connectionString);
        //    conn.Open();
        //    string sql = "select film.film_name  from comment,film where comment.film_id = film.film_id and user_id=" + user_id;
        //    SqlDataAdapter adp2 = new SqlDataAdapter(sql, conn);
        //    DataSet ds2 = new DataSet();
        //    adp2.Fill(ds2);
        //    foreach (DataRow row in ds2.Tables[0].Rows)
        //    {
        //        listBox1.Items.Add(row[0].ToString());
        //    }
        //    conn.Close();
        //}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            //textBox1.Text.Trim()  textBox2.Text.Trim()
            string sql = "select film_id as 电影id,film_name as 电影名,film_type as 类型,actor as 演员 from film  where film_name ='" + textBox2.Text+"'";
            Console.WriteLine(sql);
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string keyWord = textBox4.Text.Trim();
            Console.WriteLine(keyWord);
            string sql = "select film_id as 电影id,film_name as 电影名,film_type as 类型,actor as 演员 from film ";
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            int i;
            for (i=0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1].ToString().IndexOf(keyWord) <0)
                {
                    //ds.Tables[0].Rows.Remove(ds.Tables[0].Rows[i]);
                    ds.Tables[0].Rows[i].Delete();
                }
            }
            ds.Tables[0].AcceptChanges();
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(loginForm.connectionString);
            conn.Open();
            string keyWord = textBox4.Text.Trim();
            Console.WriteLine(keyWord);
            string sql = "select film_id as 电影id,film_name as 电影名,film_type as 类型,actor as 演员 from film where popularity='hot'";
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp1.Fill(ds);
            //载入基本信息
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }
    }
}
