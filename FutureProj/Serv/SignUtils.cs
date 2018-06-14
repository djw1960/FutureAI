using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Serv
{
    public class SignUtils
    {
        #region RSA签名验签

        #endregion

        #region 签名+加密

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static string MD5(string srcString, Encoding encoding)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(encoding.GetBytes(srcString));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// use sha1 to encrypt string
        /// </summary>
        public static string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
        /// <summary>
        /// SHA1加密算法
        /// </summary>
        /// <param name="signstr"></param>
        /// <returns></returns>
        public static string SHA1(string signstr)
        {
            var sha = System.Security.Cryptography.SHA1.Create();
            var hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(signstr));
            return BitConverter.ToString(hashed).Replace("-", "");
        }
        #endregion

        #region 数据处理
        /// <summary>
        /// KEY-VALUE值转字典
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDicFromString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("字符串不能为空");
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] arr = str.Split('&');
            foreach (string item in arr)
            {
                string[] temp = item.Split('=');
                if (temp.Length < 2)
                {
                    dic.Add(temp[0].Trim(), "");
                }
                else
                {
                    dic.Add(temp[0].Trim(), temp[1].Trim());
                }
            }
            return dic;
        }
        /// <summary>
        /// KEY-VALUE值转字典
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static SortedDictionary<string, string> GetDicFromStringSort(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("字符串不能为空");
            }
            SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
            string[] arr = str.Split('&');
            foreach (string item in arr)
            {
                string[] temp = item.Split('=');
                if (temp.Length < 2)
                {
                    dic.Add(temp[0].Trim(), "");
                }
                else
                {
                    dic.Add(temp[0].Trim(), temp[1].Trim());
                }
            }
            return dic;
        }
        /// <summary>
        /// 字符串转换为Dictionary字典数据类型
        /// </summary>
        /// <param name="str">传入可以转换为Dictionary字典类型的字符串</param>
        /// <returns>返回Dictionary字典</returns>
        public static Dictionary<string, string> dicStringToDict(string str)
        {
            Dictionary<string, string> resData = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(str))
            {
                string[] split = str.Split('&');
                for (int i = 0; i < split.Length; i++)
                {
                    int pos = split[i].IndexOf('=');
                    if (pos > 0)
                    {
                        if (pos < split[i].Length - 1)
                        {
                            resData.Add(split[i].Substring(0, pos), split[i].Substring(pos + 1));
                        }
                        else
                        {
                            resData.Add(split[i].Substring(0, pos), "");
                        }
                    }
                }
            }

            return resData;
        }
        /// <summary>
        /// 生成签名字符串--非标准ASCII排序
        /// 不可传入非签名字段
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string GetSignStr(Dictionary<string, string> parameters, string splitChar = "&")
        {
            var items = from pair in parameters
                        orderby pair.Key ascending
                        where !string.IsNullOrEmpty(pair.Key) && !pair.Value.Equals(string.Empty)
                        select pair;
            var result = new StringBuilder();
            foreach (var item in items)
            {
                result.AppendFormat("{0}={1}{2}", item.Key, item.Value, splitChar);
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        /// <summary>
        /// 生成签名字符串，不排序
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string GetSignStrNoSort(Dictionary<string, string> parameters, string splitChar = "&")
        {
            var result = new StringBuilder();
            foreach (var item in parameters)
            {
                result.AppendFormat("{0}={1}{2}", item.Key, item.Value, splitChar);
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static SortedDictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            SortedDictionary<string, string> dicArray = new SortedDictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                string tempKey = temp.Key.ToLower();
                if ((tempKey.Equals("signature") || tempKey.Equals("sign") || tempKey.Equals("sign_type") || tempKey.Equals("signType") || string.IsNullOrEmpty(temp.Value)))
                {
                    continue;
                }
                else
                {
                    dicArray.Add(temp.Key, temp.Value);
                }
            }
            return dicArray;
        }
        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="sArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(SortedDictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }
            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            return prestr.ToString();
        }
        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="sArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }
            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            return prestr.ToString();
        }
        /// <summary>
        /// ASCII值排序
        /// </summary>
        public class OrdinalComparer : System.Collections.Generic.IComparer<String>
        {
            public int Compare(String x, String y)
            {
                return string.CompareOrdinal(x, y);
            }
        }
        /// <summary>
        /// 生成签名字符串--标准ASCII排序
        /// 不可传入非签名字段
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string GetASCIISignStr(Dictionary<string, string> dic)
        {
            //按照ASCII排序之后的字典
            var arr = dic.OrderBy(k => k.Key, new OrdinalComparer()).ToDictionary(x => x.Key, y => y.Value);
            StringBuilder query = new StringBuilder();
            bool hasParam = false;
            foreach (string eachkey in dic.Keys)
            {
                string value = "";
                dic.TryGetValue(eachkey, out value);
                // 除sign、signType字段
                if (!string.IsNullOrEmpty(eachkey) && !string.IsNullOrEmpty(value))
                {
                    if (hasParam)
                    {
                        query.Append("&");
                    }
                    else
                    {
                        hasParam = true;
                    }
                    query.Append(eachkey).Append("=").Append(value);
                }
            }
            return query.ToString();

        }
        /// <summary>
		/// 除去数组中的空值和签名参数并以字母a到z的顺序排序
		/// </summary>
		/// <param name="dicArrayPre">过滤前的参数组</param>
		/// <returns>过滤后的参数组</returns>
		public static Dictionary<string, string> FilterPara(Dictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                string tempKey = temp.Key.ToLower();
                if ((tempKey.Equals("signature") || tempKey.Equals("sign") || tempKey.Equals("sign_type") || tempKey.Equals("signType") || string.IsNullOrEmpty(temp.Value)))
                {
                    continue;
                }
                else
                {
                    dicArray.Add(temp.Key, temp.Value);
                }
            }
            return dicArray;
        }
        /// <summary>
        /// 有序字典（最简单的有序字典）转为json
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string DictToJson(Dictionary<string, string> dict)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");
            foreach (KeyValuePair<string, string> temp in dict)
            {
                json.Append("\"" + temp.Key + "\"" + ":" + "\"" + temp.Value + "\"");
                json.Append(",");
            }
            json.Remove(json.Length - 1, 1);
            json.Append("}");
            string content = json.ToString();
            return content;
        }
        /// <summary>
		/// 字符串转换为Dictionary字典数据类型
		/// </summary>
		/// <param name="str">传入可以转换为Dictionary字典类型的字符串</param>
		/// <returns>返回Dictionary字典</returns>
		public static SortedDictionary<string, string> dicStrToDict(string str)
        {
            SortedDictionary<string, string> resData = new SortedDictionary<string, string>();
            if (!string.IsNullOrEmpty(str))
            {
                string[] split = str.Split('&');
                for (int i = 0; i < split.Length; i++)
                {
                    int pos = split[i].IndexOf('=');
                    if (pos > 0)
                    {
                        if (pos < split[i].Length - 1)
                        {
                            resData.Add(split[i].Substring(0, pos), split[i].Substring(pos + 1));
                        }
                        else
                        {
                            resData.Add(split[i].Substring(0, pos), "");
                        }
                    }
                }
            }

            return resData;
        }
        #endregion
    }
    /// <summary>
    /// RSA
    /// </summary>
    public static class RSASignUtils
    {
        //商户私钥签名
        public static string RSASign(string signStr, string privateKey)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                RSAParameters para = new RSAParameters();
                rsa.FromXmlString(privateKey);
                byte[] signBytes = rsa.SignData(UTF8Encoding.UTF8.GetBytes(signStr), "md5");
                return Convert.ToBase64String(signBytes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //RSA私钥格式转换
        public static string RSAPrivateKeyJava2DotNet(string privateKey)
        {
            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
            return string.Format(
                "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned())
            );
        }

        //使用智付公钥验签
        public static bool ValidateRsaSign(string plainText, string publicKey, string signedData)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                RSAParameters para = new RSAParameters();
                rsa.FromXmlString(publicKey);
                return rsa.VerifyData(UTF8Encoding.UTF8.GetBytes(plainText), "md5", Convert.FromBase64String(signedData));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //智付公钥格式转换
        public static string RSAPublicKeyJava2DotNet(string publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format(
                "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned())
            );
        }

        //使用智付公钥加密
        public static string RSAEncrypt(string encrypt_info, string publicKey)
        {
            try
            {
                byte[] dataBytes = UTF8Encoding.UTF8.GetBytes(encrypt_info);
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                RSAParameters para = new RSAParameters();
                rsa.FromXmlString(publicKey);
                return Convert.ToBase64String(rsa.Encrypt(dataBytes, false));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
