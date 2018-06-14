using EF.Common;
using EF.Entitys;
using Newtonsoft.Json;
using Serv.Entitys;
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
            
            //从数据库中查询直接通知失败的订单，调用异步系统接口推送给业务系统
            //1.0 查询支付成功 但异步通知未成功或未正确返回的数据
            //List<PayOrder> list = ibll.Proc_SqlQuery<PayOrder>(" exec [dbo].[PROC_GetNotifyOrderList] ").ToList();
            //List<PayCustomer> cusList = ibll.PayCustomer.where(a => a.ID > 0).ToList();
            //List<View_PayMert> payMertList = ibll.View_PayMert.where(a => !string.IsNullOrEmpty(a.MerNo)).ToList();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    var item = list[0];
            //    PayCustomer c = cusList.Single(a => a.CustmCode == item.CustomerNo);
            //    View_PayMert m = payMertList.Single(a => a.MerNo == item.MerNo);
            //    if (c == null||m==null)
            //    {
            //        continue;
            //    }

            //    PushNotify(item, c,m);


            //}
            //ibll.SaveChanges();
            //return;
        }
    //    private static void PushNotify(PayOrder order, PayCustomer cust, View_PayMert mert)
    //    {
            

    //        NotifyModel model = new NotifyModel();
    //        model.BillNo = order.BillNo;
    //        model.Amount = order.Amount.ToString();//单位 分
    //        model.CustomerNo = order.CustomerNo;
    //        model.Status = "1";
    //        model.Sign = "";
    //        model.Source = mert.CompShotName;
    //        model.Channel = mert.MerName;
    //        model.Charge_val = "0";//手续费费率
    //        model.Service_charge = "0";//手续费
    //        model.Charge_type = "2";//手续费计算方式
    //        model.Calculation_type = "1";//手续费计算规则
    //        model.OrderNo = order.OrderNo;
    //        string signStr = string.Format("CustomerNo={0}&BillNo={1}&Amount={2}&Status={3}{4}",
    //model.CustomerNo, model.BillNo, model.Amount, "1", DESEncrypt.Decrypt(cust.MEncryKey, DESEncrypt.info));
    //        string sign = SignUtils.MD5(signStr, System.Text.Encoding.UTF8);
    //        model.Sign = sign;
    //        string p = JsonConvert.SerializeObject(model);
    //        ServiceLog log = new ServiceLog();
    //        log.BillNo = order.BillNo;
    //        log.ReqType = LogTypeConfig.NotifyMC;
    //        log.ReqStr = JsonConvert.SerializeObject(model);
    //        try
    //        {
    //            string t = HttpUtils.HttpPost_JSON(cust.NotifyUrl, p);
    //            log.RespStr = JsonConvert.SerializeObject(t);
    //            bool resp = true;
    //            if (t.ToUpper().Equals("SUCCESS"))
    //            {
    //                resp = true;
    //            }
    //            else
    //            {
    //                //log -通知失败
    //                resp = false;
    //            }
    //            //结果写入数据库
    //            order.NotifyCount += 1;
    //            order.IsNotify = resp ? 1 : order.NotifyCount >= 5 ? 0 : 0;//0-未通知成功，1-通知成功
                
    //        }
    //        catch (Exception ex)
    //        {
    //            log.RespStr = ex.Message;
    //            order.NotifyCount += 1;
    //        }
    //        log.AddDate = DateTime.Now;
    //        ibll.ServiceLog.Add(log);
    //        ibll.PayOrder.Update(order, new string[] { "NotifyCount", "IsNotify" });
    //        ibll.SaveChanges();




    //    }
        #endregion

        #region 交易结果查询Windows服务
        /// <summary>
        /// 从支付平台查询订单状态
        /// 主动查询订单状态，处理通知失败问题
        /// </summary>
        public static void QueryOrderStatusFromService()
        {
            ////1.0 主动查询 暂定订单创建5分钟之后，如果状态是失败，则主动查询一次，依然失败则不再主动查询，此时应手动查询
            //List<PayOrder> list = ibll.PayOrder.where(A =>A.Status==0&&!string.IsNullOrEmpty(A.PNo)&&A.AddDate.AddMinutes(6)>DateTime.Now&&A.AddDate.AddMinutes(4)<DateTime.Now).ToList();
            //foreach (var item in list)
            //{
            //    //查询订单使用的查询通道
            //    View_WinQueryOrderStatus m = ibll.View_WinQueryOrderStatus.Single(a => a.BillNo == item.BillNo);

            //    QueryCodeType queryType = QueryCodeType.Default;
            //    bool r = Enum.TryParse(m.OPCode, out queryType);
            //    if (!r)
            //    {
            //        continue;
            //    }
            //    bool result = false;
            //    switch (queryType)
            //    {
            //        case QueryCodeType.QLBLQuery:
            //            result=QLBLPayService.QLBLQuery(item, m);
            //            break;
            //        case QueryCodeType.Default:
            //            break;
            //    }
            //    if (result)
            //    {
            //        item.Status = 1;//如果查询到注资成功则，修改状态
            //        ibll.PayOrder.Update(item, new string[] { "Status" });
            //    }
            //}
            //ibll.PayOrder.SaveChanges();//批量更新

        }
        #endregion
    }
}
