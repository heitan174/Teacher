using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class User
    {
        public int win_switch()
        {
            return 0;
        }
        public int Login(Model.Teacher m_user)
        {
            //判断程序是否超过指定日期
            DateTime dt = DateTime.ParseExact("20230302102000", "yyyyMMddhhmmss", System.Globalization.CultureInfo.CurrentCulture);
            if (DateTime.Now > dt)
            {
                return 3;
            }

            DAL.User d_user = new DAL.User();

            int d_res = d_user.Login(m_user);
            int b_res;

            if (d_res == 0)
            {
                b_res = 0;
            }
            else if (d_res == 1)
            {
                b_res = 1;
            }
            else
            {
                b_res = 2;
            }
            return b_res;
        }
        public DataTable Check()//通过专业选择查看队伍
        {
            DAL.User t_user = new DAL.User();
            var c_res = t_user.Check();
            return c_res;
        }

        public int modifyPassword(Model.Teacher m_user)//修改密码
        {
            DAL.User d_user = new DAL.User();

            int d_res = d_user.modifyPassword(m_user);
            int b_res;

            if (d_res == 0)
            {
                b_res = 0;
            }
            else
            {
                b_res = 1;
            }
            return b_res;
        }
        public DataTable selectTeam(DataTable d1,DataTable d2, int index)//浏览小组选择队伍
        {
            DataRow dr = d2.NewRow();
            dr.ItemArray = d1.Rows[index].ItemArray;

            foreach(DataRow item in d2.Rows)
            {
                if(d1.Rows[index]["tno"].ToString() == item[0].ToString())
                {
                    Model.login.repeat = true;
                }
            }
            if(Model.login.repeat == false)
            {
                d2.Rows.Add(dr.ItemArray);
            }
            else
            {

            }            
            return d2;

        }

        public int modifyPersonalInformation(Model.Teacher m_user)//导师修改个人信息
        {
            DAL.User d_user = new DAL.User();
            int b_res = 0;
            int d_res = 0;//dal层返回的修改完行数
            try
            {
                d_res = d_user.modifyPersonalInformation(m_user);
            }
            catch(SqlException ex)
            {
                int error_code = ex.ErrorCode;
                throw new TOOLS.AddException(string.Format("添加失败！sql 错误代码{0}", error_code));

            }
            catch(Exception e)
            {
                throw e;
            }
            
            if (d_res == 0)//判断修改是否成功
            {
                b_res = 0;
            }
            else//d_res不为0基本就为1，代表修改了一行，即修改成功
            {
                b_res = 1;
            }
            return b_res;
        }
        public int checkName(Model.Message m_user)//检验发消息的人是谁
        {
            DAL.User d_user = new DAL.User();

            int d_res = d_user.checkName(m_user);
            int b_res;

            if (d_res == 0)
            {
                b_res = 0;
            }
            else if (d_res == 1)
            {
                b_res = 1;
            }
            else
            {
                b_res = 2;
            }
            return b_res;
        }
        public int SaveTeamInformation()
        {
            DAL.User m_user = new DAL.User();
            int res = 0;
            try
            {
                m_user.saveTeamInformation();
            }
            catch (SqlException ex)
            {
                int error_code = ex.ErrorCode;
                throw new TOOLS.AddException(string.Format("添加失败！sql 错误代码{0}", error_code));

            }
            catch (Exception e)
            {
                throw e;
            }
            return res;
        }
        public Model.Team teamInformation(Model.Team m_team)
        {
            DAL.User m_user = new DAL.User();
            DataTable dt;
            try
            {
                dt = m_user.teamInformation(m_team.Tname);
            }
            catch (SqlException ex)
            {
                int error_code = ex.ErrorCode;
                throw new TOOLS.AddException(string.Format("添加失败！sql 错误代码{0}", error_code));

            }
            catch (Exception e)
            {
                throw e;
            }
            m_team.Tname = dt.Rows[0]["teamname"].ToString().Trim();
            m_team.Project = dt.Rows[0]["project"].ToString().Trim();
            m_team.ProjectProfile = dt.Rows[0]["projectprofile"].ToString().Trim();
            m_team.TeamLeader = dt.Rows[0]["leader"].ToString().Trim();
            m_team.member1 = dt.Rows[0]["member1"].ToString().Trim();
            m_team.member2 = dt.Rows[0]["member2"].ToString().Trim();
            m_team.member3 = dt.Rows[0]["member3"].ToString().Trim();

            return m_team;
        }

    }
}
