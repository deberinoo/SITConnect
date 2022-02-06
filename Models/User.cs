using System;
using System.Data.SqlClient;

namespace SITConnect.Models
{
    public class User
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string BirthDate { get; set; }
        public string Image { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiry { get; set; }
        public string CardCVV { get; set; }
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsLocked { get; set; }
        public string LockedDateTime { get; set; }
        public bool CreateUser(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES(@Id,@FirstName,@LastName,@Email,@PasswordHash,@PasswordSalt,@BirthDate,@Image,@CardNumber,@CardExpiry,@CardCVV,@Key,@IV,@FailedLoginAttempts,@IsLocked,@LockedDateTime)"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@Id",                  user.Id);
                        cmd.Parameters.AddWithValue("@FirstName",           user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName",            user.LastName);
                        cmd.Parameters.AddWithValue("@Email",               user.Email);
                        cmd.Parameters.AddWithValue("@PasswordHash",        user.PasswordHash);
                        cmd.Parameters.AddWithValue("@PasswordSalt",        user.PasswordSalt);
                        cmd.Parameters.AddWithValue("@BirthDate",           user.BirthDate);
                        cmd.Parameters.AddWithValue("@Image",               user.Image);
                        cmd.Parameters.AddWithValue("@CardNumber",          user.CardNumber);
                        cmd.Parameters.AddWithValue("@CardExpiry",          user.CardExpiry);
                        cmd.Parameters.AddWithValue("@CardCVV",             user.CardCVV);
                        cmd.Parameters.AddWithValue("@Key",                 user.Key);
                        cmd.Parameters.AddWithValue("@IV",                  user.IV);
                        cmd.Parameters.AddWithValue("@FailedLoginAttempts", user.FailedLoginAttempts);
                        cmd.Parameters.AddWithValue("@IsLocked",            user.IsLocked);
                        cmd.Parameters.AddWithValue("@LockedDateTime",      user.LockedDateTime);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return true;
        }
        public string getDBHash(string userid)
        {
            string h = null;

            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select PasswordHash FROM Users WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }
        public string getDBSalt(string userid)
        {
            string s = null;

            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select PasswordSalt FROM Users WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordSalt"] != null)
                        {
                            if (reader["PasswordSalt"] != DBNull.Value)
                            {
                                s = reader["PasswordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }
    }
}