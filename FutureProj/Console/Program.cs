using EF.Entitys;
using Serv;
using Serv.Entitys;
using Serv.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //string str = "{\"VARNAME\":\"铜$$COPPER\",\"VARSORT\":0,\"REGNAME\":\"上海$$Shanghai\",\"REGSORT\":0,\"WHABBRNAME\":\"期晟公司$$Qisheng\",\"WHROWS\":12,\"WGHTUNIT\":\"2\",\"WRTWGHTS\":1919,\"WRTCHANGE\":0,\"ROWORDER\":1,\"ROWSTATUS\":\"0\"}";
            //string s = Regex.Replace(str, "(,\"VARSORT\".+?,)", ",");
            //s = Regex.Replace(s, "(,\"REGSORT\".+?,)", ",");
            //s = Regex.Replace(s, "(,\"WHROWS\".+?,)", ",");
            //System.Console.WriteLine(s);
            //s = Regex.Replace(s, "(,\"ROWORDER\".+?,)", ",");
            //s = Regex.Replace(s, "(,\"WGHTUNIT\".+?,)", ",");
            //s = Regex.Replace(s, "(,\"ROWSTATUS\".+?})", "}");
            //s = Regex.Replace(s, "(\\$\\$.+?\")", "\"");
            //System.Console.WriteLine(s);

            //System.Console.WriteLine("---------------------------------------------------------------");
            //System.Console.ReadKey();

            //var ibll = OperationContext.BLLSession;

            #region NewsTest
            var list = new List<FNews> { };
            var pageIndex = 1;
            while (pageIndex > 0)
            {
                var news = CrawlerUtils.GetNewsFromUrl(1, pageIndex);
                foreach (var item in news)
                {
                    System.Console.WriteLine($"{item.AddDate.ToShortDateString()} {item.NewsTitle}~({(item.NewContent.Length > 0 ? "有采集到详情" : "无")})");
                }

                if (news.Count == 0)
                {
                    pageIndex = 0;
                    break;
                }

                // 请求太快貌似会被4O4
                Thread.Sleep(1000);
                pageIndex++;
            }
            #endregion

            System.Console.ReadKey();
        }
        /// <summary>
        /// 初始化上海商品交易所仓单数据
        /// 2011-20140516
        /// </summary>
        public static void GetSHFDataRepository_First()
        {
            var ibll = OperationContext.BLLSession;
            string[] years = new string[] { "2014" };
            int[] months = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            string[] days = new string[] { "01","02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"
            ,"13","14","15","16","17","18","19","20","21","22"
            ,"23","24","25","26","27","28","29","30","31"};
            foreach (string year in years)
            {
                foreach (int month in months)
                {
                    foreach (string day in days)
                    {
                        int date = Convert.ToInt32(string.Format("{0}{1}{2}", year, month + 1 < 10 ? "0" + (month + 1).ToString() : (month + 1).ToString(), day));
                        if (date > 20140516)
                        {
                            continue;
                        }
                        if (ibll.FDataReposInit.where(a => a.Date == date && a.TradeHouse == TradeHouseType.shfe.ToString() && a.Type == InitContentType.Cangdan.ToString()).Count() < 1)
                        {
                            string url = string.Format("http://www.shfe.com.cn/data/dailydata/{0}dailystock.html?isAjax=true",
                                            date);
                            var model = CrawlerUtils.GetSHFDataRepository_First(url, date.ToString());
                            if (model != null)
                            {
                                ibll.FDataReposInit.Add(model);
                                System.Console.WriteLine("上海：" + date);
                            }
                        }
                    }
                    int n = ibll.FDataReposInit.SaveChanges();
                    System.Console.WriteLine("上海：Save --{0}", n);
                }
            }
            System.Console.WriteLine("上海：FINISH");
        }
        /// <summary>
        /// 20140519-2018 上海仓单数据
        /// </summary>
        public static void GetSHFDataRepository_Second()
        {
            var ibll = OperationContext.BLLSession;
            string[] years = new string[] { "2015", "2016", "2017", "2018" };
            int[] months = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            string[] days = new string[] { "01","02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"
            ,"13","14","15","16","17","18","19","20","21","22"
            ,"23","24","25","26","27","28","29","30","31"};
            foreach (string year in years)
            {
                foreach (int month in months)
                {
                    foreach (string day in days)
                    {
                        int date = Convert.ToInt32(string.Format("{0}{1}{2}", year, month + 1 < 10 ? "0" + (month + 1).ToString() : (month + 1).ToString(), day));
                        if (date >= 20180616)
                        {
                            continue;
                        }
                        if (ibll.FDataReposInit.where(a => a.Date == date && a.TradeHouse == TradeHouseType.shfe.ToString() && a.Type == InitContentType.Cangdan.ToString()).Count() < 1)
                        {
                            string url = string.Format("http://www.shfe.com.cn/data/dailydata/{0}dailystock.dat",
                                            date);
                            var model = CrawlerUtils.GetSHFDataRepository_Second(url, date.ToString());
                            if (model != null)
                            {
                                ibll.FDataReposInit.Add(model);
                                System.Console.WriteLine("上海：" + date);
                            }
                        }
                    }
                    int n = ibll.FDataReposInit.SaveChanges();
                    System.Console.WriteLine("上海：Save --{0}", n);
                }
            }
            System.Console.WriteLine("上海：FINISH");
        }
        /// <summary>
        /// 初始化大商所仓单数据
        /// </summary>
        public static void GetDSFDataRepository_First()
        {
            var ibll = OperationContext.BLLSession;
            string[] years = new string[] { "2013", "2014", "2015", "2016", "2017", "2018" };
            int[] months = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            string[] days = new string[] { "01","02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"
            ,"13","14","15","16","17","18","19","20","21","22"
            ,"23","24","25","26","27","28","29","30","31"};
            foreach (string year in years)
            {
                foreach (int month in months)
                {
                    foreach (string day in days)
                    {
                        int d = Convert.ToInt32(string.Format("{0}{1}{2}", year, month + 1 < 10 ? "0" + (month + 1).ToString() : (month + 1).ToString(), day));
                        if (d < 20130530)
                        {
                            continue;
                        }
                        if (ibll.FDataReposInit.where(a => a.Date == d && a.TradeHouse == TradeHouseType.dce.ToString() && a.Type == InitContentType.Cangdan.ToString()).Count() < 1)
                        {
                            string url = string.Format("http://www.dce.com.cn/publicweb/quotesdata/wbillWeeklyQuotes.html?year={0}&month={1}&day={2}",
                            year, month, day);
                            var model = CrawlerUtils.GetDSFDataRepository_First(url);
                            ibll.FDataReposInit.Add(model);
                            int n = ibll.FDataReposInit.SaveChanges();
                            System.Console.WriteLine("大商：" + d);
                        }
                    }
                }
            }

        }
        public static void GetDSFDataRepository_Second()
        {
            var ibll = OperationContext.BLLSession;
            int count = ibll.FDataReposInit.where(a => a.ID > 0 && a.IsCheckFinish == 0).Count();
            for (int page = 1; page <= (count / 10) + 1; page++)
            {
                List<FDataReposInit> list = ibll.FDataReposInit.where(a => a.ID > 0 && a.IsCheckFinish == 0).OrderBy(s => s.ID).Take(10).ToList();
                foreach (FDataReposInit model in list)
                {
                    if (ibll.FDataRepository.where(a => a.Date == model.Date && a.TradeHouse == TradeHouseType.dce.ToString()).Count() < 1)
                    {
                        List<FDataRepository> resplist = CrawlerUtils.GetDSFDataRepository_Second(model.Content, model.Date);
                        foreach (var item in resplist)
                        {
                            ibll.FDataRepository.Add(item);
                        }
                        model.IsCheckFinish = 1;
                        ibll.FDataReposInit.Update(model, new string[] { "IsCheckFinish" });
                        int num = ibll.SaveChanges();
                        System.Console.WriteLine("大商Check：" + model.Date);
                    }
                    else continue;
                }
            }
            System.Console.WriteLine("大商Check：FINISH");
        }
        /// <summary>
        /// 多条件查询拼接
        /// </summary>
        public static void PredicateBuilderTest()
        {

            //List<Categorys> list = new List<Categorys>();
            //Categorys c=list.FirstOrDefault(s => s.ID > 1000);
            //System.Console.WriteLine(c.CateName);
            //var p = PredicateBuilder.True<PayOrder>();
            //p = p.And(s => s.AddDate >=DateTime.Today);
            //p = p.And(s => s.AddDate <= DateTime.Now);
            //System.Console.WriteLine(ibll.PayOrder.where(p).Count());



            //var p = PredicateBuilder.True<PayOrder>();
            //p = p.And(s => s.CustomerNo == "800002");
            //p = p.And(s=>s.Status==0);
            //System.Console.WriteLine(ibll.PayOrder.where(p).Count());

            //var p2 = PredicateBuilder.True<PayOrder>();
            //p2 = p2.And(s => s.Status == 1 && s.CustomerNo == "800002");
            //System.Console.WriteLine(ibll.PayOrder.where(p2).Count());

            //var p3 = PredicateBuilder.True<PayOrder>();
            //p3 = p3.And(s => s.Status == 1 || s.IsNotify == 1);
            //System.Console.WriteLine(ibll.PayOrder.where(p3).Count());

            //var p4 = PredicateBuilder.True<PayOrder>();
            //p4 = p4.And(s => s.IsDel == 1);
            //p4 = p4.Or(s=>s.OrderNo==null);
            //System.Console.WriteLine(ibll.PayOrder.where(p4).Count());

            //var p5= PredicateBuilder.False<PayOrder>();
            //p5 = p5.Or(s => s.ID >100);
            //System.Console.WriteLine(ibll.PayOrder.where(p5).Count());

            //var p6 = PredicateBuilder.False<PayOrder>();
            //p6 = p6.And(s => s.ID > 100);
            //System.Console.WriteLine(ibll.PayOrder.where(p6).Count());

        }
    }
    public static class Base64Util
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
