using System;
using System.Security.Cryptography;
using System.Text;

namespace SITConnect.Services
{
    public class Security
    {
        public static (string finalHash, string salt) HashPassword(string password)
        {
            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            //Fills array of bytes with a cryptographically strong sequence of random values.
            rng.GetBytes(saltByte);
            string salt = Convert.ToBase64String(saltByte);

            SHA512Managed hashing = new SHA512Managed();

            string passwordSalt = password + salt;
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passwordSalt));

            string finalHash = Convert.ToBase64String(hashWithSalt);
            return (finalHash, salt);
        }
        public static string HashWithExistingSalt(string password, string salt)
        {
            SHA512Managed hashing = new SHA512Managed();

            string passwordSalt = password + salt;
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passwordSalt));

            string finalHash = Convert.ToBase64String(hashWithSalt);
            return finalHash;
        }
        public static string Encrypt(string text, byte[] Key, byte[] IV)
        {
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.Key = Key;
            cipher.IV = IV;
            ICryptoTransform encryptTransform = cipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            string cipherString = Convert.ToBase64String(cipherText);

            return cipherString;
        }
        public static int GenerateRandomNumber()
        {
            Random rnd = new Random();
            int num = rnd.Next();
            return num;
        }
        public static string GenerateOTP()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}