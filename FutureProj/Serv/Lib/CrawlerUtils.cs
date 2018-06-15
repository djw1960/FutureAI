﻿using EF.Entitys;
using HtmlAgilityPack;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serv.Lib
{
    public class CrawlerUtils
    {
        public static string GetWebClient(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, Encoding.UTF8);//注意编码
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
        #region 获取公告通知，新闻
        /// <summary>
        /// 获取公告，新闻信息
        /// </summary>
        /// <returns></returns>
        public static List<FNews> GetNews(string url)
        {
            //下载网页源代码 
            var docText = GetWebClient(url);
            //加载源代码，获取文档对象
            var doc = new HtmlDocument(); doc.LoadHtml(docText);
            //更加xpath获取总的对象，如果不为空，就继续选择dl标签
            var res = doc.DocumentNode.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/table[1]/tbody[1]");
            if (res != null)
            {
                var list = res.SelectNodes(@"tr");//获取所有的表格行
            }
            return new List<FNews>();
        } 
        #endregion

        #region 仓单信息获取
        /// <summary>
        /// 01-获取大商所仓单块信息
        /// </summary>
        /// <returns></returns>
        public static FDataReposInit GetDSFDataRepository_First(string url)
        {
            //下载网页源代码 
            var docText = GetWebClient(url);
            //加载源代码，获取文档对象
            var doc = new HtmlDocument(); doc.LoadHtml(docText);
            //更加xpath获取总的对象，如果不为空，就继续选择dl标签 
            HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"wbillWeeklyQuotesForm\"]/div/div[2]/p[2]/span");
            string date = "";
            if (titleNode!=null)
            {
                var title = titleNode.InnerText;//查询日期：20180614 仓单数据在每日下午4点生成
                date ="20"+MatchSubString(title, "20", 1, 6);
            }
            var res = doc.DocumentNode.SelectSingleNode("//*[@id=\"printData\"]/div/table");
            if (res != null)
            {
                FDataReposInit model = new FDataReposInit();
                model.AddDate = DateTime.Now;
                model.Content = Regex.Replace(res.OuterHtml, @"\s", "");//去掉空格
                int d = 0; int.TryParse(date, out d);
                model.Date = d;
                model.IsCheckFinish = 0;
                model.Type = InitContentType.Cangdan.ToString();
                return model;
            }
            return null;
        }
        /// <summary>
        /// 02-解析大商仓单详情信息
        /// </summary>
        /// <returns></returns>
        public static List<FDataRepository> GetDSFDataRepository_Second(string table, int date)
        {

            List<FDataRepository> list = new List<FDataRepository>();
            //加载源代码，获取文档对象
            var tnode = HtmlNode.CreateNode(table);
            if (tnode != null)
            {
                var trlist = tnode.SelectNodes(@"tr");//获取所有的表格行
                Regex regex = new Regex(@"<td.*?>[\s\S]*?<\/td>");
                //每一行都是一个对象
                foreach (var tr in trlist)
                {
                    //<tr><td>豆一</td><tdstyle="font-weight:bold">良运库</td><td>5725</td><td>5725</td><td>0</td></tr>
                    MatchCollection mc = regex.Matches(tr.InnerText);
                    if (mc.Count>0)
                    {
                        FDataRepository model = new FDataRepository();
                        model.Date = date;
                        model.CateName = mc[0].ToString();
                        model.Reps = mc[1].ToString();
                        model.YTDSum =Convert.ToInt32(mc[2]);
                        model.TDSum = Convert.ToInt32(mc[3]);
                        model.Change = Convert.ToInt32(mc[4]);
                        model.DTime = DateTime.Now;
                        list.Add(model);
                    }
                    else break;
                }
            }
            return list;
        }
        #endregion

        #region 正则
        /// <summary>
        /// 统计源字符串中包含多少目标字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="dest">目标字符串</param>
        /// <returns>出现次数</returns>
        private static int MatchSubString(string source, string dest)
        {
            MatchCollection mc = Regex.Matches(source, dest);
            return mc.Count;
        }

        /// <summary>
        /// 获取目标字符串在源字符串中指定出现次数的索引
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="dest">目标字符串</param>
        /// <param name="ordinalNumber">出现序数</param>
        /// <returns>出现位置索引</returns>
        private static int MatchSubString(string source, string dest, int ordinalNumber)
        {
            MatchCollection mc = Regex.Matches(source, dest);
            if (mc.Count < ordinalNumber)
            {
                return -1;
            }
            else
            {
                return mc[ordinalNumber - 1].Index;
            }
        }

        /// <summary>
        /// 获取源字符串中在指定序数的目标字符串之后的字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="dest">目标字符串</param>
        /// <param name="ordinalNumber">出现序数</param>
        /// <param name="returnStringlength">返回字符串的长度</param>
        /// <returns>获取的字符串，不成功为null</returns>
        private static string MatchSubString(string source, string dest, int ordinalNumber, int returnStringlength)
        {
            int pos = MatchSubString(source, dest, ordinalNumber);
            if (pos != -1)
            {
                return source.Substring(pos + dest.Length, returnStringlength);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
