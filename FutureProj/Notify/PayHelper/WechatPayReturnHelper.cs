using EF.Common;
using EF.Entitys;
using Serv.Entitys;
using Serv.Lib.Wechat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Notify.PayHelper
{
    public class WechatPayReturnHelper: PayBase
    {
        //public void DealWithPayReturn()
        //{
        //    string respStr = string.Empty;
        //    ServiceLog log = new ServiceLog();
        //    log.ReqType = LogTypeConfig.NotifyMS;
        //    try
        //    {
        //        string resp = GetNotifyData();
                
        //        log.ReqStr = resp;
        //        //resp= "<xml><appid><![CDATA[wx1e53a40d38e900b6]]></appid><bank_type><![CDATA[CCB_DEBIT]]></bank_type><cash_fee><![CDATA[133]]></cash_fee><fee_type><![CDATA[CNY]]></fee_type><is_subscribe><![CDATA[N]]></is_subscribe><mch_id><![CDATA[1501277701]]></mch_id><nonce_str><![CDATA[dfa162ff4e274be695a80859acef024f]]></nonce_str><openid><![CDATA[ow_l-0UgT12v43Hi2eCehJzGLgU8]]></openid><out_trade_no><![CDATA[LTWX03900920820180410092658461]]></out_trade_no><result_code><![CDATA[SUCCESS]]></result_code><return_code><![CDATA[SUCCESS]]></return_code><sign><![CDATA[B78B272AD27F256284D8D2AE42EF4D64]]></sign><time_end><![CDATA[20180410092712]]></time_end><total_fee>133</total_fee><trade_type><![CDATA[NATIVE]]></trade_type><transaction_id><![CDATA[4200000097201804105569248684]]></transaction_id></xml>";
        //        if (string.IsNullOrEmpty(resp))
        //        {
        //            WxPayData res = new WxPayData();
        //            res.SetValue("return_code", "FAIL");
        //            res.SetValue("return_msg", "返回数据为空字符串");
        //            LogHelper.Error<WechatPayReturnHelper>(string.Format("The Pay result is error : {0} ", res.ToXml()));
        //            HttpContext.Current.Response.Write(res.ToXml());
        //            HttpContext.Current.ApplicationInstance.CompleteRequest();
        //            return;
        //        }


        //        //转换数据格式并验证签名
        //        WxPayData notifyData = new WxPayData();
        //        notifyData.FromXml(resp, "", false);//暂不做验签
        //        //1.0 获取orderid 
        //        if (!notifyData.IsSet("out_trade_no"))
        //        {
        //            // 若transaction_id不存在，则立即返回结果给微信支付后台
        //            WxPayData res = new WxPayData();
        //            res.SetValue("return_code", "FAIL");
        //            res.SetValue("return_msg", "支付结果中微信订单号不存在");
        //            LogHelper.Error<WechatPayReturnHelper>(string.Format("The Pay result is error : {0} ", res.ToXml()));
        //            HttpContext.Current.Response.Write(res.ToXml());
        //            HttpContext.Current.ApplicationInstance.CompleteRequest();
        //            return;
        //        }
        //        string orderno = notifyData.GetValue("out_trade_no").ToString();
        //        //2.0 根据订单号获取商户信息
        //        //根据订单号，获取订单所使用商户信息
        //        PayOrder order = iBll.PayOrder.Single(s => s.OrderNo == orderno);
        //        View_PayMert paywayMer = iBll.View_PayMert.Single(s => s.MerNo == order.MerNo);
        //        log.BillNo = order.BillNo;
        //        try
        //        {
        //            notifyData.FromXml(resp, paywayMer.MerEncryKey, true);//验签
        //        }
        //        catch (Exception ex)
        //        {
        //            //若签名错误，则立即返回结果给微信支付后台
        //            WxPayData res = new WxPayData();
        //            res.SetValue("return_code", "FAIL");
        //            res.SetValue("return_msg", ex.Message);
        //            LogHelper.Error<WechatPayReturnHelper>(string.Format("Sign check error : {0} ", res.ToXml()));
        //            HttpContext.Current.Response.Write(res.ToXml());
        //            HttpContext.Current.ApplicationInstance.CompleteRequest();
        //            return;
        //        }

        //        //检查支付结果中transaction_id是否存在
        //        if (!notifyData.IsSet("transaction_id"))
        //        {
        //            //若transaction_id不存在，则立即返回结果给微信支付后台
        //            WxPayData res = new WxPayData();
        //            res.SetValue("return_code", "FAIL");
        //            res.SetValue("return_msg", "支付结果中微信订单号不存在");
        //            LogHelper.Error<WechatPayReturnHelper>(string.Format("The Pay result is error : {0} ", res.ToXml()));
        //            HttpContext.Current.Response.Write(res.ToXml());
        //            HttpContext.Current.ApplicationInstance.CompleteRequest();
        //            return;
        //        }
        //        string transaction_id = notifyData.GetValue("transaction_id").ToString();

        //        WxPayConfig config = new WxPayConfig();
        //        config.ApiUrl = paywayMer.MerScanApiUrl;
        //        config.APPID = paywayMer.MerNote;
        //        config.APPSECRET = paywayMer.MerNote2;
        //        config.MCHID = paywayMer.MerNo;
        //        config.KEY = paywayMer.MerEncryKey;
        //        config.NOTIFY_URL = paywayMer.NotifyUrlUS;
        //        config.SSLCERT_PATH = paywayMer.MerPrivateKey;
        //        config.SSLCERT_PASSWORD = paywayMer.MerNo;
        //        config.IP = "127.0.0.1";
        //        //查询订单，判断订单真实性
        //        if (!QueryOrder(config, transaction_id))
        //        {
        //            //若订单查询失败，则立即返回结果给微信支付后台
        //            WxPayData res = new WxPayData();
        //            res.SetValue("return_code", "FAIL");
        //            res.SetValue("return_msg", "订单查询失败");
        //            LogHelper.Error<WechatPayReturnHelper>(string.Format("Order query failure : {0} ", res.ToXml()));
        //            HttpContext.Current.Response.Write(res.ToXml());
        //            HttpContext.Current.ApplicationInstance.CompleteRequest();
        //            return;
        //        }
        //        //查询订单成功
        //        else
        //        {
        //            //订单成功支付
        //            if (notifyData.IsSet("return_code") && notifyData.IsSet("result_code") && notifyData.GetValue("result_code").ToString().Equals("SUCCESS"))
        //            {
        //                bool r = false;
        //                int amount = Convert.ToInt32(notifyData.GetValue("total_fee"));

        //                r = UpdateStatus(order,paywayMer, amount);
        //                if (r)
        //                {
        //                    WxPayData res = new WxPayData();
        //                    res.SetValue("return_code", "SUCCESS");
        //                    res.SetValue("return_msg", "OK");
        //                    LogHelper.Error<WechatPayReturnHelper>(string.Format("order query success : {0} ", res.ToXml()));
        //                    HttpContext.Current.Response.Write(res.ToXml());
        //                    HttpContext.Current.ApplicationInstance.CompleteRequest();
        //                    return;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //写入到日志中
        //        LogHelper.Error<WechatPayReturnHelper>(ex);
        //    }
        //    finally
        //    {
        //        log.RespStr = respStr;
        //        log.AddDate = DateTime.Now;
        //        iBll.ServiceLog.Add(log);
        //        HttpContext.Current.Response.End();
        //    }

        //}
        ////
        ///// <summary>
        ///// 查询订单 可以根据任意一个订单号查询
        ///// </summary>
        ///// <param name="config"></param>
        ///// <param name="transaction_id">微信订单号</param>
        ///// <param name="out_trade_no">商户订单号</param>
        ///// <returns></returns>
        //private bool QueryOrder(WxPayConfig config, string transaction_id,string out_trade_no="")
        //{
        //    WxPayData req = new WxPayData();
        //    if (!string.IsNullOrEmpty(transaction_id))
        //    {
        //        req.SetValue("transaction_id", transaction_id);
        //    }
        //    if (!string.IsNullOrEmpty(out_trade_no))
        //    {
        //        req.SetValue("out_trade_no", out_trade_no);
        //    }
        //    WxPayData res = WxPayApi.OrderQuery(config, req);
        //    if (res.GetValue("return_code").ToString() == "SUCCESS" &&
        //        res.GetValue("result_code").ToString() == "SUCCESS")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// 接收从微信支付后台发送过来的数据并验证签名
        ///// </summary>
        ///// <returns>微信支付后台返回的数据</returns>
        //public string GetNotifyData()
        //{
        //    //接收从微信后台POST过来的数据
        //    System.IO.Stream s = HttpContext.Current.Request.InputStream;
        //    int count = 0;
        //    byte[] buffer = new byte[1024];
        //    StringBuilder builder = new StringBuilder();
        //    while ((count = s.Read(buffer, 0, 1024)) > 0)
        //    {
        //        builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
        //    }
        //    s.Flush();
        //    s.Close();
        //    s.Dispose();
        //    LogHelper.Info<WechatPayReturnHelper>(string.Format("Receive data from WeChat :{0}", builder.ToString()));
        //    return builder.ToString();
        //}
    }
}