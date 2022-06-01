using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace DAL
{
    public class User
    {
        public int Login(Model.Teacher m_user)//登录
        {
            
            try
            {
                string sql = "select * from [6_4_teacher] where tno =@id and password =@password";
                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@id", m_user.Id);
                parameter[1] = new SqlParameter("@password", m_user.Password);

                DataTable dt = SqlDbHelper.ExecuteDataTable(sql, CommandType.Text, parameter);     

                if (dt.Rows.Count > 0)//如果存在用户名和密码正确数据执行进入系统操作
                {
                    Model.login.Specialty = dt.Rows[0]["specialty"].ToString();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                return 2;
            }
            finally
            {

            }
        }
        public DataTable Check()//通过专业查看队伍
        {
            
            DataTable dt;
            string sql = "select teamname , leader , project, projectprofile from [6_4_team] where specialty = @specialty";
            SqlParameter[] specialty = new SqlParameter[1];
            specialty[0] = new SqlParameter("@specialty", Model.login.Specialty);
            dt = SqlDbHelper.ExecuteDataTable(sql, CommandType.Text, specialty);

            return dt;
 
            //string ConnString = @"Server = LAPTOP-RKD5N2CU; Initial Catalog = Student_system; integrated security=SSPI";
            
            
        }       


        public void PersonInformation(Model.Teacher b_tea,string id)//个人信息
        {
            DataTable dt;
            List<string> list = new List<string>();

            string sql = "select * from [6_4_teacher] where tno = @tno";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@tno", id);
            dt = SqlDbHelper.ExecuteDataTable(sql, CommandType.Text, parameter);
            b_tea.Id = dt.Rows[0]["tno"].ToString().Trim();
            b_tea.Name = dt.Rows[0]["tname"].ToString().Trim();
            b_tea.Specialty = dt.Rows[0]["specialty"].ToString().Trim();
            b_tea.Email = dt.Rows[0]["email"].ToString().Trim();
            b_tea.Age = dt.Rows[0]["age"].ToString().Trim();
            b_tea.Phone = dt.Rows[0]["phone"].ToString().Trim();
            b_tea.Research = dt.Rows[0]["research"].ToString().Trim();
            b_tea.Teaprofile = dt.Rows[0]["profile"].ToString().Trim();
            Model.login.Name = dt.Rows[0]["tname"].ToString().Trim();


            
            
        }

        public int modifyPassword(Model.Teacher m_user)//修改密码
        {
            //string connString = @"Server = LAPTOP-RKD5N2CU; Initial Catalog = Student_system; integrated security=SSPI";
            string sql = "UPDATE [6_4_teacher] SET password = @password WHERE tno = @id";
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@password",m_user.Password);
            parameter[1] = new SqlParameter("@id",m_user.Id);

            int res = SqlDbHelper.ExecuteNonQuery(sql,CommandType.Text,parameter);

            return res;

        }

        public int modifyPersonalInformation(Model.Teacher m_user)
        {
            string sql = "UPDATE [6_4_teacher] SET age = @age,specialty = @specialty,email = @email,phone = @phone,research = @research,profile = @profile WHERE tno = @id";
            SqlParameter[] parameter = new SqlParameter[7];
            parameter[0] = new SqlParameter("@age", m_user.Age);
            parameter[1] = new SqlParameter("@specialty", m_user.Specialty);
            parameter[2] = new SqlParameter("@email", m_user.Email);
            parameter[3] = new SqlParameter("@phone", m_user.Phone);
            parameter[4] = new SqlParameter("@research", m_user.Research);
            parameter[5] = new SqlParameter("@profile", m_user.Teaprofile);
            parameter[6] = new SqlParameter("@id", Model.login.Id);


            int res = SqlDbHelper.ExecuteNonQuery(sql,CommandType.Text,parameter);
            return res;
        }
        public int saveTeamInformation()//导师挑选队伍
        {

            string sql = "UPDATE [6_4_teachervol] SET vol1 = @vol1,vol2 = @vol2,vol3 = @vol3,vol4 = @vol4,vol5 = @vol5 WHERE tno = @id";
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@vol1", Model.login.Team1);
            parameter[1] = new SqlParameter("@vol2", Model.login.Team2);
            parameter[2] = new SqlParameter("@vol3", Model.login.Team3);
            parameter[3] = new SqlParameter("@vol4", Model.login.Team4);
            parameter[4] = new SqlParameter("@vol5", Model.login.Team5);
            parameter[5] = new SqlParameter("@id", Model.login.Id);

            int res = SqlDbHelper.ExecuteNonQuery(sql, CommandType.Text, parameter);
            return res;          
        }

        public DataTable teamInformation()//取出导师已经挑选过的队伍
        {
            string sql = string.Format("select * from [6_4_teachervol] where tno = '{0}'", Model.login.Id);
            DataTable dt = SqlDbHelper.ExecuteDataTable(sql);
            return dt;
        }
        public DataTable readMessage()//读取消息(最好改成根据学号搜索)
        {
            string sql = string.Format("SELECT date, message, sender FROM [6_4_message] WHERE receiver = '{0}' OR receiver = N'全体导师' OR receiver = N'所有人'", Model.login.Name);
            DataTable dt = SqlDbHelper.ExecuteDataTable(sql);
            return dt;
        }
        public int checkName(Model.Message m_user)//检查发件人是谁
        {
            int res = -1;
            try
            {
                //先看看是不是组长，组长就是发志愿填报通知
                string sql = string.Format("SELECT * FROM [6_4_teamleader] WHERE sname='{0}'", m_user.Sender);
                SqlDataReader sdread = SqlDbHelper.ExecuteReader(sql);
                if (sdread.Read())
                {
                    res = 0;
                    return res;
                }
                else//不是学生就只可能是管理员发的普通通知
                {
                    res = 1;
                    return res;
                }
            }
            catch
            {
                res = 2;
                return res;
            }
        }

        public DataTable senderTeam(string sender)//队伍志愿通知，通过发件人筛选出队伍信息
        {

            string sql = "select * from [6_4_team] where leader = @leader";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@leader", sender);
            DataTable dt = SqlDbHelper.ExecuteDataTable(sql, CommandType.Text, parameter);

            return dt;                         
        }
        public string noChangename(string name)
        {
            string number = "";
            string sql = "select sno from [6_4_student] where sname = @name";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@name", name);
            DataTable dt = SqlDbHelper.ExecuteDataTable(sql, CommandType.Text, parameter);
            number = dt.Rows[0]["sno"].ToString().Trim();

            return number;

        }
        public DataTable teamInformation(string teamname)
        {

            string sql = "select * from [6_4_team] where teamname = @teamname";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@teamname", teamname);
            DataTable dt = SqlDbHelper.ExecuteDataTable(sql, CommandType.Text, parameter);

            return dt;

        }

    }
}
