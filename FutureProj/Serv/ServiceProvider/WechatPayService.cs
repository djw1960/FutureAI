using EF.Common;
using EF.Entitys;
using Newtonsoft.Json;
using Serv.Entitys;
using Serv.Lib.Wechat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.ServiceProvider
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WechatPayService
    {
        //#region 扫码支付
        ///// <summary>
        ///// 自己生成二维码方式
        ///// 多次交互
        ///// </summary>
        ///// <param name="billno"></param>
        ///// <param name="amount"></param>
        ///// <param name="notifyUrl"></param>
        ///// <param name="paywayMer"></param>
        ///// <returns></returns>
        //public static string GetPrePayUrl(string billno, string amount, string notifyUrl, View_PayMerSignWay paywayMer)
        //{
        //    WxPayConfig config = new WxPayConfig();
        //    config.ApiUrl = paywayMer.MerScanApiUrl;
        //    config.APPID = paywayMer.MerNote;
        //    config.APPSECRET = paywayMer.MerNote2;
        //    config.MCHID = paywayMer.MerNo;
        //    config.KEY = paywayMer.MerEncryKey;
        //    config.NOTIFY_URL = notifyUrl;
        //    config.SSLCERT_PATH = paywayMer.MerPrivateKey;
        //    config.SSLCERT_PASSWORD = paywayMer.MerNo;

        //    WxPayData data = new WxPayData();
        //    data.SetValue("appid", config.APPID);//公众帐号id
        //    data.SetValue("mch_id", config.MCHID);//商户号
        //    data.SetValue("time_stamp", WxPayApi.GenerateTimeStamp());//时间戳
        //    data.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
        //    data.SetValue("product_id", billno);//商品ID
        //    data.SetValue("sign", data.MakeSign(config.KEY));//签名
        //    string str = ToUrlParams(data.GetValues());//转换为URL串
        //    string url = "weixin://wxpay/bizpayurl?" + str;
        //    return url;
        //}
        ///// <summary>
        ///// 接口下单返回二维码方式
        ///// </summary>
        ///// <param name="billno"></param>
        ///// <param name="amount"></param>
        ///// <param name="notifyUrl"></param>
        ///// <param name="ip">下单服务器IP</param>
        ///// <param name="paywayMer"></param>
        ///// <returns></returns>
        //public static void ScanPay(ReturnModel result, RequestParamsM model, PayOrder order, View_PayMerSignWay paywayMer)
        //{
        //    WxPayConfig config = new WxPayConfig();
        //    config.ApiUrl = paywayMer.MerScanApiUrl;
        //    config.APPID = paywayMer.MerNote;
        //    config.APPSECRET = paywayMer.MerNote2;
        //    config.MCHID = paywayMer.MerNo;
        //    config.KEY = paywayMer.MerEncryKey;
        //    config.NOTIFY_URL = paywayMer.NotifyUrlUS;
        //    config.SSLCERT_PATH = paywayMer.MerPrivateKey;
        //    config.SSLCERT_PASSWORD = paywayMer.MerNo;
        //    config.IP = order.IP;
        //    string orderno = Common.GetBillNo(32);//生成32位订单号
        //    order.OrderNo = orderno;
        //    string body = string.IsNullOrEmpty(order.Subject) ? "扫码充值" : order.Subject;
        //    WxPayData data = new WxPayData();
        //    data.SetValue("body", body);//商品描述
        //    //data.SetValue("attach", "");//附加数据
        //    data.SetValue("out_trade_no", order.OrderNo);//随机字符串
        //    data.SetValue("total_fee", order.Amount);//总金额 单位 分
        //    data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
        //    data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
        //    //data.SetValue("goods_tag", "");//商品标记-商品优惠信息
        //    data.SetValue("trade_type", "NATIVE");//交易类型
        //    data.SetValue("product_id", order.BillNo);//商品ID

        //    WxPayData r = WxPayApi.UnifiedOrder(config, data);//调用统一下单接口
        //    if (r.GetValue("return_code").Equals("SUCCESS"))
        //    {
        //        string url = r.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
        //        result.data = new PayContent() { Type = PayContentTypeConfig.ScanURL, Url = url, FormStr = "" };
        //        result.code = RespCodeConfig.Normal;
        //    }
        //    else
        //    {
        //        string msg = r.GetValue("return_msg").ToString();
        //        LogHelper.Info<WechatPayService>(string.Format("FormBuilder GetPayUrl:{0}", msg));
        //        result.code = RespCodeConfig.Faild;
        //        result.msg = "下单失败";
        //    }
        //}

        //public static ReturnModel Query(ReturnModel result, PayOrder order, View_PayMerSignWay paywayMer)
        //{
        //    WxPayConfig config = new WxPayConfig();
        //    config.ApiUrl = paywayMer.MerScanApiUrl;
        //    config.APPID = paywayMer.MerNote;
        //    config.APPSECRET = paywayMer.MerNote2;
        //    config.MCHID = paywayMer.MerNo;
        //    config.KEY = paywayMer.MerEncryKey;
        //    config.NOTIFY_URL = paywayMer.NotifyUrlUS;
        //    config.SSLCERT_PATH = paywayMer.MerPrivateKey;
        //    config.SSLCERT_PASSWORD = paywayMer.MerNo;
        //    config.IP = order.IP;
        //    ReturnModel _result = QueryOrder(config, "", order.OrderNo);
        //    return _result;
        //}

        ////
        ///// <summary>
        ///// 查询订单 可以根据任意一个订单号查询
        ///// </summary>
        ///// <param name="config"></param>
        ///// <param name="transaction_id">微信订单号</param>
        ///// <param name="out_trade_no">商户订单号</param>
        ///// <returns></returns>
        //private static ReturnModel QueryOrder(WxPayConfig config, string transaction_id, string out_trade_no = "")
        //{
        //    ReturnModel result = new ReturnModel() { code = 3 };
        //    string respStr = string.Empty;
        //    ServiceLog log = new ServiceLog();
        //    log.ReqType = LogTypeConfig.Query;
        //    try
        //    {
        //        WxPayData req = new WxPayData();
        //        if (!string.IsNullOrEmpty(transaction_id))
        //        {
        //            req.SetValue("transaction_id", transaction_id);
        //        }
        //        if (!string.IsNullOrEmpty(out_trade_no))
        //        {
        //            req.SetValue("out_trade_no", out_trade_no);
        //        }
        //        log.BillNo = out_trade_no;
        //        log.ReqStr = out_trade_no;
        //        result.ok = RespCodeConfig.NotOK;
        //        WxPayData res = WxPayApi.OrderQuery(config, req);
        //        var desc = res.GetValue("trade_state_desc").ToString();
        //        if (res.GetValue("return_code").ToString() == "SUCCESS" &&
        //            res.GetValue("result_code").ToString() == "SUCCESS")
        //        {
        //            result.code = RespCodeConfig.Normal;
        //            result.msg = desc;
        //            result.ok = RespCodeConfig.OK;
        //        }
        //        else
        //        {
        //            result.code = RespCodeConfig.Faild;
        //            result.msg = desc;
        //            result.ok = RespCodeConfig.NotOK;
        //        }
        //        respStr = JsonConvert.SerializeObject(res.GetValues()); //获取字典数据
        //        result.data = respStr;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.code = RespCodeConfig.ServerError;
        //        result.msg = ex.Message;
        //        result.ok = RespCodeConfig.NotOK;
        //        LogHelper.Error<AlipayService>("微信支付交易查询出现异常，详情：{0}", ex);
        //    }
        //    finally
        //    {
        //        EF.IService.IServiceSession iBll = OperationContext.BLLSession;
        //        log.RespStr = respStr;
        //        log.AddDate = DateTime.Now;
        //        iBll.ServiceLog.Add(log);
        //    }
        //    return result;
        //}
        //#endregion


        ///**
        //* 参数数组转换为url格式
        //* @param map 参数名与参数值的映射表
        //* @return URL字符串
        //*/
        //private static string ToUrlParams(SortedDictionary<string, object> map)
        //{
        //    string buff = "";
        //    foreach (KeyValuePair<string, object> pair in map)
        //    {
        //        buff += pair.Key + "=" + pair.Value + "&";
        //    }
        //    buff = buff.Trim('&');
        //    return buff;
        //}
    }
}
