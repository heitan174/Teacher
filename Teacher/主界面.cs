using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Teacher
{
    public partial class 主界面 : Form
    {
        string uid = "";
        DataTable d1 = new DataTable();
        DataTable d2 = new DataTable();
        Label[] tbs = new Label[5];
        string[] no = new string[5];//显示label
        int[] sel = new int[5];//选中的行数
        int[] rowsel = new int[50];//某一行是否被选中了
        int[] selrowindex = new int[5];//保存五个志愿相应的dgv索引，给textbox使用
        int[] labsel = new int[5];//表示五个label的状态
        string currentrow = "";

        public 主界面(Model.Teacher m_user)
        {
            InitializeComponent();
            uid = m_user.Id;

            tbs[0] = firstChoice;
            tbs[1] = secondChoice;
            tbs[2] = thirdChoice;
            tbs[3] = fourthChoice;
            tbs[4] = fifthChoice;
 
            for (int i = 0; i < 5; i++)
            { 
                sel[i] = -1;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                rowsel[i] = 0;
            } 
            for (int i = 0; i < 5; i++)
            {
                labsel[i] = 0;
            }
        }

        private void 主界面_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;//通知界面
            panel2.Visible = false;//个人信息
            panel3.Visible = false;//选队伍
            panel4.Visible = true;//主界面

            dataGridView1.AutoGenerateColumns = false;

            data_design();//datagridview表格的格式设计
            selectTeam(); //显示对应年级专业所有队伍信息
            teamVol();
            setLabel();
            personinFormation();//个人信息显示

            label13.Text = "当前用户：" + Model.login.Name;

        }
        private void data_design()//浏览小组里表格的属性设计
        {                     
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dataGridView1.ReadOnly = true;//设置数据表格为只读
            dataGridView1.BackgroundColor = Color.White;//背景为白色
            dataGridView1.MultiSelect = false;//只允许选中单行
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//整行选中

        }


        private void label17_Click(object sender, EventArgs e)//浏览小组返回主界面
        {
            panel1.Visible = false;//通知界面
            panel2.Visible = false;//个人信息
            panel3.Visible = false;//选队伍
            panel4.Visible = true;//主界面
        }

        private void label18_Click(object sender, EventArgs e)//个人信息返回主界面
        {
            panel1.Visible = false;//通知界面
            panel2.Visible = false;//个人信息
            panel3.Visible = false;//选队伍
            panel4.Visible = true;//主界面
        }

        private void button2_Click(object sender, EventArgs e)//进入浏览小组
        {
            panel1.Visible = false;//通知界面
            panel2.Visible = false;//个人信息
            panel3.Visible = true;//浏览小组
            panel4.Visible = false;//主界面

            //data_design();//datagridview表格的格式设计
            //selectTeam(); //显示对应年级专业所有队伍信息
            //teamVol();
         

        }
        private void selectTeam()//显示对应年级专业所有队伍信息   
        {
            BLL.User t_user = new BLL.User();
            d1 = t_user.Check();
            d2 = d1.Clone();
            dataGridView1.DataSource = d1;//设置表格控件的DataSource属性，显示队伍信息
        }

        private void teamVol()//teachervol表中的已有队伍
        {
            DAL.User d_user = new DAL.User();
            DataTable dt = d_user.teamInformation();
            no[0] = dt.Rows[0]["vol1"].ToString().Trim();
            no[1] = dt.Rows[0]["vol2"].ToString().Trim();
            no[2] = dt.Rows[0]["vol3"].ToString().Trim();
            no[3] = dt.Rows[0]["vol4"].ToString().Trim();
            no[4] = dt.Rows[0]["vol5"].ToString().Trim();
            for (int i = 0; i < 5; i++ )
            {
                if(no[i] != "")
                {
                    sel[i] = -2;//表示有初始文本
                }
            }



        }

        private void button4_Click(object sender, EventArgs e)//转到个人信息界面
        {

            panel1.Visible = false;//通知界面
            panel2.Visible = true;//个人信息
            panel3.Visible = false;//选队伍
            panel4.Visible = false;//主界面
        }

        private void label19_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;//通知界面
            panel2.Visible = false;//个人信息
            panel3.Visible = false;//选队伍
            panel4.Visible = true;//主界面
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;//通知界面
            panel2.Visible = false;//个人信息
            panel3.Visible = false;//选队伍
            panel4.Visible = false;//主界面

            DAL.User d_user = new DAL.User();
            DataTable dt = d_user.readMessage();
            dataGridView3.DataSource = dt;
            dataGridView3.AllowUserToAddRows = false;
        }



        private void button5_Click(object sender, EventArgs e)//浏览小组，取消选定(无用)
        {
            //Model.login.repeat = false;//判断添加数据的表格是否数据重复           
            //BLL.User b_user = new BLL.User();
            //if (dataGridView2.CurrentRow != null)
            //{
            //    d1 = b_user.selectTeam(d2, d1, dataGridView2.CurrentRow.Index);
            //    if (Model.login.repeat == false)
            //    {
            //        dataGridView1.DataSource = d1;
            //        d2.Rows.Remove(d2.Rows[dataGridView2.CurrentRow.Index]);
            //        dataGridView2.DataSource = d2;
            //        dataGridView1.ClearSelection();
            //        dataGridView2.ClearSelection();
            //    }
            //    else
            //        MessageBox.Show("该队伍已添加");
            //    //dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            //}
            //else
            //{
            //    MessageBox.Show("表格中无队伍");
            //}

        }

        private void label20_Click(object sender, EventArgs e)//更新个人信息到数据库，个人信息保存键
        {
            foreach (Control cp in this.panel2.Controls)
            {
                if (cp.GetType() == typeof(TextBox))
                {
                    ((TextBox)cp).ReadOnly = true;
                }
            }
            Model.Teacher b_teacher = new Model.Teacher();
            BLL.User b_user = new BLL.User();

            b_teacher.Age = textBox2.Text;
            b_teacher.Specialty = textBox4.Text;
            b_teacher.Email = textBox5.Text;
            b_teacher.Phone = textBox6.Text;
            b_teacher.Research = textBox7.Text;
            b_teacher.Teaprofile = textBox8.Text;

            int d_res = -1;
            
            try
            {
                d_res = b_user.modifyPersonalInformation(b_teacher);
                if (d_res == 1)
                    MessageBox.Show("信息修改成功");
                label20.Enabled = false;
            }
            catch(TOOLS.AddException ex)
            {
                MessageBox.Show(ex.Message);               
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void label14_Click(object sender, EventArgs e)//导师编辑个人信息
        {
            foreach (Control cp in this.panel2.Controls)
            {
                if (cp.GetType() == typeof(TextBox))
                {
                    ((TextBox)cp).ReadOnly = false;
                }
            }
            textBox1.ReadOnly = true;//姓名工号不可改
            textBox3.ReadOnly = true;
            label20.Enabled = true;//保存键解锁

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//选择队伍
        {
            currentrow = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)//主界面
        {
            panel1.Visible = false;//通知界面
            panel2.Visible = false;//个人信息
            panel3.Visible = false;//选队伍
            panel4.Visible = true;//主界面
        }
        private void personinFormation()//个人信息界面数据填写
        {
            Model.Teacher b_tea = new Model.Teacher();
            DAL.User b_user = new DAL.User();
            b_user.PersonInformation(b_tea, uid);

            textBox1.Text = b_tea.Name;
            textBox2.Text = b_tea.Age;
            textBox3.Text = b_tea.Id;
            textBox4.Text = b_tea.Specialty;
            textBox5.Text = b_tea.Email;
            textBox6.Text = b_tea.Phone;
            textBox7.Text = b_tea.Research;
            textBox8.Text = b_tea.Teaprofile;

            foreach (Control cp in this.panel2.Controls)
            {
                if (cp.GetType() == typeof(TextBox))
                {
                    ((TextBox)cp).ReadOnly = true;
                }
            }
        }

        private void setLabel()//浏览队伍，显示选中队伍
        {
            firstChoice.Text = no[0];
            secondChoice.Text = no[1];
            thirdChoice.Text = no[2];
            fourthChoice.Text = no[3];
            fifthChoice.Text = no[4];
        }
        private void setText()
        {
            for (int i = 0; i < 5; i++)
            {
                if (sel[i] == -1)//selrowindex[Array.Indexof(tbs,tb)] != 0)
                    no[i] = "";
                else if (sel[i] == -2)
                {

                }
                else
                    no[i] = dataGridView1.Rows[sel[i]].Cells[0].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)//浏览界面，选定队伍(无用)
        {
            //Model.login.repeat = false;//判断添加数据的表格是否数据重复           
            //BLL.User b_user = new BLL.User();
            //int dt_rows = dataGridView2.Rows.Count;

            //if (dataGridView1.CurrentRow != null && dt_rows < 6)
            //{
            //    d2 = b_user.selectTeam(d1, d2, dataGridView1.CurrentRow.Index);
            //    if (Model.login.repeat == false)
            //    {
            //        dataGridView2.DataSource = d2;
            //        d1.Rows.Remove(d1.Rows[dataGridView1.CurrentRow.Index]);
            //        dataGridView1.DataSource = d1;
            //        dataGridView1.ClearSelection();
            //        dataGridView2.ClearSelection();
            //    }
            //    else
            //        MessageBox.Show("该队伍已添加");
            //    //dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            //}
            //else if (dt_rows < 6)
            //{
            //    MessageBox.Show("表格中无队伍");
            //}
            //else
            //    //dataGridView2.AllowUserToAddRows = false;
            //    MessageBox.Show("已选满五支队伍");


        }

        private void save1_Click(object sender, EventArgs e)//浏览队伍—保存键
        {
            
            BLL.User m_user = new BLL.User();
            Model.login.Team1 = firstChoice.Text;
            Model.login.Team2 = secondChoice.Text;
            Model.login.Team3 = thirdChoice.Text;
            Model.login.Team4 = fourthChoice.Text;
            Model.login.Team5 = fifthChoice.Text;

            try
            {
                int res = m_user.SaveTeamInformation();
                if (res == 1)
                    MessageBox.Show("信息修改成功");
                
            }
            catch (TOOLS.AddException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void firstChoice_DoubleClick(object sender, EventArgs e)
        {
            Label tb = (Label)sender;
            if(tb.Text != "")
            {
                if(sel[Array.IndexOf(tbs,tb)] == -2)//有初始文本
                {
                    sel[Array.IndexOf(tbs, tb)] = -1;
                    setText();
                }
                else//无初始文本，即消除从左dgv取出的队伍信息
                {
                    rowsel[sel[Array.IndexOf(tbs, tb)]] = 0;
                    sel[Array.IndexOf(tbs, tb)] = -2;
                    setText();
                }
            }
            else
            {

            }

        }

        private void firstChoice_Click(object sender, EventArgs e)
        {
            Label tb = (Label)sender;
            if (tb.Text != "")
            {
                if (sel[Array.IndexOf(tbs, tb)] == -2)//有初始文本
                {
                    sel[Array.IndexOf(tbs, tb)] = -1;
                    setText();
                    setLabel();
                }
                else//无初始文本，即消除从左dgv取出的队伍信息
                {
                    rowsel[sel[Array.IndexOf(tbs, tb)]] = 0;
                    sel[Array.IndexOf(tbs, tb)] = -2;
                    setText();
                    setLabel();
                }
            }
            else
            {

            }
        }

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)//消息通知
        {
            //自动编号，与数据无关
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               e.RowBounds.Location.Y,
               this.dataGridView3.RowHeadersWidth - 4,
               e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                  (e.RowIndex + 1).ToString(),
                     this.dataGridView3.RowHeadersDefaultCellStyle.Font,
                   rectangle,
                     this.dataGridView3.RowHeadersDefaultCellStyle.ForeColor,
                   TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView3_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (this.dataGridView3.Columns[e.ColumnIndex].HeaderText == "操作")
                {
                    StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;//设置重绘入单元格的字体样式
                    sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;

                    e.PaintBackground(e.CellBounds, false);//重绘边框

                    //设置要写入字体的大小
                    System.Drawing.Font myFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    SizeF sizeLook = e.Graphics.MeasureString("查看", myFont);

                    float fLook = sizeLook.Width / sizeLook.Width;
                    //设置每个“按钮的边界”
                    RectangleF rectLook = new RectangleF(e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Width * fLook, e.CellBounds.Height);

                    e.Graphics.DrawString("查看", myFont, Brushes.Black, rectLook, sf);
                    e.Handled = true;
                }
            }
        }

        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                Point curPosition = e.Location;//当前鼠标在当前单元格中的坐标
                if (this.dataGridView3.Columns[e.ColumnIndex].HeaderText == "操作")
                {
                    Graphics g = this.dataGridView3.CreateGraphics();
                    System.Drawing.Font myFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    SizeF sizeLook = g.MeasureString("查看", myFont);
                    float fLook = sizeLook.Width / sizeLook.Width;

                    Rectangle rectTotal = new Rectangle(0, 0, this.dataGridView3.Columns[e.ColumnIndex].Width, this.dataGridView3.Rows[e.RowIndex].Height);
                    RectangleF rectLook = new RectangleF(rectTotal.Left, rectTotal.Top, rectTotal.Width * fLook, rectTotal.Height);

                    int rowindex;
                    if (rectLook.Contains(curPosition))
                    {
                        rowindex = dataGridView3.CurrentRow.Index;
                        Model.Message m_user = new Model.Message();
                        m_user.Time = dataGridView3.Rows[rowindex].Cells[1].Value.ToString();
                        m_user.Text = dataGridView3.Rows[rowindex].Cells[2].Value.ToString();
                        m_user.Sender = dataGridView3.Rows[rowindex].Cells[3].Value.ToString();                       


                        BLL.User b_user = new BLL.User();
                        int b_res = b_user.checkName(m_user);//检查发件人是谁，是学生就是邀请通知
                        if (b_res == 0)//如果是学生发来的消息
                        {
                            new 队伍志愿通知(m_user).ShowDialog();                           
                        }
                        else if(b_res == 1)
                        {
                            new 消息通知(m_user).ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                    }
                }//

            }//排除表头

            

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;//当前选中的行
            bool repeat = false;

            if (rowsel[rowindex] == 1)//判断是否选中过，有的话进去if
            {
                rowsel[rowindex] = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (sel[i] == rowindex)
                    {
                        sel[i] = -1;
                        selrowindex[i] = -1;//
                        break;
                    }
                }
            }
            else//没选中过
            {
                rowsel[rowindex] = 1;
                for (int i = 0; i < 5; i++)
                {
                    if (sel[i] == -1)
                    {
                        foreach (Label tb in tbs)
                        {
                            if (tb.Text == dataGridView1.Rows[rowindex].Cells[0].Value.ToString())
                            {
                                repeat = true;
                            }
                            else
                            {

                            }

                        }
                        if (repeat == false)
                        {
                            sel[i] = rowindex;
                            selrowindex[i] = rowindex;
                            break;
                        }
                        else
                        {
                            MessageBox.Show("该小组已选定");
                            break;
                        }




                    }
                }
            }
            setText();
            setLabel();
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            Form f1 = new 队伍信息(currentrow);
            f1.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            System.Threading.Thread t2 = new System.Threading.Thread(new System.Threading.ThreadStart(() => Application.Run(new 登陆界面())));
            t2.Start();
            this.Close();
        }





        //private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        //{
        //    if (dataGridView2.CurrentCell == null)//点击列头
        //        return;
        //    int row = dataGridView2.CurrentCell.RowIndex;
        //    int col = dataGridView2.CurrentCell.ColumnIndex;
        //    if (row != -1 && col != -1)
        //    {
        //        if (comboBox1.SelectedValue != dataGridView2.CurrentCell.Value)
        //        {
        //            comboBox1.SelectedIndexChanged -= null;
        //            comboBox1.SelectedValue = dataGridView2.CurrentCell.Value;
        //            comboBox1.SelectedIndexChanged += SelectIndexChange;
        //        }
        //        Rectangle rec = dataGridView2.GetCellDisplayRectangle(col, row, true);
        //        comboBox1.Location = rec.Location;
        //        comboBox1.Size = rec.Size;
        //        comboBox1.Visible = true;
        //    }
        //    else
        //        comboBox1.Visible = false;
        //}

        //private void SelectIndexChange(object sender, EventArgs e)
        //{
        //    //更改dataGridView中的值為下拉框所選的值
        //    ComboBox cmb = (ComboBox)sender;
        //    if (cmb == null) return;
        //    if (dataGridView1.CurrentCell.Value != cmb.SelectedValue)
        //        dataGridView1.CurrentCell.Value = cmb.SelectedValue;
        //}















    }
}
