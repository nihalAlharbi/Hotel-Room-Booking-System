using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationRooms.Models;

namespace WebApplicationRooms
{
    public partial class NotificationsList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("LoginPage.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadNotifications();
            }
        }

        private void LoadNotifications()
        {
            var currentUser = (User)Session["User"];

            // Get notifications for the current user
            var userNotifications = DataManagement.Notifications
                .Where(n => n.UserId == currentUser.Id)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            rptNotifications.DataSource = userNotifications;
            rptNotifications.DataBind();
            pnlNoNotifications.Visible = !userNotifications.Any();
        }

        protected void rptNotifications_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var currentUser = (User)Session["User"];
                string notificationId = e.CommandArgument.ToString();
              if(DataManagement.DeleteNotification(int.Parse(notificationId)))
                {
                    // Show success message (you can add a toast here if needed)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "DeleteSuccess",
                        "alert('Notification deleted successfully!');", true);
                }
            }
        }
    }
}