using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Console
{
    public class EasyEncrypt
    {
        /// <summary>
        /// 
        /// </summary>
        private static string[] dicS = new string[] { "E", "B", "Q", "D", "A", "F", "G", "H", "J", "K" };
        private const int key = 7;

        /// <summary>
        /// 简单数字加密-整形数字简单的一个加密/解密算法
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ECode(int num)
        {
            var result = string.Empty;
            try
            {
                //对数字进行加密解密处理每个数位上的数字变为与7乘积的个位数字，再把每个数位上的数字a变为10-a．
                string temp = string.Empty;
                if (num > 0)
                    temp = num.ToString();
                for (int i = 0; i < temp.Length; i++)
                {
                    int t = Convert.ToInt32(temp[i].ToString());
                    if (i % 2 == 0)
                    {
                        result += dicS[(10 - (t * key % 10)) % 10];
                    }
                    else
                    {
                        result += (10 - (t * key % 10)) % 10;
                    }
                }
            }
            catch (Exception ex) { }
            return result;
        }

        /// <summary>
        /// 数字解密-整形数字简单的一个加密/解密算法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static int DCode(string code)
        {
            string result = string.Empty;
            try
            {
                //对数字进行解密处理，把每个数位上的数字乘以7再进行与10求余
                if (string.IsNullOrEmpty(code))
                    return 0;
                for (int i = 0; i < code.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        int t = GetIndex(code[i].ToString());

                        result += t * key % 10;
                    }
                    else
                    {
                        int t = Convert.ToInt32(code[i].ToString());
                        result += t * key % 10;
                    }
                }
            }
            catch (Exception ex) { }
            int num = 0;
            int.TryParse(result, out num);
            return num;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static int GetIndex(string c)
        {
            for (int i = 0; i < dicS.Length; i++)
            {
                if (dicS[i] == c)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
