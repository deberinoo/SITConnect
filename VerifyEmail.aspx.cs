using System;
using System.Web;
using SITConnect.Models;
using SITConnect.Services;

namespace SITConnect
{
    public partial class VerifyEmail : System.Web.UI.Page
    {
        string email;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["email"] != null)
            {
                email = Request.QueryString["email"];
            }
        }
        protected void btn_sendEmail_Click(object sender, EventArgs e)
        {
            User user = new User();
            string enteredCode = HttpUtility.HtmlEncode(tb_activation.Text.ToString());
            string storedCode = user.GetVerificationCode(email);
            if (enteredCode == storedCode)
            {
                Response.Redirect("Profile.aspx", false);
            }
            else
            {
                errorMsg.Text = "Verification code is incorrect. Please try again.";
            }
        }
    }
}