using System;

namespace SITConnect
{
    public partial class Homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    loginBtn.Visible = false;
                    registerBtn.Visible = false;
                    profileBtn.Visible = true;
                }
            } else
            {
                profileBtn.Visible = false;
            }
        }
    }
}