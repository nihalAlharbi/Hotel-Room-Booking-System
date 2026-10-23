using System;
using System.Web.UI;
using WebApplicationRooms.Models;

namespace WebApplicationRooms
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Response.Redirect("BookingsDashboard.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter username and password";
                lblError.Visible = true;
                lblError.Style["color"] = "red";
                lblError.Style["font-weight"] = "bold";
                return;
            }

            User user = DataManagement.AuthenticateUser(username, password);
            if (user != null)
            {
                Session["User"] = user;
                Response.Redirect("BookingsDashboard.aspx");
            }
            else
            {
                lblError.Text = "Invalid username or password";
                lblError.Visible = true;
                lblError.Style["color"] = "red";
                lblError.Style["font-weight"] = "bold";
            }
        }

        

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPage.aspx");
        }
    }
}
