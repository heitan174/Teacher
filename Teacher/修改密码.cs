using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teacher
{
    public partial class 修改密码 : Form
    {
        public 修改密码()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model.Teacher m_user = new Model.Teacher();
            m_user.Id = textBox1.Text;
            m_user.Password = textBox2.Text;

            BLL.User b_user = new BLL.User();
            int b_res = b_user.Login(m_user);

            if (b_res == 0)
            {
                m_user.Id = textBox1.Text;
                m_user.Password = textBox4.Text;

                int b_res1 = b_user.modifyPassword(m_user);

                if (b_res1 == 0)
                {
                    MessageBox.Show("修改成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("修改失败！");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
            }
        }
    }
}
