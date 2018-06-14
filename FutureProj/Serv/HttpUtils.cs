using EF.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Serv
{
    public class HttpUtils
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        /// <summary>
        /// post请求到指定地址并获取返回的信息内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求参数</param>
        /// <param name="encodeType">编码类型如：UTF-8</param>
        /// <returns>返回响应内容</returns>
        public static string HttpPost(string URL, string strPostdata, string strEncoding = "UTF-8")
        {
            System.Net.ServicePointManager.Expect100Continue = false;//默认是true，所以导致错误
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = encoding.GetBytes(strPostdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(strEncoding)))
            {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// 使用代理服务器发起请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="strPostdata"></param>
        /// <param name="ProxyStr"></param>
        /// <param name="strEncoding"></param>
        /// <returns></returns>
        public static string HttpPostWebProxy(string url, string strPostdata, string ProxyStr, string strEncoding = "UTF-8")
        {
            string html = string.Empty;
            try
            {
                //代理暂无法直接处理https请求
                if (!string.IsNullOrEmpty(ProxyStr)&&url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    url = url.Replace("https", "http");
                }
                HttpWebRequest WebReques = (HttpWebRequest)HttpWebRequest.Create(url);
                WebReques.Method = "POST";
                WebReques.Accept = "text/html, application/xhtml+xml, */*";
                WebReques.ContentType = "application/x-www-form-urlencoded";
                WebReques.Timeout = 120000;
                if (!string.IsNullOrEmpty(ProxyStr)&& ProxyStr.Trim().Length>0)
                {
                    LogHelper.Info<HttpUtils>("ProxyStr:{0}",ProxyStr);
                    WebProxy proxy = new WebProxy(ProxyStr,true);
                    WebReques.Proxy = proxy;
                }
                Encoding encoding = Encoding.UTF8;
                byte[] buffer = encoding.GetBytes(strPostdata);
                WebReques.ContentLength = buffer.Length;
                WebReques.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse WebRespon = (HttpWebResponse)WebReques.GetResponse();
                if (WebRespon != null)
                {
                    StreamReader sr = new StreamReader(WebRespon.GetResponseStream(), System.Text.Encoding.GetEncoding(strEncoding));
                    html = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                    WebRespon.Close();
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error<HttpUtils>(ex);
                html = "err";
            }
            return html;
        }
        /// <summary>
        /// post请求到指定地址并获取返回的信息内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求参数</param>
        /// <param name="encodeType">编码类型如：UTF-8</param>
        /// <returns>返回响应内容</returns>
        public static string HttpPost_JSON(string URL, string strPostdata, string strEncoding = "UTF-8")
        {
            System.Net.ServicePointManager.Expect100Continue = false;//默认是true，所以导致错误
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            byte[] buffer = encoding.GetBytes(strPostdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(strEncoding)))
            {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// GBK Post请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string HttpPost_GBK(string Url, string postDataStr)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";
            // request.ContentType = "application/xml;charset=utf-8";
            // request.ContentLength = postDataStr.Length;  

            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.GetEncoding("GBK"));

            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                //encoding = "UTF-8"; //默认编码  
                encoding = "GBK"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GBK"));
            string retString = reader.ReadToEnd();
            return retString;
        }
        /// <summary>
		/// 以GET方式抓取远程页面内容
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <returns></returns>
		public static string GetHttpResponse(string url)
        {
            string strResult;
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(url);
                hwr.Timeout = 19600;
                hwr.Method = "GET";
                hwr.UserAgent = DefaultUserAgent;
                HttpWebResponse hwrs = (HttpWebResponse)hwr.GetResponse();
                Stream myStream = hwrs.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
                StringBuilder sb = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    sb.Append(sr.ReadLine() + "\r\n");
                }
                strResult = sb.ToString();
                hwrs.Close();
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return strResult;
        }
        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dicPara">请求参数数组（已处理过的参数）</param>
        /// <returns>提交表单HTML文本</returns>
        public static string BuildForm(Dictionary<string, string> dicPara, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<form name=\"payForm\" id=\"payForm\" method=\"post\" action=\"{0}\">", url);
            foreach (var item in dicPara)
            {
                sb.AppendFormat("<input name = \"{0}\" id=\"{0}\" type = \"hidden\" value = \"{1}\" />", item.Key, item.Value);
            }
            sb.AppendFormat("<input type='submit' value='确定' style='display:none;'></form >");
            sb.Append("<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('payForm').submit();\",10);</script>");

            return sb.ToString();
        }
        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dicPara">请求参数数组（已处理过的参数）</param>
        /// <returns>提交表单HTML文本</returns>
        public static string BuildForm(SortedDictionary<string, string> dicPara, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<form name=\"payForm\" id=\"payForm\" method=\"post\" action=\"{0}\">", url);
            foreach (var item in dicPara)
            {
                sb.AppendFormat("<input name = \"{0}\" id=\"{0}\" type = \"hidden\" value = \"{1}\" />", item.Key, item.Value);
            }
            sb.AppendFormat("<input type='submit' value='确定' style='display:none;'></form >");
            sb.Append("<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('payForm').submit();\",10);</script>");

            return sb.ToString();
        }
        /// <summary>
		/// 建立请求，以表单HTML形式构造（默认）
		/// </summary>
		/// <param name="url"></param>
		/// <param name="dicPara">请求参数数组（已处理过的参数）</param>
		/// <returns>提交表单HTML文本</returns>
		public static string BuildRequest(string url, SortedDictionary<string, string> dicPara)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<form id='reqpaysubmit' name='reqpaysubmit' action='" + url + "' method='POST'>");
            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='确定' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['reqpaysubmit'].submit();</script>");
            return sbHtml.ToString();
        }
        /// <summary>
		/// 建立请求，以表单HTML形式构造（默认）
		/// </summary>
		/// <param name="url"></param>
		/// <param name="dicPara">请求参数数组（对参数不处理）</param>
		/// <returns>提交表单HTML文本</returns>
        public static string BuildRequest(string url, Dictionary<string, string> dicPara)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<form id='reqpaysubmit' name='reqpaysubmit' action='" + url + "' method='POST'>");
            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='确定' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['reqpaysubmit'].submit();</script>");
            return sbHtml.ToString();
        }
        /// <summary>
        /// 使用通过给定格式化程序序列化的指定值，以异步操作方式发送 POST 请求。
        /// 处理Json类型返回
        /// </summary>
        /// <typeparam name="T"> 要序列化的对象的类型。</typeparam>
        /// <param name="client">用于发出请求的客户端。</param>
        /// <param name="requestUri">请求将发送到的 URI。</param>
        /// <param name="value">要写入到请求的实体正文的值。</param>
        /// <returns> 一个表示异步操作的任务对象。</returns>
        public static M PostAsync<T,M>(string requestUri, T value)
        {
            var requestJson = JsonConvert.SerializeObject(value);
            HttpContent httpContent = new StringContent(requestJson);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string responseJson = string.Empty;
            
            using (var httpClient = new HttpClient())
            {
                responseJson = httpClient.PostAsync(requestUri, httpContent)
                    .Result.Content.ReadAsStringAsync().Result;

            }
            var result = JsonConvert.DeserializeObject<M>(responseJson);
            return result;
        }
    }
    public class Base64Util
    {
        public static string EncodeBase64(Encoding encode, string source)
        {
            byte[] bytes = encode.GetBytes(source);
            String resutl = "";
            try
            {
                resutl = Convert.ToBase64String(bytes);
            }
            catch
            {
                resutl = source;
            }
            return resutl;
        }

        public static string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
    }
}
