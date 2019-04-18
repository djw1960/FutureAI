using EF.Common;
using EF.Entitys;
using Newtonsoft.Json;
using Serv.Entitys;
using Serv.Lib;
using Serv.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv
{
    /// <summary>
    /// windows 扫表服务
    /// </summary>
    public class WindowControl
    {
        private static EF.IService.IServiceSession ibll = OperationContext.BLLSession;

        #region 异步结果通知Windows服务+PushOrderStatusToCustomer
        /// <summary>
        /// 此方法一般用来处理第一次同步通知失败的情况
        /// </summary>
        public static void PushOrderStatusToCustomer()
        {
            //1.0 获取配置信息
            //2.0 解析站点对不同站点不同解析
            //http://www.chancheng.gov.cn/ccjy/060102/bmlist2.shtml
            //demo /html/body/div[1]/div[2]/div/div[2]/div[2]
            var confilist = ibll.SX_SiteConfig.where(s => true).ToList();
            foreach (var site in confilist)
            {
                List<SX_News> list = new List<SX_News>();
                switch (site.Type)
                {
                    case "HuiZhouShanXiangedu":
                        list = CrawlerUtils.HuiZhouShanXiangedu(site);
                        break;
                    case "HZLongMen":
                        list = CrawlerUtils.HZLongMen(site);
                        break;
                    case "HZShiZhi":
                        list = CrawlerUtils.HZShiZhi(site);
                        break;
                    case "HZHuiCheng":
                        list = CrawlerUtils.HZHuiCheng(site);
                        break;
                    case "HZDaYaWan1":
                        list = CrawlerUtils.HZDaYaWan1(site);
                        break;
                    case "HZDaYaWan2":
                        list = CrawlerUtils.HZDaYaWan2(site);
                        break;
                    case "HZZhongKai":
                        list = CrawlerUtils.HZZhongKai(site);
                        break;
                    case "HZHuiDong":
                        list = CrawlerUtils.HZHuiDong(site);
                        break;
                    case "HZHuiYang":
                        list = CrawlerUtils.HZHuiYang(site);
                        break;
                    case "HZBoLuo":
                        list = CrawlerUtils.HZBoLuo(site);
                        break;
                    //case "FoShanChanChengedu":
                    //    //佛山禅城教育局
                    //    list = CrawlerUtils.FoShanChanChengedu(site);
                    //    break;
                        //case "SWChengQu":
                        //    list = CrawlerUtils.SWChengQu(site);
                        //    break;
                        //case "FSShunDe":
                        //    list = CrawlerUtils.FSShunDe(site);
                        //    break;
                        //case "FSNanHai":
                        //    list = CrawlerUtils.FSNanHai(site);
                        //    break;
                        //case "FSGaoMing1":
                        //    list = CrawlerUtils.FSGaoMing1(site);
                        //    break;
                        //case "FSGaoMing2":
                        //    list = CrawlerUtils.FSGaoMing2(site);
                        //    break;
                        //case "FSSanShui":
                        //    list = CrawlerUtils.FSSanShui(site);
                        //    break;
                }
                if (list!=null&&list.Count>0)
                {
                    foreach (var item in list)
                    {
                        if (ibll.SX_News.where(a=>a.SiteName== item.SiteName&&a.Url==item.Url).Count()==0)
                        {
                            ibll.SX_News.Add(item);
                        }
                    }
                    ibll.SX_News.SaveChanges();
                    LogHelper.Info<WindowControl>("{0}:获取{1}条数据", site.SiteName, list.Count);
                }
            }
        }
        #endregion

    }
}
