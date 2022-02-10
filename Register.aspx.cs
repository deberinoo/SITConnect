using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using SITConnect.Models;
using SITConnect.Services;

namespace SITConnect
{
    public partial class Registration : System.Web.UI.Page
    {
        static string finalHash;
        static string salt;
        byte[] Image;
        byte[] IV;
        byte[] Key;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string password = tb_password.Text.ToString().Trim();
            string email = tb_email.Text.ToString().Trim();

            var user = new User();
            var log = new AuditLog();

            if (Page.IsValid)
            {
                if (user.CheckAccountExists(email))
                {
                    lbl_email.Text = "Email already exists";
                }
                else
                {
                    (finalHash, salt) = Security.HashPassword(password);

                    RijndaelManaged cipher = new RijndaelManaged();
                    cipher.GenerateKey();
                    Key = cipher.Key;
                    IV = cipher.IV;

                    createAccount();
                    sendVerification(email);
                    log.LogUserInformation(email, "register");
                }
            }
        }
        protected void createAccount()
        {
            if (validateFile())
            {
                User user = new User();
                user.Id = Security.GenerateRandomNumber();
                user.FirstName          = HttpUtility.HtmlEncode(tb_fname.Text.Trim());
                user.LastName           = HttpUtility.HtmlEncode(tb_fname.Text.Trim());
                user.Email              = HttpUtility.HtmlEncode(tb_email.Text.Trim());
                user.PasswordHash       = finalHash;
                user.PasswordSalt       = salt;
                user.BirthDate          = HttpUtility.HtmlEncode(tb_dob.Text.Trim());
                user.Image              = Image;
                user.CardNumber         = Security.Encrypt(tb_cardnum.Text.Trim(), Key, IV);
                user.CardExpiry         = Security.Encrypt(tb_cardexp.Text.Trim(), Key, IV);
                user.CardCVV            = Security.Encrypt(tb_cardcvv.Text.Trim(), Key, IV);
                user.Key                = Key;
                user.IV                 = IV;
                user.FailedLoginAttempts = 0;
                user.IsLocked           = false;
                user.LockedDateTime     = DBNull.Value.ToString();
                user.PasswordHistory1   = finalHash;
                user.PasswordHistory2   = DBNull.Value.ToString();
                user.PasswordChangeTime = DBNull.Value.ToString();
                user.VerificationCode   = DBNull.Value.ToString();

                user.CreateUser(user);
                sendVerification(tb_email.Text.Trim());
                Response.Redirect(String.Format("VerifyEmail.aspx?email={0}", tb_email.Text.Trim(), 0));
            }
        }
        protected bool validateFile()
        {
            HttpPostedFile postedFile = file_image.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                Image = binaryReader.ReadBytes((int)stream.Length);
                return true;
            }
            else
            {
                imageError.Text = "Only files with the following extensions are allowed: jpg bmp gif png";
                return false;
            }
        }
        protected void sendVerification(string user_email)
        {
            Email email = new Email();
            string code = Security.GenerateOTP();
            email.saveVerificationCode(user_email, code);
            email.sendVerificationCode(user_email, code);
        }
    }
}