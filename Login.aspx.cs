using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using SITConnect.Models;

namespace SITConnect
{
    public class MyObject
    {
        public string success { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public bool ValidateCaptcha()
        {
            bool result = true;

            string captchaResponse = Request.Form["g-recaptcha-response"];

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
            (" https://www.google.com/recaptcha/api/siteverify?secret=6LfWMyUeAAAAABrobJOKljjBi6XVT7i5Z9BnqRFW &response=" + captchaResponse);
            
            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);
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

            if (ValidateCaptcha())
            {
                string pwd = tb_password.Text.ToString().Trim();
                string userid = tb_userid.Text.ToString().Trim();

                SHA512Managed hashing = new SHA512Managed();
                string dbHash = user.getDBHash(userid);
                string dbSalt = user.getDBSalt(userid);

                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);
                    if (userHash.Equals(dbHash))
                    {
                        // Create session
                        Session["LoggedIn"] = userid;
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;
                        Response.Cookies.Add(new System.Web.HttpCookie("AuthToken", guid));

                        Response.Redirect("Profile.aspx", false);
                    }
                    else
                    {
                        errorMsg.Text = "Userid or password is not valid. Please try again.";
                    }
                }
                else
                {
                    // if login fails
                    errorMsg.Text = "Userid or password is not valid. Please try again.";
                }
            }
        }
    }
}