using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SITConnect.Models
{
    public class Email
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        public bool saveVerificationCode(string email, string code)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Users SET VerificationCode = @VerificationCode WHERE Email = @EMAIL"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@VerificationCode", code);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return true;
        }
        public string sendVerificationCode(string email, string code)
        {
            string fromaddress = "SITConnect <deborah.sitconnect@gmail.com>";
            string str = null;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("deborah.sitconnect@gmail.com", "202772k!"),
                EnableSsl = true
            };
            var mailMessage = new MailMessage
            {
                Subject = "Verify your email address",
                Body = "Hello! Your verification code is: " + code
            };
            mailMessage.To.Add(email);
            mailMessage.From = new MailAddress(fromaddress);
            try
            {
                smtpClient.Send(mailMessage);
                return str;
            }
            catch
            {
                throw;
            }
        }
    }
}