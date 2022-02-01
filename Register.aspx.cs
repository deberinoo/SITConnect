using System;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SITConnect
{
    public partial class Registration : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string pwd = tb_password.Text.ToString().Trim();

            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            //Fills array of bytes with a cryptographically strong sequence of random values.
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);

            SHA512Managed hashing = new SHA512Managed();

            string pwdWithSalt = pwd + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
            
            finalHash = Convert.ToBase64String(hashWithSalt);
            
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;
            createAccount();
        }
        protected void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES(@FirstName,@LastName,@Email,@PasswordHash,@PasswordSalt,@BirthDate,@Image,@CardNumber,@CardExpiry,@CardCVV)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName",  tb_fname.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName",   tb_lname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email",      tb_email.Text.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash",   finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt",   salt);
                            cmd.Parameters.AddWithValue("@BirthDate",  tb_dob.Text.Trim());
                            cmd.Parameters.AddWithValue("@Image",      DBNull.Value);
                            cmd.Parameters.AddWithValue("@CardNumber", tb_cardnum.Text.Trim());
                            cmd.Parameters.AddWithValue("@CardExpiry", tb_cardexp.Text.Trim());
                            cmd.Parameters.AddWithValue("@CardCVV",    tb_cardcvv.Text.Trim());
                            
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        private int checkPassword(string password)
        {
            int score = 0;

            // if length of password is less than 8 chars
            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }

            // Score 2 Weak
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }

            // Score 3 Medium
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }

            // Score 4 Strong
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }

            // Score 5 Excellent
            if (Regex.IsMatch(password, "[^A-Za-z0-9]"))
            {
                score++;
            }

            return score;
        }
        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            } catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            } finally { }
            return cipherText;
        }
    }
}