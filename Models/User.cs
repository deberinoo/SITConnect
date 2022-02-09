using SITConnect.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

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
        public byte[] Image { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiry { get; set; }
        public string CardCVV { get; set; }
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsLocked { get; set; }
        public string LockedDateTime { get; set; }
        public string PasswordHistory1 { get; set; }
        public string PasswordHistory2 { get; set; }


        public bool CreateUser(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES(@Id,@FirstName,@LastName,@Email,@PasswordHash,@PasswordSalt,@BirthDate,@Image,@CardNumber,@CardExpiry,@CardCVV,@Key,@IV,@FailedLoginAttempts,@IsLocked,@LockedDateTime,@PasswordHistory1,@PasswordHistory2)"))
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
                        cmd.Parameters.AddWithValue("@PasswordHistory1",    user.PasswordHistory1);
                        cmd.Parameters.AddWithValue("@PasswordHistory2",    user.PasswordHistory2);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return true;
        }
        public bool CheckAccountExists(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select * FROM Users WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["Id"].ToString() != null)
                    {
                        return true;
                    }
                }
            }
            connection.Close();
            return false;
        }
        public bool CheckPassword(string email, string enteredPassword)
        {
            SHA512Managed hashing = new SHA512Managed();
            string storedHash = GetDBHash(email);
            string storedSalt = GetDBSalt(email);

            if (storedHash != null && storedSalt.Length > 0 && storedHash != null && storedHash.Length > 0)
            {
                string passwordSalt = enteredPassword + storedSalt;
                byte[] hashSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passwordSalt));
                string userHash = Convert.ToBase64String(hashSalt);

                if (userHash.Equals(storedHash)) 
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public int GetFailedLoginAttempts(string email)
        {
            int attempts = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select FailedLoginAttempts FROM Users WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["FailedLoginAttempts"] != null)
                        {
                            attempts = Convert.ToInt32(reader["FailedLoginAttempts"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return attempts;
        }
        public bool ResetFailedLoginAttempts(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Users SET IsLocked = @IsLocked, FailedLoginAttempts = @FailedLoginAttempts WHERE Email = @EMAIL"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@IsLocked", false);
                        cmd.Parameters.AddWithValue("@FailedLoginAttempts", 0);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return true;
        }
        public bool UpdateFailedLoginAttempts(string email, int loginAttempt)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Users SET FailedLoginAttempts = @FailedLoginAttempts WHERE Email = @EMAIL"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@FailedLoginAttempts", loginAttempt);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return true;
        }
        public bool LockOutUser(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Users SET IsLocked = @IsLocked, LockedDateTime = @LockedDateTime WHERE Email = @EMAIL"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@IsLocked", true);
                        cmd.Parameters.AddWithValue("@LockedDateTime", DateTime.Now);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return true;
        }
        public string GetLockedOutTime(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select LockedDateTime FROM Users WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["LockedDateTime"] != null)
                        {
                            LockedDateTime = reader["LockedDateTime"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return LockedDateTime;
        }
        public string GetDBHash(string email)
        {
            string hash = null;

            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select PasswordHash FROM Users WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);

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
                                hash = reader["PasswordHash"].ToString();
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
            return hash;
        }
        public string GetDBSalt(string email)
        {
            string salt = null;

            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select PasswordSalt FROM Users WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);

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
                                salt = reader["PasswordSalt"].ToString();
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
            return salt;
        }
        public string GetPasswordHash(string email)
        {
            string hash = null;

            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select PasswordHash FROM Users WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);

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
                                hash = reader["PasswordHash"].ToString();
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
            return hash;
        }
        public bool ChangePassword(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Users SET PasswordHash = @PasswordHash, PasswordHistory1 = @PasswordHistory1, PasswordHistory2 = @PasswordHistory2 WHERE Email = @EMAIL"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        string oldPassword;
                        string finalHash;
                        string salt;

                        // Retrieve old password
                        oldPassword = GetDBHash(email);

                        // Hash new password
                        salt = GetDBSalt(email);
                        finalHash = Security.HashWithExistingSalt(password, salt);

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                        cmd.Parameters.AddWithValue("@PasswordHistory1", finalHash);
                        cmd.Parameters.AddWithValue("@PasswordHistory2", oldPassword);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return true;
        }
        public bool CheckPasswordReuse(string email, string password)
        {
            bool reusedPassword = false;
            string passwordHistory1 = null;
            string passwordHistory2 = null;

            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select PasswordHistory1, PasswordHistory2 FROM Users WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);

            // Retrieve values of PasswordHistory1 and PasswordHistory2
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordHistory1"] != null)
                        {
                            if (reader["PasswordHistory1"] != DBNull.Value)
                            {
                                passwordHistory1 = reader["PasswordHistory1"].ToString();
                                passwordHistory2 = reader["PasswordHistory2"].ToString();
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

            // Salt new password with existing salt and hash
            string salt = GetDBSalt(email);
            string userHash = Security.HashWithExistingSalt(password, salt);

            // Compare new password hash to PasswordHistory1 and PasswordHistory2
            // If new password hash matches any one of the PasswordHistory, set reusedPassword to true
            // Else, set reusedPassword to false
            if (userHash.Equals(passwordHistory1) || userHash.Equals(passwordHistory2))
            {
                reusedPassword = true;
            } else
            {
                reusedPassword = false;
            }
            return reusedPassword;
        }
    }
}