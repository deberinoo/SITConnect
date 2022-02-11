using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using SITConnect.Models;
using System.Web;

namespace SITConnect
{
    public partial class Login : System.Web.UI.Page
    {
        public string success { get; set; }
        public List<string> ErrorMessage { get; set; }
        int FailedLoginAttempts = 0;
        DateTime StoredLockedDateTime;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public bool ValidateCaptcha()
        {
            bool result = true;

            string captchaResponse = Request.Form["g-recaptcha-response"];

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
            (" https://www.google.com/recaptcha/api/siteverify?secret= &response=" + captchaResponse);
            
            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        Login jsonObject = js.Deserialize<Login>(jsonResponse);
                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            } catch (WebException ex)
            {
                throw ex;
            }
        }
        protected void btn_Login_Click(object sender, EventArgs e)
        {
            var user = new User();
            var log = new AuditLog();

            string password = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim());
            string email = HttpUtility.HtmlEncode(tb_email.Text.ToString().Trim());
            StoredLockedDateTime = Convert.ToDateTime(user.GetLockedOutTime(email));

            //if (ValidateCaptcha())
            //{
                // Check time difference between time now and stored locked date time
                TimeSpan timespan = (DateTime.Now).Subtract(StoredLockedDateTime);
                Int32 minutesLocked = Convert.ToInt32(timespan.TotalMinutes);
                Int32 pendingMinutes = 15 - minutesLocked;

                // If timer has not reached 0 yet, throw error message
                if (pendingMinutes > 0)
                {
                    errorMsg.Text = "Your account is still locked. Please try again later.";
                }
                // Else, check if the password is correct
                // If incorrect, increment failed login attempts
                else
                {
                    // Checks if email belongs to an account
                    if (user.CheckAccountExists(email))
                    {
                        if (user.CheckPassword(email, password))
                        {
                            // Create session
                            Session["LoggedIn"] = email;
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;
                            Response.Cookies.Add(new System.Web.HttpCookie("AuthToken", guid));
                            Response.Redirect("Profile.aspx", false);

                            user.ResetFailedLoginAttempts(email);
                            log.LogUserInformation(email, "login");
                        }
                        else
                        {
                            log.LogUserInformation(email, "fail");
                            FailedLoginAttempts = user.GetFailedLoginAttempts(email);
                            FailedLoginAttempts++;
                            user.UpdateFailedLoginAttempts(email, FailedLoginAttempts);
                            if (FailedLoginAttempts == 1)
                            {
                                errorMsg.Text = "Email or password is not valid. 2 login attempts remaining.";
                            }
                            else if (FailedLoginAttempts == 2)
                            {
                                errorMsg.Text = "Email or password is not valid. 1 login attempt remaining";
                            }
                            else if (FailedLoginAttempts >= 3)
                            {
                                errorMsg.Text = "Your account has been locked. Please try again later.";
                                user.LockOutUser(email);
                                log.LogUserInformation(email, "lock");
                            }
                        }
                    }
                    else
                    {
                        errorMsg.Text = "Email address and password do not match our records. Please try again.";
                    }
                }
            //}
        }
    }
}