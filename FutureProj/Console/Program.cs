using EF.Entitys;
using Serv;
using Serv.Entitys;
using Serv.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Console
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
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

        //string title = "品种：苹果AP单位：张日期：2018-06-19";
        //string temp = Regex.Match(title, "(品种：.+单位：)").ToString();
        //System.Console.WriteLine(temp);
        //temp = temp.Replace("品种：", "").Replace("单位：", "");
        //System.Console.WriteLine(temp);
        //System.Console.WriteLine("---------------------------------------------------------------");
        //System.Console.ReadKey();

        //var ibll = OperationContext.BLLSession;
        //string str = "/dalianshangpin/yw/fw/jystz/ywtz/6095044/index.html";
        //string no = Regex.Match(str, "(/[0-9]+/)").ToString();
        //System.Console.WriteLine(no);

        #region NewsTest
        //var list = new List<FNews> { };
        //var pageIndex = 1;
        //while (pageIndex > 0 && pageIndex <= 2)
        //{
        //    /* 郑州所 这个站点做了处理
        //     * 1.登录会话做了限制，会被重定向
        //     * 2.详情被生成为PDF格式，需要解析
        //     * */
        //    var news = CrawlerUtils.GetNewsFromUrl_ZZ(1, pageIndex);
        //    foreach (var item in news)
        //    {
        //        //ibll.FNews.Add(item);
        //        System.Console.WriteLine($"{item.AddDate.Value.ToShortDateString()} {item.NewsTitle}~({(item.NewContent.Length > 0 ? "有采集到详情" : "无")})");
        //    }
        //    /*
        //    int n = ibll.FNews.SaveChanges();
        //    if (n > 0)
        //    {
        //        System.Console.WriteLine("新闻：save-{0}", n);
        //    }
        //    */
        //    if (news.Count == 0)
        //    {
        //        pageIndex = 0;
        //        break;
        //    }

        //    // 请求太快貌似会被4O4
        //    Thread.Sleep(1000);
        //    pageIndex++;
        //}
        //System.Console.WriteLine("新闻：Finish");
        #endregion
        //SX_SiteConfig site = new SX_SiteConfig() { };
        //site.SiteHost = "http://www.chancheng.gov.cn";
        //site.SiteName = "佛山市禅城区教育局";
        //site.SiteUrl = "http://www.chancheng.gov.cn/ccjy/060102/bmlist2.shtml";
        //site.SiteListXPath = "/html/body/div[1]/div[2]/div/div[2]/div[2]";
        //site.SiteItemXPath = "div/div";
        //var list=CrawlerUtils.FoShanChanChengedu(site);

        
            SX_SiteConfig site = new SX_SiteConfig() { };
            site.SiteHost = "http://www.longmen.gov.cn";
            site.SiteName = "山香教育惠州分校";
            site.SiteUrl = "http://www.dayawan.gov.cn/qxjj/artList.html?sn=qxjj&cataId=bf317b5054bb4123b252c5d9b34e77aa&pageNo=1";
            site.SiteListXPath = "//*[@id=\"art_list_div\"]";
            site.SiteItemXPath = "dl";
            var list = CrawlerUtils.HZDaYaWan1(site);
            System.Console.WriteLine(list.Count);
            System.Console.ReadKey();
        }

        #region 郑州商品交易所仓单
        /// <summary>
        /// 获取商品交易所仓单数据
        /// 2011-
        /// </summary>
        public static void GetZZFDataRepository_First()
        {
            ZZTaskStart:
            try
            {
                var ibll = OperationContext.BLLSession;
                //string[] years = new string[] { "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018" };
                string[] years = new string[] {"2018" };
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
                            if (date < 20180616)
                            {
                                continue;
                            }
                            if (ibll.FDataReposInit.where(a => a.Date == date && a.TradeHouse == TradeHouseType.czce.ToString() && a.Type == InitContentType.Cangdan.ToString()).Count() < 1)
                            {
                                string url = "";
                                if (date < 20151001)
                                {
                                    url = string.Format("http://www.czce.com.cn/portal/exchange/{0}/datawhsheet/{1}.htm", year, date);
                                }
                                else if (date < 20180616)
                                {
                                    url = string.Format("http://www.czce.com.cn/portal/DFSStaticFiles/Future/{0}/{1}/FutureDataWhsheet.htm", year, date);
                                }
                                else
                                {
                                    url = string.Format("http://www.czce.com.cn/cn/DFSStaticFiles/Future/{0}/{1}/FutureDataWhsheet.htm", year, date);
                                }
                                var model = CrawlerUtils.GetZZFDataRepository_First(url, date);
                                if (model != null)
                                {
                                    ibll.FDataReposInit.Add(model);
                                    System.Console.WriteLine("郑州init：" + date);
                                }
                            }
                        }
                        int n = ibll.FDataReposInit.SaveChanges();
                        System.Console.WriteLine("郑州：Save --{0}", n);
                    }
                }
                System.Console.WriteLine("郑州：FINISH");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("郑商所仓单：{0}", ex.Message);
                System.Threading.Thread.Sleep(60000);
                goto ZZTaskStart;
            }
        }
        /// <summary>
        /// 解析郑商所交易仓单详情数据
        /// 20110104-20151001-2018
        /// </summary>
        public static void GetZZFDataRepository_Second()
        {
            TaskStart:
            try
            {
                var ibll = OperationContext.BLLSession;
                int count = ibll.FDataReposInit.where(a => a.Date > 20180616 && a.TradeHouse == TradeHouseType.czce.ToString() && a.IsCheckFinish == 0).Count();
                for (int page = 1; page <= (count / 10) + 1; page++)
                {
                    List<FDataReposInit> list = ibll.FDataReposInit.where(a => a.Date > 20180616 && a.TradeHouse == TradeHouseType.czce.ToString() && a.IsCheckFinish == 0).OrderBy(s => s.ID).Take(10).ToList();
                    foreach (FDataReposInit model in list)
                    {
                        List<FDataRepository> resplist = CrawlerUtils.GetZZFDataRepository_Second(model.Content, model.Date);
                        foreach (var item in resplist)
                        {
                            ibll.FDataRepository.Add(item);
                        }
                        model.IsCheckFinish = 1;
                        ibll.FDataReposInit.Update(model, new string[] { "IsCheckFinish" });
                        int num = ibll.SaveChanges();
                        System.Console.WriteLine("郑州Check：" + model.Date);
                    }
                }
                System.Console.WriteLine("郑州Check：FINISH");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(string.Format("异常：{0} 开始休眠", ex.Message));
                System.Threading.Thread.Sleep(60000);
                goto TaskStart;
            }
        }
        #endregion

        #region 上海商品交易所仓单初始化
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
        /// 上海仓单初始化
        /// 2011-20140516
        /// </summary>
        public static void GetSHFDataRepository_FirstD()
        {
            TaskStart:
            try
            {
                var ibll = OperationContext.BLLSession;
                int count = ibll.FDataReposInit.where(a => a.Date <= 20140516 && a.TradeHouse == TradeHouseType.shfe.ToString() && a.IsCheckFinish == 0).Count();
                for (int page = 1; page <= (count / 10) + 1; page++)
                {
                    List<FDataReposInit> list = ibll.FDataReposInit.where(a => a.Date <= 20140516 && a.TradeHouse == TradeHouseType.shfe.ToString() && a.IsCheckFinish == 0).OrderBy(s => s.ID).Take(10).ToList();
                    foreach (FDataReposInit model in list)
                    {
                        List<FDataRepository> resplist = CrawlerUtils.GetSHFDataRepository_FirstD(model.Content, model.Date);
                        foreach (var item in resplist)
                        {
                            ibll.FDataRepository.Add(item);
                        }
                        model.IsCheckFinish = 1;
                        ibll.FDataReposInit.Update(model, new string[] { "IsCheckFinish" });
                        int num = ibll.SaveChanges();
                        System.Console.WriteLine("上海2011-2014Check：" + model.Date);
                    }
                }
                System.Console.WriteLine("上海2011-2014Check：FINISH");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(string.Format("异常：{0} 开始休眠", ex.Message));
                System.Threading.Thread.Sleep(60000);
                goto TaskStart;
            }

        }
        /// <summary>
        /// 20140519-2018 上海仓单数据
        /// </summary>
        public static void GetSHFDataRepository_Second()
        {
            var ibll = OperationContext.BLLSession;
            string[] years = new string[] {  "2018" };
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
                        if (date <= 20180616)
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
        /// 20140519-2018 上海仓单数据解析
        /// </summary>
        public static void GetSHFDataRepository_SecondD()
        {
            var ibll = OperationContext.BLLSession;
            int count = ibll.FDataReposInit.where(a => a.Date > 20140516 && a.TradeHouse == TradeHouseType.shfe.ToString() && a.IsCheckFinish == 0).Count();
            for (int page = 1; page <= (count / 10) + 1; page++)
            {
                List<FDataReposInit> list = ibll.FDataReposInit.where(a => a.Date > 20140516 && a.TradeHouse == TradeHouseType.shfe.ToString() && a.IsCheckFinish == 0).OrderBy(s => s.ID).Take(10).ToList();
                foreach (FDataReposInit model in list)
                {
                    if (ibll.FDataRepository.where(a => a.Date == model.Date && a.TradeHouse == TradeHouseType.shfe.ToString()).Count() < 1)
                    {
                        List<FDataRepository> resplist = CrawlerUtils.GetSHFDataRepository_SecondD(model.Content, model.Date);
                        foreach (var item in resplist)
                        {
                            ibll.FDataRepository.Add(item);
                        }
                        model.IsCheckFinish = 1;
                        ibll.FDataReposInit.Update(model, new string[] { "IsCheckFinish" });
                        int num = ibll.SaveChanges();
                        System.Console.WriteLine("上海2014Check：" + model.Date);
                    }
                    else continue;
                }
            }
            System.Console.WriteLine("上海2014Check：FINISH");
        }
        #endregion

        #region  初始化大商所仓单数据
        /// <summary>
        /// 初始化大商所仓单数据
        /// </summary>
        public static void GetDSFDataRepository_First()
        {
            var ibll = OperationContext.BLLSession;
            string[] years = new string[] {"2018" };
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
                        if (d < 20180616)
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
            int count = ibll.FDataReposInit.where(a => a.ID > 0 && a.TradeHouse == TradeHouseType.dce.ToString() && a.IsCheckFinish == 0).Count();
            for (int page = 1; page <= (count / 10) + 1; page++)
            {
                List<FDataReposInit> list = ibll.FDataReposInit.where(a => a.ID > 0 && a.TradeHouse == TradeHouseType.dce.ToString() && a.IsCheckFinish == 0).OrderBy(s => s.ID).Take(10).ToList();
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
        #endregion
        public static void Proc_InitCateCode()
        {
            var ibll = OperationContext.BLLSession;
            ibll.Proc_ExecuteSqlCommand(" exec [dbo].[Proc_InitCateCode] ");
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
