using EF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Serv
{
    public class Common
    {
        /// <summary>
        /// 
        /// </summary>
        public static string GUID
        {
            get
            {
                return Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            }
        }
        /// <summary>
        /// 生成订单唯一标识
        /// 64个字符长度
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string BillToken()
        {
            return GUID+ DESEncrypt.Encrypt(DateTime.Now.ToString("yyyyMMddHHmmssfff"), DESEncrypt.info);
        }

        private static object BillLock = new object();
        /// <summary>
        ///  生成一定规则的订单号--数字
        /// </summary>
        /// <param name="len">订单号长度</param>
        /// <returns></returns>
        public static string GetBillNo(int len)
        {
            lock (BillLock)
            {
                string result = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                int n = len - result.Length;
                Random r = new Random(unchecked((int)DateTime.Now.Ticks));
                result = result + GetRandom(r, n);
                return result;
            }
            
        }
        private static string GetRandom(Random r, int len)
        {
            string num = "";
            System.Threading.Thread.Sleep(6);
            for (int i = 0; i < len; i++)
            {
                num += r.Next(0,9);
            }
            return num;
        }

        /// <summary>
        /// 得到ip
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            var userHostAddress = string.Empty;
            try
            {
                //userHostAddress = GetIPBySohu();
                //if (userHostAddress.Length > 0) return userHostAddress;

                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(userHostAddress))
                    {
                        userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    if (string.IsNullOrEmpty(userHostAddress))
                    {
                        userHostAddress = HttpContext.Current.Request.UserHostAddress;
                    }
                    if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
                    {
                        return userHostAddress;
                    }
                    return "127.0.0.1";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<Common>(ex);
                userHostAddress = string.Empty;
            }
            return userHostAddress;
        }
        /// <summary>
        /// 验证ip
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIP(string value)
        {
            return Regex.IsMatch(value, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        /// <summary>
        /// 随机抽取
        /// </summary>
        /// <param name="max"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static int[] GetRandomSequence2(int max, int N)
        {

            int[] sequence = new int[max];
            int[] output = new int[N];

            for (int i = 0; i < max; i++)
            {
                sequence[i] = i;
            }
            //System.Threading.Thread.Sleep(5);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            
            int end = max - 1;

            for (int i = 0; i < N; i++)
            {
                int num = random.Next(end);
                output[i] = sequence[num];
                sequence[num] = sequence[end];
                end--;
            }

            return output;
        }

    }
}
