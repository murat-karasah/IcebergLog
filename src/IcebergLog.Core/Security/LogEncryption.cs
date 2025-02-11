using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IcebergLog.Core.Security
{
    public static class LogEncryption
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("MySuperSecretKey123!"); // 16 byte key
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("MySuperSecretIV123!");

        public static string EncryptLog(string logMessage)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(logMessage);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public static string DecryptLog(string encryptedLog)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using var msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedLog));
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}
