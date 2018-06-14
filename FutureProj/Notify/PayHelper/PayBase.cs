using EF.Common;
using EF.Entitys;
using Newtonsoft.Json;
using Serv;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notify
{
    public abstract class PayBase
    {
        protected EF.IService.IServiceSession iBll = OperationContext.BLLSession;
        ///// <summary>
        ///// 服务端响应处理
        ///// </summary>
        ///// <param name="tradeNo"></param>
        ///// <param name="orderMoney"></param>
        //protected bool UpdateStatus(PayOrder order,View_PayMert payMert,int Amount)
        //{
        //    bool dresult = false;
        //    #region  业务处理
        //    //1.0 根据订单号，获取订单
        //    //2.0 确认订单状态，修改订单状态
        //    //3.0 通知业务系统订单状态
        //    if (order != null)
        //    {
        //        if (order.Status != 1)
        //        {
        //            order.Status = 1;
        //            iBll.PayOrder.Update(order, new string[] { "Status" });
        //            int num = iBll.PayOrder.SaveChanges();
        //            if (num > 0)
        //            {
        //                PayCustomer cust = iBll.PayCustomer.Single(s => s.CustmCode == order.CustomerNo);
        //                dresult = true;
        //                //调用业务系统接口，通知业务系统
        //                NotifyModel model = new NotifyModel();
        //                model.BillNo = order.BillNo;
        //                model.CustomerNo = order.CustomerNo;
        //                model.Amount = Amount.ToString();
        //                model.Status = "1";//成功
        //                model.Source = payMert.CompShotName;//商城名称
        //                model.Channel = payMert.MerName;//支付通道
        //                model.Service_charge = "0";//手续费
        //                model.Charge_val = "0";//手续费费率
        //                model.Charge_type = "2";//手续费计算方式
        //                model.Calculation_type = "1";//手续费计算规则
        //                model.OrderNo = order.OrderNo;
        //                string signStr = string.Format("CustomerNo={0}&BillNo={1}&Amount={2}&Status={3}{4}",
        //        model.CustomerNo, model.BillNo, model.Amount, "1", DESEncrypt.Decrypt(cust.MEncryKey, DESEncrypt.info));
        //                string sign = SignUtils.MD5(signStr, System.Text.Encoding.UTF8);
        //                model.Sign = sign;
        //                string p = JsonConvert.SerializeObject(model);
        //                ServiceLog log = new ServiceLog();
        //                log.BillNo = order.BillNo;
        //                log.ReqType = LogTypeConfig.NotifyMC;
        //                log.ReqStr = JsonConvert.SerializeObject(model);
        //                try
        //                {
        //                    string t = HttpUtils.HttpPost_JSON(cust.NotifyUrl, p);
        //                    log.RespStr = JsonConvert.SerializeObject(t);
        //                    if (t.ToUpper().Equals("SUCCESS"))
        //                    {
        //                        order.IsNotify = 1;
        //                        order.NotifyCount = 1;
        //                    }
        //                    else
        //                    {
        //                        //log -通知失败
        //                        order.NotifyCount = 1;//通知失败或未正常响应
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    order.NotifyCount = order.NotifyCount >= 0 ? order.NotifyCount + 1 : 1;//通知失败或未正常响应
        //                    LogHelper.Error<PayBase>("异步通知商城支付结果"+ex);
        //                }
        //                log.AddDate = DateTime.Now;
        //                iBll.ServiceLog.Add(log);
        //                iBll.PayOrder.Update(order, new string[] { "NotifyCount", "IsNotify" });
        //                iBll.SaveChanges();
        //            }
        //            else
        //            {
        //                //状态保存失败log
        //            }
        //        }
        //        else
        //        {
        //            //重复通知-未给支付方返回接收成功状态 log
        //        }
        //    }
        //    else
        //    {
        //        //订单不存在log
        //    }
        //    #endregion
        //    return dresult;
        //}
    }
}