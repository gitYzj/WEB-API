using System;
using System.IO;
using System.Security.Cryptography;

namespace WEB_API.Tools
{
    public class Crypto
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="PlainText"></param>
        /// <returns></returns>
        public static string Encrypt(string PlainText)
        {
            string KEY_64 = "dafei250";
            string IV_64 = "DAFEI500";

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(PlainText);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="CypherText"></param>
        /// <returns></returns>
        public static string Decrypt(string CypherText)
        {
            //string KEY_64 = "haeren55"; //必须是8个字符(64Bit) 
            //string IV_64 = "HAEREN55";  //必须8个字符(64Bit) 
            string KEY_64 = "dafei250";
            string IV_64 = "DAFEI500";

            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                byte[] byEnc;
                try
                {
                    byEnc = Convert.FromBase64String(CypherText);
                }
                catch
                {
                    return null;
                }
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(byEnc);
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cst);
                return sr.ReadToEnd();
            }
            catch { return "无法解密!"; }
        }
    }
}
