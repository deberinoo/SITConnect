using SITConnect.Models;
using SITConnect.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SITConnect
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
        protected void btn_update_Click(object sender, EventArgs e)
        {
            User user = new User();
            string email = Session["LoggedIn"].ToString();

            string currentPassword = tb_cpassword.Text.ToString().Trim();
            string newPassword = tb_npassword.Text.ToString().Trim();
            string confirmPassword = tb_cfpassword.Text.ToString().Trim();

            // If currentpassword matches password stored in database
            if (user.CheckPassword(email, currentPassword))
            {
                // If newPassword and confirmPassword does not match
                if (newPassword != confirmPassword)
                {
                    errorMsg.Text = "Passwords must match.";
                }
                // If matches, check if newPassword has been used before
                else
                {
                    if (user.CheckPasswordReuse(email, newPassword))
                    {
                        errorMsg.Text = "Passwords cannot be reused.";
                    } else
                    {
                        user.ChangePassword(email, newPassword);
                        Response.Redirect("Login.aspx", false);
                    }
                }
            }
            // Else, throw error message
            else
            {
                errorMsg.Text = "Current password is incorrect.";
            }
        }
    }
}