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
    public partial class 队伍信息 : Form
    {
        Model.Team m_team = new Model.Team();
        public 队伍信息(string a)
        {
            InitializeComponent();
            m_team.Tname = a;
        }
        
        

        private void 队伍信息_Load(object sender, EventArgs e)
        {
            BLL.User m_user = new BLL.User();
            m_team = m_user.teamInformation(m_team);
            label6.Text = m_team.Tname;
            label7.Text = m_team.Project;
            label8.Text = m_team.TeamLeader;
            label9.Text = m_team.member1;
            label10.Text = m_team.member2;
            label11.Text = m_team.member3;
            textBox1.Text = m_team.ProjectProfile;

        }

    }
}
