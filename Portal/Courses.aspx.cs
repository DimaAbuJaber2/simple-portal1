using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Portal
{
    public partial class Reg : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\portal.mdf;Integrated Security=True;Connect Timeout=30");
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!IsPostBack)
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Course,Registration,Teacher where Registration.Course_id=Course.C_id and Registration.Teacher_id=Teacher.Dr_id  ", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                  //  ListBox1.Items.Add(new ListItem(" Course_name"+" | "+"no_hours"+" | "+"Dr_name"+" | "+"Lecture_Time"+" | "+"\n ",0+"\t"));
                    while(reader.Read())
                    {
                        ListBox1.Items.Add(new ListItem(reader["Course_name"].ToString()+"  |   " + reader["no of hours "]+"   |   " + reader["Dr_name"]+"  |  " + reader["Lecture_Time"]+"    |     " + "\n"+"\n",reader["C_id"].ToString()));
                        
             
                    }
                    reader.Close();
                    conn.Close();

                }
                catch(Exception ex)
                {
                    Label5.Text = ex.Message;
                }
            }
        }
        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Course,Registration where Course.C_id=Registration.Course_id and Course.C_id=" + ListBox1.SelectedValue, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {


                 Session["Course"] = new CourseWithTime(new Course(reader["Course_name"].ToString(), Int32.Parse(reader["no of hours "].ToString()), Int32.Parse(reader["C_id"].ToString())), reader["Lecture_Time"].ToString(),float.Parse(reader["start"].ToString()),float.Parse(reader["end"].ToString()));
               
            }

        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            bool addOrNot ;
            if (Session["ListCourse"] == null)
                Session["ListCourse"] = new ListCourse();

            
           addOrNot= ((ListCourse)Session["ListCourse"]).addCourse((CourseWithTime)Session["Course"]);
            if (addOrNot == false)
                Label5.Text = "there is conflict in courses";
            else Label5.Text = "";
            

            PrintALL();
            
        }
        public void PrintALL()
        {
            ListBox2.Items.Clear();
            int sumHours = 0;
            foreach (CourseWithTime cr in ((ListCourse)Session["ListCourse"]).Courses)
            {

                ListBox2.Items.Add(new ListItem((cr.CourseItem.name + "\t" + cr.CourseItem.no_of_hours + "\t" + cr.Lecture), (cr.CourseItem.C_id.ToString())));
                sumHours += cr.CourseItem.no_of_hours;
            }

            TextBox3.Text = sumHours.ToString();
            if (Int32.Parse(TextBox3.Text) > 18)
            {
                Label7.Text = "Your Registration wasn't complete ,The maximum number of hours allowed is 18";
                int currenthours = Int32.Parse(TextBox3.Text);
                int hourstoMinus = ((ListCourse)Session["ListCourse"]).getCourse(Int32.Parse((ListBox1.SelectedValue).ToString())).CourseItem.no_of_hours;
                TextBox3.Text = (currenthours - hourstoMinus).ToString();
                ListBox2.Items.Clear();
                ((ListCourse)Session["ListCourse"]).removeCourse(((ListCourse)Session["ListCourse"]).getCourse(Int32.Parse(ListBox1.SelectedValue)));
                foreach (CourseWithTime cr in ((ListCourse)Session["ListCourse"]).Courses)
                {
                   
                    ListBox2.Items.Add(new ListItem((cr.CourseItem.name + "\t" + cr.CourseItem.no_of_hours + "\t" + cr.Lecture), (cr.CourseItem.C_id.ToString())));
                    //sumHours += cr.CourseItem.no_of_hours;
                }

            }
            else
                Label7.Text = "";


        }

        
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ListBox2.Items.Count > 0)
                ((ListCourse)Session["ListCourse"]).removeCourse(((ListCourse)Session["ListCourse"]).getCourse(Int32.Parse(ListBox2.SelectedValue)));
            PrintALL();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}