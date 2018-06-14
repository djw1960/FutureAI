using EF.Common;
using Serv;
using Serv.Lib;
using System;
using System.Linq;
using System.Text;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var ibll = OperationContext.BLLSession;
            string url = "http://www.dce.com.cn/publicweb/quotesdata/wbillWeeklyQuotes.html";
            var model=CrawlerUtils.GetFDataRepository_First(url);
            ibll.FDataReposInit.Add(model);
            int n=ibll.FDataReposInit.SaveChanges();
            System.Console.WriteLine(n);
            //103.45.13.74:8090  103.45.9.70:8090 https://crmapi.xs9999.com/v2/A401
            //var html =HttpUtils.HttpPostWebProxy("http://crmapi.xs9999.com/v2/A401", "equipment=ios", "103.45.9.70:8090");
            //System.Console.WriteLine(html);

            //var html2 = HttpUtils.HttpPostAddHost("http://47.89.33.205:8002/api/paycenter", "ver=100", "47.89.33.205:8002");
            //System.Console.WriteLine(html2);
            //int num=
            ////调用业务系统接口，通知业务系统
            //NotifyModel model = new NotifyModel();
            //model.BillNo = "Bill41685123168546312";
            //model.CustomerNo = "C46546464";
            //model.Amount = "10000";
            //model.Status = "1";//成功
            //model.SignType = "MD5";

            //NotifyResultModel t = HttpUtils.PostAsync<NotifyModel, NotifyResultModel>("http://192.168.1.60:8880/api/PayNotify/Notify", model);


            //DateTime dend = DateTime.Parse("2018-05-04").AddDays(1).AddSeconds(-1);
            //System.Console.WriteLine(dend.ToString());



            System.Console.WriteLine(ibll.FDataMaterial.where(a=>a.ID>0).Count());
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



            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("result", "0");
            //if (dic.Keys.Contains("resp"))
            //{
            //    System.Console.WriteLine(dic["resp"]);
            //}
            //else
            //{
            //    System.Console.WriteLine(dic["result"]);
            //}

            //string f = "<form name=\"form1\" id=\"form1\" method=\"post\" action=\"{0}\"><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /><form/>";
            //string r = Base64Util.EncodeBase64(Encoding.UTF8, f);
            //System.Console.WriteLine(r.Length);
            //System.Console.WriteLine(r);
            //string r2 = DESEncrypt.Encrypt(f, DESEncrypt.info);
            //System.Console.WriteLine(r2);
            //System.Console.WriteLine(r2.Length);

            //string temp = "2795191D44AD4BF3DF1583FEC96B807EB263D7CCBE2A525AE565951B55C3155B5B6BAA13D03969F2C4DAF6C968F370AD";
            //string s = DESEncrypt.Decrypt(temp, DESEncrypt.info);
            //System.Console.WriteLine(s);

            //System.Console.WriteLine(DESEncrypt.Decrypt("5CBD1785551853BD074D5377288E009D",DESEncrypt.info));
            string en = "姓名|身份证号";
            string str = DESEncrypt.Encrypt(en, DESEncrypt.info);
            System.Console.WriteLine(str);
            System.Console.WriteLine(str.Length);
            ////拼接签名字符串 CustomerNo={0}&BillNo={1}&Amount={2}&Status={3}{4}
            //string signStr = string.Format("CustomerNo={0}&BillNo={1}&Amount={2}&Status={3}{4}",
            //    "800002", "801180403165700382", "10000", "1", DESEncrypt.Decrypt("5CBD1785551853BD074D5377288E009D", DESEncrypt.info));
            //string sign = SignUtils.MD5(signStr, Encoding.UTF8);
            ////调用业务系统接口，通知业务系统
            //NotifyModel model = new NotifyModel();
            //model.BillNo = "801180403165700382";
            //model.CustomerNo = "800002";
            //model.Amount = "10000";
            //model.Status = "1";//成功
            //model.Sign = sign;
            //string p = JsonConvert.SerializeObject(model);

            //string result= HttpUtils.HttpPost_JSON("http://192.168.1.99/zhifu/common_callback.php", p);

            //System.Console.WriteLine(result);
            //decimal n = 4.568M;


            //System.Console.WriteLine(Math.Round(n*100));
            //System.Console.WriteLine(Math.Ceiling(n * 100));
            //System.Console.WriteLine(Math.Floor(n * 100));


            //for (int i = 0; i < 1000; i++)
            //{
            //    System.Console.WriteLine(Common.GetBillNo(32));
            //}

            System.Console.ReadKey();
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
