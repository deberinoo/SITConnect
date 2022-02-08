using System;
using System.IO;
using System.Security.Cryptography;
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
            }
        }
        protected void createAccount()
        {
            if (validateFile())
            {
                User user = new User();
                user.Id = Security.GenerateRandomNumber();
                user.FirstName = tb_fname.Text.Trim();
                user.LastName = tb_fname.Text.Trim();
                user.Email = tb_email.Text.Trim();
                user.PasswordHash = finalHash;
                user.PasswordSalt = salt;
                user.BirthDate = tb_dob.Text.Trim();
                user.Image = Image;
                user.CardNumber = Security.Encrypt(tb_cardnum.Text.Trim(), Key, IV);
                user.CardExpiry = Security.Encrypt(tb_cardexp.Text.Trim(), Key, IV);
                user.CardCVV = Security.Encrypt(tb_cardcvv.Text.Trim(), Key, IV);
                user.Key = Key;
                user.IV = IV;
                user.FailedLoginAttempts = 0;
                user.IsLocked = false;
                user.LockedDateTime = DBNull.Value.ToString();

                user.CreateUser(user);
                Response.Redirect("Login.aspx", false);
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
    }
}