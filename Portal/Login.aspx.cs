using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Portal
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
               
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\portal.mdf;Integrated Security=True;Connect Timeout=30");
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Student where UserName=@UserName and Pwd=@Pwd ", conn);
               cmd.Parameters.AddWithValue("UserName",TextBox2.Text);
                cmd.Parameters.AddWithValue("Pwd", TextBox1.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Session.Timeout = 15;
                    Session["UserName"] = reader["UserName"];
                    Session["S_id"] = reader["student_id"];
                    Session["psw"] = reader["Pwd"];
                    Session["Fname"] = reader["First_name"];
                    Session["Lname"] = reader["Last_name "];
                    Session["Year"] = reader["Year "];
                    Session["Mobile"] = reader["Mobile "];
                    Session["Email"] = reader["Email"];
                    Session["address"] = reader["Address "];
                    Session["dept_id"] = reader["Department_id"];

                    Response.Redirect("REG.aspx");

                }
                else
                {
                    Label4.Text = "invalid user";
                }
             
             

                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Label3.Text = ex.Message;
            }
           
            
            
        }
        
       
    }
}