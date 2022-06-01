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
    public partial class 队伍志愿通知 : Form
    {
        public string uid;
        public string time;
        public string message;
        public string msender;
        public string receiver;
        public string teamname;
        public 队伍志愿通知(Model.Message m_user)
        {
            InitializeComponent();
            time = m_user.Time;
            message = m_user.Text;
            msender = m_user.Sender;//发件人
            receiver = Model.login.Name;//收件人
            //teamname = m_user.Name;
        }

        private void 队伍志愿通知_Load(object sender, EventArgs e)
        {
            DAL.User m_user = new DAL.User();
            string sno = m_user.noChangename(msender);
            DataTable dt = m_user.senderTeam(sno);

            textBox2.Text = string.Format("尊敬的{0}导师您好，我们队伍已经填报了您的志愿，\n以下是我们的队伍信息，希望你能进一步了解一下我们",Model.login.Name);
            textBox1.Text = dt.Rows[0]["projectprofile"].ToString().Trim();
            label4.Text = dt.Rows[0]["teamname"].ToString().Trim();
            label5.Text = dt.Rows[0]["project"].ToString().Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
