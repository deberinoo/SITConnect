using System;
using System.Data;
using System.Data.SqlClient;

namespace SITConnect.Models
{
    public class AuditLog
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        string action;
        public void LogUserInformation(string email, string status)
        {
            switch (status)
            {
                case "register":
                    action = "User has successfully created an account.";
                    break;
                case "login":
                    action = "User has successfully logged in.";
                    break;
                case "fail":
                    action = "User failed to login.";
                    break;
                case "lock":
                    action = "User account has been locked.";
                    break;
                case "logout":
                    action = "User has successfully logged out.";
                    break;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO AuditLogs VALUES(@DateTime,@User,@Action) "))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@User", email);
                            cmd.Parameters.AddWithValue("@Action", action);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}