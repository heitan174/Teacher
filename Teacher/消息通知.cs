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
    public partial class 消息通知 : Form
    {
        public string time;
        public string message;
        public string name;
        public 消息通知(Model.Message m_user)
        {
            InitializeComponent();
            time = m_user.Time;
            message = m_user.Text;
            name = m_user.Sender;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 消息通知_Load(object sender, EventArgs e)
        {
            label3.Text = time;
            textBox1.Text = message;
            label4.Text = name;
        }
    }
}
