using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
namespace Teacher
{
    public partial class 登陆界面 : Form
    {
        public 登陆界面()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t1 = new System.Threading.Thread(new System.Threading.ThreadStart(() => Application.Run(new 修改密码())));
            t1.Start();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            Model.Teacher m_user = new Model.Teacher();
            m_user.Id = textBox1.Text;
            m_user.Password = textBox2.Text;
            

            BLL.User b_user = new BLL.User();
            int b_res = b_user.Login(m_user);

            if (b_res == 0)
            {
                Model.login.Id = textBox1.Text;
                System.Threading.Thread t1 = new System.Threading.Thread(new System.Threading.ThreadStart(() => Application.Run(new 主界面(m_user))));
                t1.Start();
                this.Close();
            }
            else if (b_res == 1)
            {
                MessageBox.Show("用户名或密码错误！");
            }
            else if (b_res == 2)
            {
                MessageBox.Show("登录失败！");
            }
            else
            {
                MessageBox.Show("已经超过登录日期，不能使用！");
            }
        }
    }
}
