using System;
using System.Security.Cryptography;

namespace CacheMachine.Common
{
    public class HashcodeHelper
    {
        /// <summary>
        /// Check if hashcode computed by the given plain text and salt is equal to the existing hashcode 
        /// </summary>
        /// <param name="plainText">Plain text</param>
        /// <param name="salt">Salt</param>
        /// <param name="hash">Existing hashcode</param>
        /// <returns>Check result</returns>
        public bool CheckHashMatch(string plainText, string salt, string hash)
        {
            var finalString = plainText + salt;
            return hash == GetHash(finalString);
        }

        /// <summary>
        /// Convert byte array to string
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>String</returns>
        private string ConvertBytesToString(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Convert string to byte array
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>Byte array</returns>
        private byte[] ConvertStringToBytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// Generate hashcode by the given plain text and salt
        /// </summary>
        /// <param name="plainText">Plain text</param>
        /// <param name="salt">Salt</param>
        /// <returns>Hashcode</returns>
        public string GenerateHash(string plainText, out string salt)
        {
            salt = GetSalt();
            string finalString = plainText + salt;
            return GetHash(finalString);
        }

        /// <summary>
        /// Compute hash of the given string
        /// </summary>
        /// <param name="text">Given text(plain+salt)</param>
        /// <returns>Hash string</returns>
        private string GetHash(string text)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = ConvertStringToBytes(text);
            byte[] resultBytes = sha.ComputeHash(dataBytes);
            return ConvertBytesToString(resultBytes);
        }

        /// <summary>
        /// Get generated salt
        /// </summary>
        /// <returns>Salt string</returns>
        private string GetSalt()
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[Constants.SaltSize];
            rngCryptoServiceProvider.GetNonZeroBytes(saltBytes);
            string saltString = ConvertBytesToString(saltBytes);
            return saltString;
        }
    }
}