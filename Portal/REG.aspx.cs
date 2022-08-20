using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class REG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Fname"].ToString() != null)
            {
                Label4.Text = Session["Fname"].ToString() + " " + Session["Lname"].ToString();
                Label5.Text = Session["S_id"].ToString();
            }
            else
            {
                Label4.Text = "";
                Label5.Text = "";
            }
                
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Courses.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}