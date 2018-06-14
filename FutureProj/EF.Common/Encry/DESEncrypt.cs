using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EF.Common
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class DESEncrypt
    {
        /// <summary>
        /// 管理员登陆信息加密key
        /// </summary>
        public const string login = "admin(info)%info=(admin)";
        /// <summary>
        /// 用户资料加密key
        /// </summary>
        public const string info = "loga(b)*logb(a)=log(abc)";
        /// <summary>
        /// SFTP加密key
        /// </summary>
        public const string sftp = "sftp=s+f+t+p&yes&no";
        /// <summary>
        /// 模拟账号加密key
        /// </summary>
        public const string moni = "moni&Simulate%(zhanghao)";
        /// <summary>
        /// 数据库连接方式加密key
        /// </summary>
        public const string connection = "sql_connection&very+(Trouble)";
        /// <summary>
        /// 数据库加密
        /// </summary>
        public const string newcon = "sql_@00&conect%@!(lgCan)";
        public DESEncrypt() { }

        #region ========加密========
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "sql_connection&very+(Trouble)");
        }

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            if (string.IsNullOrEmpty(Text)) return string.Empty;

            var des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(MD5Encrypt.MD5(sKey,Encoding.UTF8).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(MD5Encrypt.MD5(sKey, Encoding.UTF8).Substring(0, 8));
            var ms = new System.IO.MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            var ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        #endregion

        #region ========解密========
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "sql_connection&very+(Trouble)");
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string text, string sKey)
        {
            try
            {
                if (string.IsNullOrEmpty(text)) return string.Empty;

                var des = new DESCryptoServiceProvider();
                int len;
                len = text.Length / 2;
                byte[] inputByteArray = new byte[len];
                int x, i;
                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                des.Key = Encoding.ASCII.GetBytes(MD5Encrypt.MD5(sKey, Encoding.UTF8).Substring(0, 8));
                des.IV = Encoding.ASCII.GetBytes(MD5Encrypt.MD5(sKey, Encoding.UTF8).Substring(0, 8));
                var ms = new System.IO.MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
