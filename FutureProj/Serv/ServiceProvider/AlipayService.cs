using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Com.Alipay;
using Com.Alipay.Business;
using Com.Alipay.Domain;
using Com.Alipay.Model;
using EF.Common;
using EF.Entitys;
using Newtonsoft.Json;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace Serv.ServiceProvider
{
    public class AlipayService
    {
        
        //public static void WebPay(ReturnModel result, RequestParamsM model, PayOrder order, View_PayMerSignWay paywayMer)
        //{
        //    //入口，处理订单
        //    string orderno = Common.GetBillNo(32);//生成32位订单号
        //    order.OrderNo = orderno;
        //    string amount = ((decimal)order.Amount / 100).ToString("0.00");
        //    string returnUrl = order.MediaType == 1 ? paywayMer.ReturnUrl : paywayMer.ReturnUrlM;
        //    string tradeno = string.Empty;
        //    string formHTML = IAlipayWebPay(ref tradeno,paywayMer,order.Subject,order.SubjectDesrc, amount, order.OrderNo,returnUrl, order.MediaType == 1);

        //    if (string.IsNullOrEmpty(formHTML))
        //    {
        //        result.code = RespCodeConfig.Faild;
        //        result.msg = "下单失败";
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(tradeno))
        //        {
        //            order.TradeNo = tradeno;//支付宝内部流水号，用于查询
        //        }
        //        result.data = new PayContent() { Type = PayContentTypeConfig.FormStr, FormStr = formHTML, Url = "" };
        //        result.code = RespCodeConfig.Normal;
        //    }
        //}
        //public static void ScanPay(ReturnModel result, RequestParamsM model, PayOrder order, View_PayMerSignWay paywayMer)
        //{
        //    //入口，处理订单
        //    string orderno = Common.GetBillNo(32);//生成32位订单号
        //    order.OrderNo = orderno;
        //    string amount = ((decimal)order.Amount / 100).ToString("0.00");
        //    string returnUrl = order.MediaType == 1 ? paywayMer.ReturnUrl : paywayMer.ReturnUrlM;
        //    string tradeno = string.Empty;
        //    AlipayF2FPrecreateResult precreateResult = IScanPay(ref tradeno, paywayMer, order.Subject, order.SubjectDesrc, amount, order.OrderNo, returnUrl);
        //    switch (precreateResult.Status)
        //    {
        //        case ResultEnum.SUCCESS:
        //            //请求成功 由外层生成二维码
        //            //打印出 preResponse.QrCode 对应的条码 当前预下单请求生成的二维码码串，可以用二维码生成工具根据该码串值生成对应的二维码 OutTradeNo
        //            order.TradeNo = tradeno;//支付宝内部流水号，用于查询
        //            string enCodeString = precreateResult.response.QrCode;
        //            result.data = new PayContent() { Type = PayContentTypeConfig.ScanURL, FormStr = "", Url = enCodeString };
        //            result.code = RespCodeConfig.Normal;
        //            LogHelper.Info<AlipayService>("支付宝创建订单二维码成功");
        //            LogHelper.Info<AlipayService>("支付宝扫码支付当前预下单请求生成的二维码码串信息为：{0}", enCodeString);
        //            break;
        //        case ResultEnum.FAILED:
        //            string strBody = precreateResult.response.Body;
        //            result.code = RespCodeConfig.Faild;
        //            result.msg = "下单失败";
        //            LogHelper.Info<AlipayService>("支付宝扫码支付创建订单二维码失败!!!，详情为：{0}", strBody);
        //            break;

        //        case ResultEnum.UNKNOWN:
        //            result.code = RespCodeConfig.Faild;
        //            result.msg = "下单失败";
        //            if (precreateResult.response == null)
        //            {
        //                LogHelper.Info<AlipayService>("支付宝扫码支付配置或网络异常，请检查后重试");
        //            }
        //            else
        //            {
        //                LogHelper.Info<AlipayService>("支付宝扫码支付系统异常，请更新外部订单后重新发起请求");
        //            }
        //            break;
        //        default:
        //            LogHelper.Info<AlipayService>("不支持的返回状态，创建订单二维码返回异常!!!");
        //            break;
        //    }
        //}
        //public static ReturnModel Query(ReturnModel result,PayOrder order, View_PayMerSignWay paywayMer)
        //{
        //    ReturnModel _result = IAlipayQuery(order.OrderNo, "", paywayMer);
        //    return _result;
        //}

        //#region 阿里支付宝支付相关接口
        ///// <summary>
        ///// 支付宝收银台扫码接口
        ///// </summary>
        ///// <param name="order_money"></param>
        ///// <param name="order_num"></param>
        ///// <param name="strNotifyUrl"></param>
        ///// <param name="paywayMer"></param>
        ///// <param name="isPc"></param>
        ///// <returns></returns>
        //private static string IAlipayWebPay(ref string tradeno,View_PayMerSignWay paywayMer,string subject,string subjectdesrc, string order_money, string order_num, string returnUrl,  bool isPc = false)
        //{
        //    try
        //    {
        //        //默认返回参数为空
        //        string strBody = string.Empty;
        //        // 签名方式
        //        string sign_type = "RSA2";
        //        // 编码格式
        //        string charset = "UTF-8";
        //        //支付宝网关地址
        //        string serverUrl = paywayMer.MerScanApiUrl;
        //        //应用ID
        //        string appId = paywayMer.MerNo;
        //        //商户私钥
        //        string privateKey = paywayMer.MerPrivateKey;
        //        string alipay_public_key = paywayMer.MerVerifyPublicKey;
        //        DefaultAopClient client = new DefaultAopClient(serverUrl, appId, privateKey, "json", "1.0", sign_type, alipay_public_key, charset, false);
        //        //外部订单号，商户网站订单系统中唯一的订单号
        //        string out_trade_no = order_num;
        //        // 订单名称
        //        subject = string.IsNullOrEmpty(subject)? "充值":subject;
        //        // 付款金额
        //        string total_amout = order_money;
        //        // 商品描述
        //        string body = string.IsNullOrEmpty(subjectdesrc) ? "扫码充值" : subjectdesrc;
        //        // 支付中途退出返回商户网站地址
        //        string quit_url = "";
        //        if (isPc)
        //        {
        //            // 组装业务参数model
        //            AlipayTradePagePayModel model = new AlipayTradePagePayModel();
        //            model.Body = body;
        //            model.Subject = subject;
        //            model.TotalAmount = total_amout;
        //            model.OutTradeNo = out_trade_no;
        //            model.ProductCode = "FAST_INSTANT_TRADE_PAY";
        //            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();
        //            // 设置同步回调地址
        //            request.SetReturnUrl(returnUrl);
        //            // 设置异步通知接收地址
        //            request.SetNotifyUrl(paywayMer.NotifyUrlUS);
        //            // 将业务model载入到request
        //            request.SetBizModel(model);
        //            AlipayTradePagePayResponse response = client.pageExecute(request, null, "post");
        //            //没有异常即返回正常form表单
        //            strBody = response.Body;
        //            tradeno = response.TradeNo;
        //        }
        //        else
        //        {
        //            // 组装业务参数model
        //            AlipayTradeWapPayModel model = new AlipayTradeWapPayModel();
        //            model.Body = body;
        //            model.Subject = subject;
        //            model.TotalAmount = total_amout;
        //            model.OutTradeNo = out_trade_no;
        //            model.ProductCode = "QUICK_WAP_WAY";
        //            model.QuitUrl = quit_url;
        //            AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
        //            //设置支付完成同步回调地址
        //            request.SetReturnUrl(returnUrl);
        //            //设置支付完成异步通知接收地址
        //            request.SetNotifyUrl(paywayMer.NotifyUrlUS);
        //            //将业务model载入到request
        //            request.SetBizModel(model);
        //            AlipayTradeWapPayResponse response = client.pageExecute(request, null, "post");
        //            //没有异常即返回正常form表单
        //            strBody = response.Body;
        //            tradeno = response.TradeNo;

        //        }
        //        LogHelper.Info<AlipayService>("支付宝收银台支付From表单信息，详情为：{0}", strBody);
        //        return strBody;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error<AlipayService>("支付宝收银台支付出现异常，详情：{0}", ex);
        //    }
        //    return string.Empty;
        //}
        //private static AlipayF2FPrecreateResult IScanPay(ref string tradeno, View_PayMerSignWay paywayMer, string subject, string subjectdesrc, string order_money, string order_num, string returnUrl)
        //{
        //    try
        //    {
        //        // 签名方式
        //        string sign_type = "RSA2";
        //        // 编码格式
        //        string charset = "UTF-8";
        //        //支付宝网关地址
        //        string serverUrl = paywayMer.MerScanApiUrl;
        //        //应用ID
        //        string appId = paywayMer.MerNo;
        //        //商户私钥
        //        string privateKey = paywayMer.MerPrivateKey;
        //        string alipay_public_key = paywayMer.MerVerifyPublicKey;
        //        IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(serverUrl, appId, privateKey, "1.0",sign_type, alipay_public_key, charset);
        //        AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent(paywayMer,subject,subjectdesrc,order_money,order_num, appId);
        //        string out_trade_no = builder.out_trade_no;
        //        string notify_url = paywayMer.NotifyUrlUS;  //商户接收异步通知的地址
        //        AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder, notify_url);
                
        //        return precreateResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error<AlipayService>("支付宝扫码支付出现异常，详情：{0}", ex);
        //    }
        //    return null;
        //}
        ///// <summary>
        ///// 构造支付请求数据
        ///// </summary>
        ///// <returns>请求数据集</returns>
        //private static AlipayTradePrecreateContentBuilder BuildPrecreateContent(View_PayMerSignWay paywayMer, string subject, string subjectdesrc, string order_money, string order_num,string appId)
        //{
        //    //线上联调时，请输入真实的外部订单号。
        //    string out_trade_no = "";
        //    //if (String.IsNullOrEmpty(order_num))
        //    //{
        //    //    out_trade_no = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();
        //    //}
        //    //else
        //    //{
        //        out_trade_no = order_num;
        //    //}

        //    AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder();
        //    //收款账号
        //    //builder.seller_id = appId;
        //    //订单编号
        //    builder.out_trade_no = out_trade_no;
        //    //订单总金额
        //    builder.total_amount = order_money;
        //    //参与优惠计算的金额
        //    //builder.discountable_amount = "";
        //    //不参与优惠计算的金额
        //    //builder.undiscountable_amount = "";
        //    //订单名称
        //    builder.subject = subject;
        //    //自定义超时时间
        //    builder.timeout_express = "5m";
        //    //订单描述
        //    builder.body = "";
        //    //门店编号，很重要的参数，可以用作之后的营销 可选
        //    //builder.store_id = "test store id";
        //    //操作员编号，很重要的参数，可以用作之后的营销 可选
        //    //builder.operator_id = "test";

        //    //传入商品信息详情 可选
        //    //List<GoodsInfo> gList = new List<GoodsInfo>();
        //    //GoodsInfo goods = new GoodsInfo();
        //    //goods.goods_id = "goods id";
        //    //goods.goods_name = "goods name";
        //    //goods.price = "0.01";
        //    //goods.quantity = "1";
        //    //gList.Add(goods);
        //    //builder.goods_detail = gList;

        //    //系统商接入可以填此参数用作返佣
        //    //ExtendParams exParam = new ExtendParams();
        //    //exParam.sysServiceProviderId = "20880000000000";
        //    //builder.extendParams = exParam;

        //    return builder;

        //}
        ///// <summary>
        ///// 生成二维码并展示到页面上  最外层生成图片
        ///// </summary>
        ///// <param name="precreateResult">二维码串</param>
        //private static void DoWaitProcess(AlipayF2FPrecreateResult precreateResult)
        //{
        //    //打印出 preResponse.QrCode 对应的条码
        //    Bitmap bt;
        //    string enCodeString = precreateResult.response.QrCode;
        //    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        //    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
        //    qrCodeEncoder.QRCodeScale = 3;
        //    qrCodeEncoder.QRCodeVersion = 8;
        //    bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
        //    string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString()
        //     + ".jpg";
        //    bt.Save(HttpContext.Current.Server.MapPath("~/images/") + filename);
        //    //this.Image1.ImageUrl = "~/images/" + filename;

        //    //轮询订单结果
        //    //根据业务需要，选择是否新起线程进行轮询
        //    //ParameterizedThreadStart ParStart = new ParameterizedThreadStart(LoopQuery);
        //    //Thread myThread = new Thread(ParStart);
        //    //object o = precreateResult.response.OutTradeNo;
        //    //myThread.Start(o);

        //}

        ///// <summary>
        ///// 轮询
        ///// </summary>
        ///// <param name="o">订单号</param>
        //public static void LoopQuery(object o,IAlipayTradeService serviceClient)
        //{
        //    AlipayF2FQueryResult queryResult = new AlipayF2FQueryResult();
        //    int count = 100;
        //    int interval = 10000;
        //    string out_trade_no = o.ToString();

        //    for (int i = 1; i <= count; i++)
        //    {
        //        Thread.Sleep(interval);
        //        queryResult = serviceClient.tradeQuery(out_trade_no);
        //        if (queryResult != null)
        //        {
        //            if (queryResult.Status == ResultEnum.SUCCESS)
        //            {
        //                DoSuccessProcess(queryResult);
        //                return;
        //            }
        //        }
        //    }
        //    DoFailedProcess(queryResult);
        //}

        ///// <summary>
        ///// 请添加支付成功后的处理
        ///// </summary>
        //private static void DoSuccessProcess(AlipayF2FQueryResult queryResult)
        //{
        //    //支付成功，请更新相应单据
        //    LogHelper.Info<AlipayService>("支付宝扫码支付成功：外部订单号" + queryResult.response.OutTradeNo);
        //}

        ///// <summary>
        ///// 请添加支付失败后的处理
        ///// </summary>
        //private static void DoFailedProcess(AlipayF2FQueryResult queryResult)
        //{
        //    //支付失败，请更新相应单据
        //}
        ///// <summary>
        ///// 查询接口
        ///// </summary>
        ///// <param name="orderNum">商户订单号</param>
        ///// <param name="aLiTradeNo">支付宝流水号 两个订单号至少一个有值</param>
        ///// <param name="paywayMer">商户相关信息</param>
        ///// <returns></returns>
        //private static ReturnModel IAlipayQuery(string orderNum, string aLiTradeNo, View_PayMerSignWay paywayMer)
        //{
        //    ReturnModel result = new ReturnModel() {  code=3 };
        //    string respStr = string.Empty;
        //    ServiceLog log = new ServiceLog();
        //    log.ReqType = LogTypeConfig.Query;
        //    try
        //    {
        //        // 签名方式
        //        string sign_type = "RSA2";
        //        // 编码格式
        //        string charset = "UTF-8";
        //        //支付宝网关地址
        //        string serverUrl = paywayMer.MerScanApiUrl;
        //        //应用ID
        //        string appId = paywayMer.MerNo;
        //        //商户私钥
        //        string privateKey = paywayMer.MerPrivateKey;
        //        string alipay_public_key = paywayMer.MerVerifyPublicKey;
        //        DefaultAopClient client = new DefaultAopClient(serverUrl, appId, privateKey, "json", "1.0", sign_type, alipay_public_key, charset, false);
        //        // 商户订单号，和交易号不能同时为空
        //        string out_trade_no = orderNum;
        //        // 支付宝交易号，和商户订单号不能同时为空
        //        string trade_no = aLiTradeNo;
        //        AlipayTradeQueryModel model = new AlipayTradeQueryModel();
        //        model.OutTradeNo = out_trade_no;
        //        model.TradeNo = trade_no;
        //        AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
        //        request.SetBizModel(model);
        //        AlipayTradeQueryResponse response = null;
        //        response = client.Execute(request);
        //        log.BillNo = orderNum;
        //        log.ReqStr = orderNum;
        //        result.code= RespCodeConfig.Normal;
        //        result.msg = response.Body;
        //        result.data= response.Body;
        //        result.ok = RespCodeConfig.NotOK;
        //        respStr = response.Body;
        //        Dictionary<string, object> sArray = JsonConvert.DeserializeObject<Dictionary<string, object>>(respStr);
        //        if (sArray.Count>0)
        //        {
        //            string alipay_trade_query_response = sArray["alipay_trade_query_response"].ToString();
        //            Dictionary<string, object> sArray2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(alipay_trade_query_response);
        //            if (sArray2.Count>0)
        //            {
        //                try
        //                {
        //                    string status = sArray2["trade_status"].ToString();
        //                    if (status.Equals("TRADE_SUCCESS"))
        //                    {
        //                        result.ok = RespCodeConfig.OK;
        //                        result.msg = "交易成功";
        //                    }
        //                    else
        //                    {
        //                        result.ok = RespCodeConfig.NotOK;
        //                        var msg = sArray2["msg"];
        //                        try
        //                        {
        //                            if (msg != null && string.IsNullOrEmpty(msg.ToString()))
        //                            {
        //                                result.msg = msg.ToString();
        //                            }
        //                            else
        //                            {
        //                                result.msg = "交易失败";
        //                            }
                                    
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            result.code = RespCodeConfig.ServerError;
        //                            result.msg = "支付宝支付交易查询msg这个此字段不存在当前字典中";
        //                            result.ok = RespCodeConfig.NotOK;
        //                            LogHelper.Error<AlipayService>("支付宝支付交易查询出现异常，详情：{0}", ex);
        //                        }
                                

        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    result.code = RespCodeConfig.ServerError;
        //                    //result.msg = "支付宝支付交易查询trade_status这个此字段不存在当前字典中";
        //                    // 出异常，字段不在字典中时： 取sub_msg的值
        //                    var submsg = sArray2["sub_msg"];
        //                    result.msg = submsg.ToString();
        //                    result.ok = RespCodeConfig.NotOK;
        //                    LogHelper.Error<AlipayService>("支付宝支付交易查询出现异常，详情：{0}", ex);
        //                }
        //            }
        //            else
        //            {
        //                result.ok = RespCodeConfig.NotOK;
        //                result.msg = "接口返回为空";
        //            }
        //        }
        //        else
        //        {
        //            result.ok = RespCodeConfig.NotOK;
        //            result.msg = "接口返回为空";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.code = RespCodeConfig.ServerError;
        //        result.msg = ex.Message;
        //        result.ok = RespCodeConfig.NotOK;
        //        LogHelper.Error<AlipayService>("支付宝支付交易查询出现异常，详情：{0}", ex);
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
    }
    /// <summary>
    /// 返回信息
    /// </summary>
    public class ResultMsg
    {
        public bool IsError { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
