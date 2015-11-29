// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace eCollabro.Utilities
{   
    
    /// <summary>
    /// DataEncryption
    /// </summary>
    public class    DataEncryption
    {

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {

            if (plainText == null) throw new ArgumentNullException("plainText");

            //encrypt data
            var dataPlain = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(dataPlain, null, DataProtectionScope.LocalMachine);

            //return as base64 string
            return Convert.ToBase64String(encrypted);
         }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public static string Decrypt(string cipher)
        {
            if (cipher == null) throw new ArgumentNullException("cipher");

            //parse base64 string
            byte[] data = Convert.FromBase64String(cipher);

            //decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
            return Encoding.Unicode.GetString(decrypted);
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Encrypt(string data,string salt)
        {

            if (data == null || salt ==null) throw new ArgumentNullException("plainText");

            //encrypt data
            var dataBytes = Encoding.Unicode.GetBytes(data);
            var saltBytes = Encoding.Unicode.GetBytes(salt);

            byte[] encrypted = ProtectedData.Protect(dataBytes, saltBytes, DataProtectionScope.LocalMachine);

            //return as base64 string
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Decrypt(string cipher,string salt)
        {
            if (cipher == null || salt==null ) throw new ArgumentNullException("cipher");

            //parse base64 string
            byte[] data = Convert.FromBase64String(cipher);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);

            //decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, saltBytes, DataProtectionScope.LocalMachine);
            return Encoding.Unicode.GetString(decrypted);
        }

        /// <summary>
        /// CreateHashedText
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="useSalt"></param>
        /// <returns></returns>
        public static KeyValuePair<string,string> CreateHashedText(string plainText,bool useSalt)
        {
            var plainTextBytes = Encoding.Unicode.GetBytes(plainText);

            string salt = string.Empty;
            string hashedText = string.Empty;
            if (useSalt)
            {
                var saltBytes = new byte[0x10];
                using (var random = new RNGCryptoServiceProvider())
                {
                    random.GetBytes(saltBytes);
                }
                
                salt = Convert.ToBase64String(saltBytes);
                plainTextBytes=saltBytes.Concat(plainTextBytes).ToArray();
            }
            
            byte[] hashBytes;
            using (var hashAlgorithm = HashAlgorithm.Create())
            {
                hashBytes = hashAlgorithm.ComputeHash(plainTextBytes);
            }
            
            hashedText = Convert.ToBase64String(hashBytes);

            return new KeyValuePair<string, string>(hashedText, salt);

        }

        /// <summary>
        /// ValidateHashedText
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <param name="hashedText"></param>
        /// <returns></returns>
        public static bool ValidateHashedText(string plainText,string salt,string hashedText)
        {

            var saltBytes = Convert.FromBase64String(salt);

            var plainTextBytes = Encoding.Unicode.GetBytes(plainText);

            plainTextBytes = saltBytes.Concat(plainTextBytes).ToArray();

            byte[] hashBytes;
            using (var hashAlgorithm = HashAlgorithm.Create())
            {
                hashBytes = hashAlgorithm.ComputeHash(plainTextBytes);
            }

            return hashedText == Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// ValidateHashedText
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="hashedText"></param>
        /// <returns></returns>
        public static bool ValidateHashedText(string plainText, string hashedText)
        {

            var plainTextBytes = Encoding.Unicode.GetBytes(plainText);

            byte[] hashBytes;
            using (var hashAlgorithm = HashAlgorithm.Create())
            {
                hashBytes = hashAlgorithm.ComputeHash(plainTextBytes);
            }

            return hashedText == Convert.ToBase64String(hashBytes);
        }
        

    }
}
