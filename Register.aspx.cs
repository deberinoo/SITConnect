using System;
using System.Security.Cryptography;
using SITConnect.Models;
using SITConnect.Services;

namespace SITConnect
{
    public partial class Registration : System.Web.UI.Page
    {
        static string finalHash;
        static string salt;
        byte[] IV;
        byte[] Key;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string pwd = tb_password.Text.ToString().Trim();

            (finalHash, salt) = Security.HashPassword(pwd);
            
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;

            createAccount();
        }
        protected void createAccount()
        {
            User user = new User();
            user.Id                  = Security.GenerateRandomNumber();
            user.FirstName           = tb_fname.Text.Trim();
            user.LastName            = tb_fname.Text.Trim();
            user.Email               = tb_email.Text.Trim();
            user.PasswordHash        = finalHash;
            user.PasswordSalt        = salt;
            user.BirthDate           = tb_dob.Text.Trim();
            user.Image               = DBNull.Value.ToString();
            user.CardNumber          = Security.Encrypt(tb_cardnum.Text.Trim(), Key, IV);
            user.CardExpiry          = Security.Encrypt(tb_cardexp.Text.Trim(), Key, IV);
            user.CardCVV             = Security.Encrypt(tb_cardcvv.Text.Trim(), Key, IV);
            user.Key                 = Key;
            user.IV                  = IV;
            user.FailedLoginAttempts = 0;
            user.IsLocked            = false;
            user.LockedDateTime      = DBNull.Value.ToString();

            user.CreateUser(user);
            Response.Redirect("Login.aspx", false);
        }
    }
}