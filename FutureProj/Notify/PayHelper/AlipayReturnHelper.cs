using Aop.Api.Util;
using EF.Common;
using EF.Entitys;
using Serv;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Notify.PayHelper
{
    public class AlipayReturnHelper: PayBase
    {
        //public void DealWithPayReturn()
        //{
        //    string respStr = string.Empty;
        //    ServiceLog log = new ServiceLog();
        //    log.ReqType = LogTypeConfig.NotifyMS;
        //    try
        //    {
        //        // 签名方式
        //        string sign_type = "RSA2";
        //        // 编码格式
        //        string charset = "UTF-8";
        //        /* 实际验证过程建议商户添加以下校验。
        //        1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
        //        2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
        //        3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
        //        4、验证app_id是否为该商户本身。
        //        */
        //        HttpRequest request = HttpContext.Current.Request;
        //        int i = 0;
        //        Dictionary<string, string> sArray = new Dictionary<string, string>();
        //        //string str = "gmt_create=2018-04-22+10%3a45%3a19&charset=UTF-8&gmt_payment=2018-04-22+10%3a45%3a29&notify_time=2018-04-22+10%3a49%3a15&subject=%e5%85%85%e5%80%bc&sign=fv8PhwTRLijg46mulGrrV%2f8wW%2f1opvrq6%2bO%2f0TdTjSRQescqbWA9sDfLSrrO56K5wjTZLA2KGAxPi%2fIIVPS8qQ4rq7NxQX0tNngv5L2S15KtQVt2DC%2fPNySCyQQ9oUerftwWec7n7ECkX4q6SvOQZmWKvYGEESfkUUO6ncZMcYmRJB7D6c2X4n%2fHVYHoREJ9sq08bOJNEys9jef1OdDOTDy0FbYJbbtyRUfgWJFZ8lckPP0ElI7hABKlWXX58X0Xf%2bCE6PN5Ie5s0nO88jq2sGzLK%2fX%2bS0RpQsRKhB6x7CjBBINEguhVQ5Lchzw9LTCMiEsdYbMXs4BSph6G7fVIXA%3d%3d&buyer_id=2088402573828421&body=%e6%89%ab%e7%a0%81%e5%85%85%e5%80%bc&invoice_amount=0.01&version=1.0&notify_id=4bf0e9c7c774e4f0718393ff2dac470j8t&fund_bill_list=%5b%7b%22amount%22%3a%220.01%22%2c%22fundChannel%22%3a%22ALIPAYACCOUNT%22%7d%5d&notify_type=trade_status_sync&out_trade_no=20180422104453703172042450511550&total_amount=0.01&trade_status=TRADE_SUCCESS&trade_no=2018042221001004420586001305&auth_app_id=2018041502561579&receipt_amount=0.01&point_amount=0.00&app_id=2018041502561579&buyer_pay_amount=0.01&sign_type=RSA2&seller_id=2088031924330410";//request.Form.ToString();
        //        string str = request.Form.ToString();
        //        str = HttpUtility.UrlDecode(str);
        //        LogHelper.Info<AlipayReturnHelper>("支付宝返回异步通知："+str);
        //        sArray = SignUtils.dicStringToDict(str);

        //        if (sArray.Count != 0)
        //        {
        //            //根据订单号获取相应商户信息
        //            string orderNo = sArray["out_trade_no"].ToString();
        //            //交易的总金额，//单位元
        //            string tranAmt = sArray["total_amount"].ToString();
        //            string status = sArray["trade_status"].ToString();
                    
        //            PayOrder order = iBll.PayOrder.Single(s => s.OrderNo == orderNo);
        //            View_PayMert paywayMer = iBll.View_PayMert.Single(s => s.MerNo == order.MerNo);
        //            log.BillNo = order.BillNo;
        //            log.ReqStr = str;
        //            //获取商户信息
        //            if (paywayMer != null)
        //            {
        //                //商户存在的时候才进行下步操作 
        //                bool flag = AlipaySignature.RSACheckV1(sArray, paywayMer.MerVerifyPublicKey, charset, sign_type, false);
        //                if (flag&& status.Equals("TRADE_SUCCESS"))
        //                {
        //                    //交易状态
        //                    //判断该笔订单是否在商户网站中已经做过处理
        //                    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
        //                    //请务必判断请求时的total_amount与通知时获取的total_fee为一致的
        //                    //如果有做过处理，不执行商户的业务程序
        //                    bool r = UpdateStatus(order, paywayMer, Convert.ToInt32(Convert.ToDecimal(tranAmt)*100));
        //                    if (r)
        //                    {
        //                        //商户处理成功后，返回状态
        //                        respStr = "success";
        //                    }
        //                }
        //                else
        //                {
        //                    //验签失败！ (验签主要是确认是否被劫持过)
        //                    //写入错误日志
        //                    LogHelper.Info<AlipayReturnHelper>("验签失败");
        //                    respStr = "fail";
        //                }
        //            }
        //            else
        //            {
        //                respStr = "fail";
        //            }

        //        }
        //        else
        //        {
        //            respStr = "fail";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error<AlipayReturnHelper>(ex);
        //        respStr = "fail";
        //    }
        //    finally
        //    {
        //        log.RespStr = respStr;
        //        log.AddDate = DateTime.Now;
        //        iBll.ServiceLog.Add(log);

        //        HttpContext.Current.Response.Write(respStr);
        //        HttpContext.Current.ApplicationInstance.CompleteRequest();
        //    }
        //}
    }
}