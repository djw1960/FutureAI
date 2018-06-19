using EF.Entitys;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Serv.Lib
{
    /// <summary>
    /// 
    /// </summary>
    public class CrawlerUtils
    {
        /// <summary>
        /// 大连商品交易所
        /// </summary>
        const string site = "http://www.dce.com.cn";
        /// <summary>
        /// 业务通知页面地址
        /// </summary>
        const string businessUrl = "/dalianshangpin/yw/fw/jystz/ywtz/13305-{0}.html";
        /// <summary>
        /// 交易所新闻页面地址
        /// </summary>
        const string exchangeUrl = "/dalianshangpin/xwzx93/jysxw/13363-{0}.html";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetWebClient(string url,Encoding encode=null)
        {
            var strHTML = string.Empty;
            try
            {
                WebClient myWebClient = new WebClient();
                Stream myStream = myWebClient.OpenRead(url);
                Encoding encd = encode == null ? Encoding.UTF8 : encode;
                StreamReader sr = new StreamReader(myStream, encd);//注意编码
                strHTML = sr.ReadToEnd();
                myStream.Close();
            }
            catch { /* TODO */ }
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
        #region 郑州商品交易所仓单

        /// <summary>
        /// 郑州商品交易所仓单
        /// </summary>
        /// <param name="url"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static FDataReposInit GetZZFDataRepository_First(string url, int date)
        {
            //下载网页源代码 
            var docText = GetWebClient(url,Encoding.Default);
            //加载源代码，获取文档对象
            var doc = new HtmlDocument(); doc.LoadHtml(docText);
            HtmlNode res = null;
            if (date < 20151001)
            {
                res = doc.DocumentNode.SelectSingleNode("//*[@id=\"toexcel\"]");
            }
            else
            {
                res = doc.DocumentNode.SelectSingleNode("/html/body/table");
            }
            if (res != null)
            {
                FDataReposInit model = new FDataReposInit();
                model.TradeHouse = TradeHouseType.czce.ToString();
                model.AddDate = DateTime.Now;
                model.Content = Regex.Replace(Regex.Replace(res.OuterHtml, @"[\f\n\r\t\v]", ""), @" +", " ");//去掉空格
                string temp = Regex.Replace(model.Content, @"(<table .+?>)", "<table>");
                temp = Regex.Replace(temp, @"(<div .+?>)", "<div>");
                temp = Regex.Replace(temp, @"(<col .+?>)", "<col>");
                temp = Regex.Replace(temp, @"(<tr .+?>)", "<tr>");
                temp = Regex.Replace(temp, @"(<td .+?>)", "<td>");
                temp = temp.Replace("&nbsp;", "");
                model.Content = temp;
                model.Date = date;
                model.IsCheckFinish = 0;
                model.Type = InitContentType.Cangdan.ToString();
                return model;
            }
            return null;
        }
        /// <summary>
        /// 郑商所仓单解析
        /// </summary>
        /// <param name="table"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<FDataRepository> GetZZFDataRepository_Second(string table, int date)
        {
            List<FDataRepository> list = new List<FDataRepository>();

            if (date > 20151001)
            {
                #region 20151001-2018

                #endregion
            }
            else
            {
                #region 20110104-20151001

                #endregion
            }

            //加载源代码，获取文档对象
            var tnode = HtmlNode.CreateNode(table);
            if (tnode != null)
            {
                var trlist = tnode.SelectNodes(@"tr");//获取所有的表格行
                Regex regex = new Regex(@"<td.*?>[\s\S]*?<\/td>");
                //每一行都是一个对象
                foreach (var tr in trlist)
                {
                    MatchCollection mc = regex.Matches(tr.InnerHtml);
                    if (mc.Count > 0 && !string.IsNullOrEmpty(HtmlNode.CreateNode(mc[0].ToString()).InnerText))
                    {
                        FDataRepository model = new FDataRepository();
                        model.TradeHouse = TradeHouseType.dce.ToString();
                        model.Date = date;
                        model.CateName = HtmlNode.CreateNode(mc[0].ToString()).InnerText;
                        model.Reps = HtmlNode.CreateNode(mc[1].ToString()).InnerText;
                        model.Type = 1;
                        if (model.CateName.Contains("小计"))
                        {
                            model.Type = 2;//统计
                            model.Reps = "ALL";
                        }
                        else if (model.CateName.Contains("总计"))
                        {
                            model.Type = 3;//统计
                            model.Reps = "ALL";
                        }
                        model.YTDSum = Convert.ToInt32(HtmlNode.CreateNode(mc[2].ToString()).InnerText);
                        model.TDSum = Convert.ToInt32(HtmlNode.CreateNode(mc[3].ToString()).InnerText);
                        model.Change = Convert.ToInt32(HtmlNode.CreateNode(mc[4].ToString()).InnerText);
                        model.DTime = DateTime.Now;
                        list.Add(model);
                    }
                    else continue;
                }
            }
            return list;
        }
        #endregion

        #region 上海交易所仓单信息
        /// <summary>
        /// 处理2011年-2014年5月16日之间的数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static FDataReposInit GetSHFDataRepository_First(string url, string date)
        {
            //下载网页源代码 
            var docText = GetWebClient(url);
            //加载源代码，获取文档对象
            var doc = new HtmlDocument(); doc.LoadHtml(docText);
            //更加xpath获取总的对象，如果不为空，就继续选择dl标签 
            HtmlNode res = doc.DocumentNode.SelectSingleNode("//*[@id=\"仓单日报20100329_20236\"]/table");
            if (res != null)
            {
                FDataReposInit model = new FDataReposInit();
                model.TradeHouse = TradeHouseType.shfe.ToString();
                model.AddDate = DateTime.Now;
                model.Content = Regex.Replace(Regex.Replace(res.OuterHtml, @"[\f\n\r\t\v]", ""), @" +", " ");//去掉空格
                string temp = Regex.Replace(model.Content, @"(<table .+?>)", "<table>");
                temp = Regex.Replace(temp, @"(<col .+?>)", "<col>");
                temp = Regex.Replace(temp, @"(<tr .+?>)", "<tr>");
                temp = Regex.Replace(temp, @"(<td .+?>)", "<td>");
                model.Content = temp;
                int d = 0; int.TryParse(date, out d);
                model.Date = d;
                model.IsCheckFinish = 0;
                model.Type = InitContentType.Cangdan.ToString();
                return model;
            }
            return null;
        }
        /// <summary>
        /// 处理2011年-2014年5月16日之间的数据
        /// 处理table
        /// </summary>
        /// <param name="table"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<FDataRepository> GetSHFDataRepository_FirstD(string table, int date)
        {
            List<FDataRepository> list = new List<FDataRepository>();
            //加载源代码，获取文档对象
            var tnode = HtmlNode.CreateNode(table);
            if (tnode != null)
            {
                var trlist = tnode.SelectNodes(@"tr");//获取所有的表格行
                Regex regex = new Regex(@"<td.*?>[\s\S]*?<\/td>");
                string cate = "";
                //每一行都是一个对象
                foreach (var tr in trlist)
                {
                    MatchCollection mc = regex.Matches(tr.InnerHtml);
                    if (mc.Count < 3)
                    {
                        continue;
                    }
                    else if (mc.Count == 3)
                    {
                        var mc0 = HtmlNode.CreateNode(mc[0].ToString());
                        var mc1 = HtmlNode.CreateNode(mc[1].ToString());
                        var mc2 = HtmlNode.CreateNode(mc[2].ToString());
                        if (string.IsNullOrWhiteSpace(cate)|| string.IsNullOrWhiteSpace(mc0.InnerText) || string.IsNullOrWhiteSpace(mc1.InnerText) || string.IsNullOrWhiteSpace(mc2.InnerText))
                        {
                            continue;
                        }
                        if (mc0.InnerText.Contains("上期所指定")||mc0.InnerText.Contains("保税总计") || mc0.InnerText.Contains("完税总计"))
                        {
                            continue;
                        }
                        else
                        {
                            FDataRepository model = new FDataRepository();
                            model.TradeHouse = TradeHouseType.shfe.ToString();
                            model.Date = date;
                            model.CateName = cate;
                            model.Reps = mc0.InnerText;
                            model.Type = 1;
                            if (mc0.InnerText.Contains("合")&& mc0.InnerText.Contains("计"))//小计
                            {
                                model.Type = 2;//统计
                                model.Reps = "ALL";
                            }
                            else if (mc0.InnerText.Contains("总") && mc0.InnerText.Contains("计"))
                            {
                                model.Type = 3;//统计
                                model.Reps = "ALL";
                            }
                            model.TDSum = Convert.ToInt32(mc1.InnerText);
                            model.Change = Convert.ToInt32(mc2.InnerText);
                            model.YTDSum = model.TDSum - model.Change;
                            model.DTime = DateTime.Now;
                            list.Add(model);
                        }
                    }
                    else if (mc.Count == 4)//品种信息
                    {
                        var mc0 = HtmlNode.CreateNode(mc[0].ToString());
                        var mc1 = HtmlNode.CreateNode(mc[1].ToString());
                        var mc2 = HtmlNode.CreateNode(mc[2].ToString());
                        var mc3 = HtmlNode.CreateNode(mc[3].ToString());
                        if (string.IsNullOrWhiteSpace(mc1.InnerText) && string.IsNullOrWhiteSpace(mc2.InnerText))
                        {
                            cate = mc0.InnerText.Replace("&nbsp;","");
                        }
                        if (mc0.InnerText.Contains("地区")||mc0.InnerText.Contains ("上期所指定")|| mc0.InnerText.Contains("保税总计") || mc0.InnerText.Contains("完税总计"))
                        {
                            continue;
                        }
                        if (!string.IsNullOrWhiteSpace(mc0.InnerText) && !string.IsNullOrWhiteSpace(mc1.InnerText) && !string.IsNullOrWhiteSpace(mc2.InnerText) && !string.IsNullOrWhiteSpace(mc3.InnerText))
                        {
                            if (string.IsNullOrWhiteSpace(cate))
                            {
                                continue;
                            }
                            FDataRepository model = new FDataRepository();
                            model.TradeHouse = TradeHouseType.shfe.ToString();
                            model.Date = date;
                            model.CateName = cate;
                            model.Reps = mc1.InnerText;
                            model.Type = 1;
                            model.TDSum = Convert.ToInt32(mc2.InnerText);
                            model.Change = Convert.ToInt32(mc3.InnerText);
                            model.YTDSum = model.TDSum - model.Change;
                            model.DTime = DateTime.Now;
                            list.Add(model);
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 处理20140516年-2018年之间的数据
        /// 返回json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static FDataReposInit GetSHFDataRepository_Second(string url, string date)
        {
            var docText = GetWebClient(url);//返回json

            if (!string.IsNullOrEmpty(docText))
            {
                FDataReposInit model = new FDataReposInit();
                model.TradeHouse = TradeHouseType.shfe.ToString();
                model.AddDate = DateTime.Now;
                string temp = Regex.Replace(docText, "(,\"VARSORT\".+?,)", ",");//去掉空格
                temp = Regex.Replace(temp, "(,\"REGSORT\".+?,)", ",");
                temp = Regex.Replace(temp, "(,\"WHROWS\".+?,)", ",");
                temp = Regex.Replace(temp, "(,\"ROWORDER\".+?,)", ",");
                temp = Regex.Replace(temp, "(,\"WGHTUNIT\".+?,)", ",");
                temp = Regex.Replace(temp, "(,\"ROWSTATUS\".+?})", "}");
                temp = Regex.Replace(temp, "(\\$\\$.+?\")", "\"");
                model.Content = temp;
                int d = 0; int.TryParse(date, out d);
                model.Date = d;
                model.IsCheckFinish = 0;
                model.Type = InitContentType.Cangdan.ToString();
                return model;
            }
            return null;
        }

        public static List<FDataRepository> GetSHFDataRepository_SecondD(string table, int date)
        {
            List<FDataRepository> list = new List<FDataRepository>();
            //table is jsonobject
            SHFE_DataModel obj = JsonConvert.DeserializeObject<SHFE_DataModel>(table);
            if (obj != null&&obj.o_cursor.Count>0)
            {
                foreach (SHFE_CangDanModel item in obj.o_cursor)
                {
                    if (string.IsNullOrEmpty(item.REGNAME)&&(!item.WHABBRNAME.Equals("合计") && !item.WHABBRNAME.Equals("总计")))
                    {
                        continue;
                    }
                    FDataRepository model = new FDataRepository();
                    model.TradeHouse = TradeHouseType.shfe.ToString();
                    model.Date = date;
                    model.CateName = item.VARNAME;
                    model.Reps =item.WHABBRNAME;
                    model.Type = 1;
                    if (model.Reps.Equals("合计"))
                    {
                        model.Type = 2;//统计
                        model.Reps = "ALL";
                    }
                    else if (model.Reps.Equals("总计"))
                    {
                        model.Type = 3;//统计
                        model.Reps = "ALL";
                    }
                    model.TDSum = Convert.ToInt32(item.WRTWGHTS);
                    model.Change = Convert.ToInt32(item.WRTCHANGE);
                    model.YTDSum = model.TDSum - model.Change;
                    model.DTime = DateTime.Now;
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion

        #region 大商所仓单信息获取
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
            if (titleNode != null)
            {
                var title = titleNode.InnerText;//查询日期：20180614 仓单数据在每日下午4点生成
                date = "20" + MatchSubString(title, "20", 1, 6);
            }
            var res = doc.DocumentNode.SelectSingleNode("//*[@id=\"printData\"]/div/table");
            if (res != null)
            {
                FDataReposInit model = new FDataReposInit();
                model.TradeHouse = TradeHouseType.dce.ToString();
                model.AddDate = DateTime.Now;
                model.Content = Regex.Replace(Regex.Replace(res.OuterHtml, @"[\f\n\r\t\v]", ""), @" +", " ");//去掉空格
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
                    MatchCollection mc = regex.Matches(tr.InnerHtml);
                    if (mc.Count > 0 && !string.IsNullOrEmpty(HtmlNode.CreateNode(mc[0].ToString()).InnerText))
                    {
                        FDataRepository model = new FDataRepository();
                        model.TradeHouse = TradeHouseType.dce.ToString();
                        model.Date = date;
                        model.CateName = HtmlNode.CreateNode(mc[0].ToString()).InnerText;
                        model.Reps = HtmlNode.CreateNode(mc[1].ToString()).InnerText;
                        model.Type = 1;
                        if (model.CateName.Contains("小计"))
                        {
                            model.Type = 2;//统计
                            model.Reps = "ALL";
                        }
                        else if (model.CateName.Contains("总计"))
                        {
                            model.Type = 3;//统计
                            model.Reps = "ALL";
                        }
                        model.YTDSum = Convert.ToInt32(HtmlNode.CreateNode(mc[2].ToString()).InnerText);
                        model.TDSum = Convert.ToInt32(HtmlNode.CreateNode(mc[3].ToString()).InnerText);
                        model.Change = Convert.ToInt32(HtmlNode.CreateNode(mc[4].ToString()).InnerText);
                        model.DTime = DateTime.Now;
                        list.Add(model);
                    }
                    else continue;
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

        #region 获取业务通知/交易所新闻
        /// <summary>
        /// 获取业务通知/交易所新闻
        /// </summary>
        /// <param name="type">(1:业务通知;2:交易所新闻;)</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public static List<FNews> GetNewsFromUrl(int type, int pageIndex)
        {
            var list = new List<FNews> { };
            var url = string.Empty;

            switch (type)
            {
                case 1: url = $"{site}{businessUrl}"; break;
                case 2: url = $"{site}{exchangeUrl}"; break;
                default: break;
            }

            var doc = new HtmlDocument();
            var html = GetWebClient(string.Format(url, pageIndex));
            if (html.Length == 0) return list;

            doc.LoadHtml(html);
            var rootNode = doc.DocumentNode;
            var nodelCollection = rootNode.SelectNodes("//*[@class='list_tpye06']/li");
            foreach (var node in nodelCollection)
            {
                var aNode = node.LastChild;
                var news = new FNews
                {
                    AddDate = Convert.ToDateTime(node.FirstChild.InnerText),
                    NewsTitle = aNode.InnerText,
                    NewsUrl = $"{site}{aNode.Attributes["href"].Value}",
                    NewsType = type,
                    NewContent = "",
                    NSource = url,
                };

                GetDetailByLink(ref news);
                list.Add(news);
            }

            return list;
        }

        /// <summary>
        /// 通过标题获取详情
        /// </summary>
        /// <param name="model"></param>
        private static void GetDetailByLink(ref FNews model)
        {
            var doc = new HtmlDocument();
            var html = GetWebClient(model.NewsUrl);
            if (html.Length > 0)
            {
                doc.LoadHtml(html);
                var rootNode = doc.DocumentNode;
                var node = rootNode.SelectSingleNode("//*[@id='zoom']");
                model.NewContent = node.InnerHtml;
            }
        }
        #endregion

    }
}
