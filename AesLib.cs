using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace libAES
{
    /// <summary>
    /// AES Encryption and Decryption Library
    /// </summary>
    public static class AesLib
    {

        /// <summary>
        /// Encrypt text with password
        /// </summary>
        /// <param name="text">Plain Text</param>
        /// <param name="password">Secure Password</param>
        /// <returns>Encrypyed base64 string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string EncryptText(string text, string password)
        {
            // Check arguments.
            if (text == null || text.Length <= 0)
            {
                throw new ArgumentNullException("text");
            }
            if (password == null || password.Length <= 0)
            {
                throw new ArgumentNullException("password");
            }
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                using (SHA256 sha = SHA256.Create())
                {
                    aesAlg.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
                using (MD5 md = MD5.Create())
                {
                    aesAlg.IV = md.ComputeHash(Encoding.UTF8.GetBytes(password));
                }

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(text);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
                // Return the encrypted bytes from the memory stream.
                return Convert.ToBase64String(encrypted);
            }
        }

        /// <summary>
        /// Decrypt, encrypted text
        /// </summary>
        /// <param name="encryptedBase64String">Encrypted Base64 String</param>
        /// <param name="password">Password of ciphertext</param>
        /// <returns>Decryped text</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string DecryptText(string encryptedBase64String, string password)
        {
            // Check arguments.
            if (encryptedBase64String == null || encryptedBase64String.Length <= 0)
            {
                throw new ArgumentNullException("encryptedBase64String");
            }
            if (password == null || password.Length <= 0)
            {
                throw new ArgumentNullException("password");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                using (SHA256 sha = SHA256.Create())
                {
                    aesAlg.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
                using (MD5 md = MD5.Create())
                {
                    aesAlg.IV = md.ComputeHash(Encoding.UTF8.GetBytes(password));
                }

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedBase64String)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
