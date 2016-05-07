using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Курсовая
{
    /// <summary>
    /// класс содержащий методы: создания ключей,хэш образов паролей, шифрования и расшифрования сообщений
    /// </summary>
    class rs
    {
        public static string pubkey = "";
        public static string privkey = "";
        public static int keyid = 0;
        /// <summary>
        /// Генерация ключей и присвоение им уникального номера
        /// </summary>
        public static void GenerateKeys()
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                pubkey = RSA.ToXmlString(false);
                privkey = RSA.ToXmlString(true);
                Random rnd = new Random();
                keyid = rnd.Next(111111, 999999);
            }
        }
        /// <summary>
        /// Метод для создания шифрограммы
        /// </summary>
        /// <param name="msg">принимает на вход сообщение</param>
        /// <returns>возвращает шифрограмму</returns>
        public static string Encrypt(string msg)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                UTF8Encoding ByteConverter = new UTF8Encoding();
                byte[] encryptedData = null;
                byte[] DataToEncrypt = ByteConverter.GetBytes(msg);
                RSA.FromXmlString(pubkey);
                encryptedData = RSA.Encrypt(DataToEncrypt, false);
                string result = Convert.ToBase64String(encryptedData);
                return result;
            }
        }
        /// <summary>
        /// Метод для расшифровки шифрограммы
        /// </summary>
        /// <param name="msg">принимает на вход шифрограмму</param>
        /// <returns>Возвращает сообщение</returns>
        public static string Decrypt(string msg)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                UTF8Encoding ByteConverter = new UTF8Encoding();
                byte[] decryptedData = null;
                byte[] DataToDecrypt = Convert.FromBase64String(msg);
                RSA.FromXmlString(privkey);
                decryptedData = RSA.Decrypt(DataToDecrypt, false);
                string result = ByteConverter.GetString(decryptedData);
                return result;
            }
        }
        /// <summary>
        /// Метод создания хэш образа из пароля
        /// </summary>
        /// <param name="myDataEncoded">принимает на вход строку</param>
        /// <returns>возвращает хеш образ</returns>
        public static string hashcode(string myDataEncoded)
        {

            SHA1 sha = SHA1.Create();
            byte[] bytes = new ASCIIEncoding().GetBytes(myDataEncoded);
            sha.ComputeHash(bytes);
            myDataEncoded = Convert.ToBase64String(sha.Hash);
            return myDataEncoded;
        }
    }
}
