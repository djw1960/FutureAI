using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;

namespace Serv.Helper
{
    /// <summary>
    /// json类型帮忙类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// C#将Json字符串反序列化成List对象类集合
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="JsonStr">json字符串</param>
        /// <returns></returns>
        public static List<T> JSONStringToList<T>(this string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
            return objs;
        }
        /// <summary>
        /// 反序列化成对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
        /// <summary>
        /// 字符串转换为Dictionary字典数据类型
        /// </summary>
        /// <param name="str">传入可以转换为Dictionary字典类型的字符串</param>
        /// <returns>返回Dictionary字典</returns>
        public static Dictionary<string, string> dicStrToDict(string str)
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
        /// 有序字典（最简单的有序字典）转为json
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string dictToJson(Dictionary<string, string> dict)
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
    }
}
