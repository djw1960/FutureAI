using EF.Common;
using EF.Entitys;
using Newtonsoft.Json;
using Serv.Entitys;
using Serv.Helper;
using Serv.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Serv
{
    public class PayControl
    {
        private static EF.IService.IServiceSession ibll = OperationContext.BLLSession;

//        #region 业务系统直接对接
//        ///// <summary>
//        ///// 1000-获取当前客户可用的支付通道列表
//        ///// </summary>
//        ///// <param name="result"></param>
//        ///// <param name="param"></param>
//        //public static void GetChannelList(ReturnModel result, RequestParamsM param)
//        //{
//        //    #region 参数验证+签名验证
//        //    if (string.IsNullOrEmpty(param.CustomerNo))
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "参数异常";
//        //        return;
//        //    }
//        //    PayCustomer custm = ibll.PayCustomer.Single(a => a.CustmCode == param.CustomerNo);
//        //    if (custm == null)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    //拼接签名字符串
//        //    string signStr = param.No + param.CustomerNo + DESEncrypt.Decrypt(custm.MEncryKey, DESEncrypt.info);
//        //    if (param.SignType.Equals("MD5"))
//        //    {
//        //        if (!SignUtils.MD5(signStr, Encoding.UTF8).Equals(param.Sign))
//        //        {
//        //            result.code = RespCodeConfig.SignError;
//        //            result.msg = "签名错误";
//        //            return;
//        //        }
//        //    }
//        //    else if (param.SignType.Equals("RSA")) { }
//        //    else { }
//        //    #endregion
//        //    var list = ibll.View_IPayChannelList.where(a => a.CustomerNo == param.CustomerNo).ToList();
//        //    if (list.Count > 0)
//        //    {
//        //        var rlist = list.Select(s => new
//        //        {
//        //            s.SignNo,
//        //            s.CustomerNo,
//        //            s.PayWay,
//        //            s.Orderby,
//        //            s.DataType
//        //        });
//        //        result.code = RespCodeConfig.Normal;
//        //        result.data = rlist;
//        //    }
//        //    else
//        //    {
//        //        result.code = RespCodeConfig.ServerError;
//        //        result.data = "无可用支付通道";
//        //    }
//        //}
//        ///// <summary>
//        ///// 1001-根据支付签约号获取支付通道支持的银行信息
//        ///// </summary>
//        ///// <param name="result"></param>
//        ///// <param name="param"></param>
//        //public static void GetChannelBankList(ReturnModel result, RequestParamsM param)
//        //{
//        //    #region 参数验证+签名验证
//        //    if (string.IsNullOrEmpty(param.CustomerNo) || string.IsNullOrEmpty(param.SignNo))
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "参数异常";
//        //        return;
//        //    }
//        //    PayCustomer custm = ibll.PayCustomer.Single(a => a.CustmCode == param.CustomerNo);
//        //    if (custm == null)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    //拼接签名字符串
//        //    string signStr = param.SignNo + param.No + param.CustomerNo + DESEncrypt.Decrypt(custm.MEncryKey, DESEncrypt.info);
//        //    if (param.SignType.Equals("MD5"))
//        //    {
//        //        if (!SignUtils.MD5(signStr, Encoding.UTF8).Equals(param.Sign))
//        //        {
//        //            result.code = RespCodeConfig.SignError;
//        //            result.msg = "签名错误";
//        //            return;
//        //        }
//        //    }
//        //    else if (param.SignType.Equals("RSA")) { }
//        //    else { }
//        //    #endregion

//        //    var list = ibll.View_IPayChannelBankList.where(a => a.SignNo == param.SignNo).ToList();
//        //    if (list.Count > 0)
//        //    {
//        //        var rlist = list.Select(s => new
//        //        {
//        //            s.SignNo,
//        //            s.BankName,
//        //            s.BankCode,
//        //        });
//        //        result.code = RespCodeConfig.Normal;
//        //        result.data = rlist;
//        //    }
//        //    else
//        //    {
//        //        result.code = RespCodeConfig.ServerError;
//        //        result.data = "无支持银行";
//        //    }
//        //}
//        ///// <summary>
//        ///// 1010-网银接口处理
//        ///// </summary>
//        ///// <param name="param"></param>
//        ///// <returns></returns>
//        //public static void WebPay(ReturnModel result, RequestParamsM param)
//        //{
//        //    //1.0 验证签名
//        //    //2.0 验证参数
//        //    //3.0 记录--订单创建，C请求M日志，M响应C日志
//        //    #region 签名验证+参数验证
//        //    if (string.IsNullOrEmpty(param.CustomerNo) || string.IsNullOrEmpty(param.SignNo))
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "参数异常";
//        //        return;
//        //    }
//        //    PayCustomer custm = ibll.PayCustomer.Single(a => a.CustmCode == param.CustomerNo);
//        //    if (custm == null)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    View_PayMerSignWay payMert = ibll.View_PayMerSignWay.Single(a => a.SignNo == param.SignNo);
//        //    PayWayCodeType paywayType = PayWayCodeType.DinPay;
//        //    bool r = Enum.TryParse(payMert.PayWayCode, out paywayType);
//        //    if (!r)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    //验证billToken
//        //    PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//        //    if (order == null)
//        //    {
//        //        result.code = RespCodeConfig.Faild;
//        //        result.msg = "数据异常";
//        //    }
//        //    #endregion

//        //    switch (paywayType)
//        //    {
//        //        case PayWayCodeType.ZFTWebPay:
//        //            ZFTPayService.WebPay(result,param.BankValue, order, payMert);
//        //            break;
//        //        case PayWayCodeType.KLTWebPay:
//        //            KLTPayService.WebPay(result, param, order, payMert);
//        //            break;
//        //    }

//        //    #region 创建订单+写响应日志
//        //    if (result.code == RespCodeConfig.Normal)
//        //    {
//        //        //创建订单，写响应日志
//        //        order.MerNo = payMert.MerNo;
//        //        order.SignNo = param.SignNo;
//        //        ibll.PayOrder.Update(order,null);
//        //        ServiceLog log = new ServiceLog();
//        //        log.BillNo = param.BillNo;
//        //        log.ReqType = LogTypeConfig.RespMC;
//        //        log.RespStr = result.data.ToString();
//        //        log.AddDate = DateTime.Now;
//        //        AddServiceLog(log);
//        //    }
//        //    #endregion
//        //}
//        ///// <summary>
//        ///// 1015-快捷，跳转商户或者银联绑卡的快捷
//        ///// </summary>
//        ///// <param name="result"></param>
//        ///// <param name="param"></param>
//        //public static void KJSignUp(ReturnModel result, RequestParamsM param)
//        //{
//        //    #region 签名验证+参数验证
//        //    if (string.IsNullOrEmpty(param.CustomerNo) || string.IsNullOrEmpty(param.SignNo))
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "参数异常";
//        //        return;
//        //    }
//        //    PayCustomer custm = ibll.PayCustomer.Single(a => a.CustmCode == param.CustomerNo);
//        //    if (custm == null)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    View_PayMerSignWay payMert = ibll.View_PayMerSignWay.Single(a => a.SignNo == param.SignNo);
//        //    PayWayCodeType paywayType = PayWayCodeType.DinPay;
//        //    bool r = Enum.TryParse(payMert.PayWayCode, out paywayType);
//        //    if (!r)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    //拼接签名字符串
//        //    string signStr = param.BillNo + param.Amount + param.BankCode + param.No + param.CustomerNo + DESEncrypt.Decrypt(custm.MEncryKey, DESEncrypt.info);
//        //    if (param.SignType.Equals("MD5"))
//        //    {
//        //        if (!SignUtils.MD5(signStr, Encoding.UTF8).Equals(param.Sign))
//        //        {
//        //            result.code = RespCodeConfig.SignError;
//        //            result.msg = "签名错误";
//        //            return;
//        //        }
//        //    }
//        //    else if (param.SignType.Equals("RSA")) { }
//        //    else { }
//        //    #endregion
//        //    //验证billToken
//        //    PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//        //    if (order == null)
//        //    {
//        //        result.code = RespCodeConfig.Faild;
//        //        result.msg = "数据异常";
//        //    }
//        //    switch (paywayType)
//        //    {
//        //        case PayWayCodeType.ZFTPayKJ:
//        //            ZFTPayService.KJPay(result, param, order, payMert);
//        //            break;
//        //        case PayWayCodeType.KLTPayKJ:
//        //            KLTPayService.KJWebSignPay(result, param, order, payMert);
//        //            break;
//        //        default:
//        //            //如果接口走API模式
//        //            result.code = RespCodeConfig.Normal;
//        //            result.data = new PayContent() { Type = PayContentTypeConfig.APIKJ, Url = "", FormStr = ""};
//        //            break;
//        //    }

//        //    #region 创建订单+写响应日志
//        //    if (result.code == RespCodeConfig.Normal)
//        //    {
//        //        //创建订单，写响应日志
//        //        order.MerNo = payMert.MerNo;
//        //        order.SignNo = param.SignNo;
//        //        ibll.PayOrder.Update(order,null);
//        //        ServiceLog log = new ServiceLog();
//        //        log.BillNo = param.BillNo;
//        //        log.ReqType = LogTypeConfig.RespMC;
//        //        log.RespStr = result.data.ToString();
//        //        log.AddDate = DateTime.Now;
//        //        AddServiceLog(log);
//        //    }
//        //    #endregion
//        //}
//        ///// <summary>
//        ///// 1016-快捷，API支付发送验证码
//        ///// </summary>
//        ///// <param name="result"></param>
//        ///// <param name="param"></param>
//        //public static void KJCreateOrder(ReturnModel result, RequestParamsM param)
//        //{
//        //    //1.0 验证签名
//        //    //2.0 验证参数
//        //    //3.0 记录--创建订单，C请求M日志，M请求S日志，S响应M日志，M响应C日志
//        //    #region 签名验证+参数验证
//        //    if (string.IsNullOrEmpty(param.CustomerNo) || string.IsNullOrEmpty(param.SignNo))
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "参数异常";
//        //        return;
//        //    }
//        //    PayCustomer custm = ibll.PayCustomer.Single(a => a.CustmCode == param.CustomerNo);
//        //    if (custm == null)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    View_PayMerSignWay payMert = ibll.View_PayMerSignWay.Single(a => a.SignNo == param.SignNo);
//        //    PayWayCodeType paywayType = PayWayCodeType.DinPay;
//        //    bool r = Enum.TryParse(payMert.PayWayCode, out paywayType);
//        //    if (!r)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    //拼接签名字符串
//        //    string signStr = param.BillNo + param.Amount + param.BankCode + param.No + param.CustomerNo + DESEncrypt.Decrypt(custm.MEncryKey, DESEncrypt.info);
//        //    if (param.SignType.Equals("MD5"))
//        //    {
//        //        if (!SignUtils.MD5(signStr, Encoding.UTF8).Equals(param.Sign))
//        //        {
//        //            result.code = RespCodeConfig.SignError;
//        //            result.msg = "签名错误";
//        //            return;
//        //        }
//        //    }
//        //    else if (param.SignType.Equals("RSA")) { }
//        //    else { }
//        //    #endregion
//        //    //验证billToken
//        //    PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//        //    if (order == null)
//        //    {
//        //        result.code = RespCodeConfig.Faild;
//        //        result.msg = "数据异常";
//        //    }
//        //    switch (paywayType)
//        //    {
//        //        case PayWayCodeType.KLTPayKJ:
//        //            KLTPayService.KJCreateOrder(result, param, order, payMert);
//        //            break;
//        //    }

//        //    #region 创建订单+写响应日志
//        //    if (result.code == RespCodeConfig.Normal)
//        //    {
//        //        //创建订单，写响应日志
//        //        order.MerNo = payMert.MerNo;
//        //        order.SignNo = param.SignNo;
//        //        ibll.PayOrder.Update(order,null);
//        //        ServiceLog log = new ServiceLog();
//        //        log.BillNo = param.BillNo;
//        //        log.ReqType = LogTypeConfig.RespMC;
//        //        log.RespStr = result.data.ToString();
//        //        log.AddDate = DateTime.Now;
//        //        AddServiceLog(log);
//        //    }
//        //    #endregion
//        //}
//        ///// <summary>
//        ///// 1017-快捷，API确认支付
//        ///// </summary>
//        ///// <param name="result"></param>
//        ///// <param name="param"></param>
//        //public static void KJConfirmPay(ReturnModel result, RequestParamsM param)
//        //{
//        //    #region 签名验证+参数验证
//        //    if (string.IsNullOrEmpty(param.CustomerNo) || string.IsNullOrEmpty(param.SignNo))
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "参数异常";
//        //        return;
//        //    }
//        //    PayCustomer custm = ibll.PayCustomer.Single(a => a.CustmCode == param.CustomerNo);
//        //    if (custm == null)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    View_PayMerSignWay payMert = ibll.View_PayMerSignWay.Single(a => a.SignNo == param.SignNo);
//        //    PayWayCodeType paywayType = PayWayCodeType.DinPay;
//        //    bool r = Enum.TryParse(payMert.PayWayCode, out paywayType);
//        //    if (!r)
//        //    {
//        //        result.code = RespCodeConfig.ArgumentExp;
//        //        result.msg = "数据异常";
//        //        return;
//        //    }
//        //    //拼接签名字符串
//        //    string signStr = param.BillNo + param.Amount + param.BankCode + param.No + param.CustomerNo + DESEncrypt.Decrypt(custm.MEncryKey, DESEncrypt.info);
//        //    if (param.SignType.Equals("MD5"))
//        //    {
//        //        if (!SignUtils.MD5(signStr, Encoding.UTF8).Equals(param.Sign))
//        //        {
//        //            result.code = RespCodeConfig.SignError;
//        //            result.msg = "签名错误";
//        //            return;
//        //        }
//        //    }
//        //    else if (param.SignType.Equals("RSA")) { }
//        //    else { }
//        //    #endregion
//        //    //验证billToken
//        //    PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//        //    if (order == null)
//        //    {
//        //        result.code = RespCodeConfig.Faild;
//        //        result.msg = "数据异常";
//        //    }
//        //    switch (paywayType)
//        //    {
//        //        case PayWayCodeType.KLTPayKJ:
//        //            KLTPayService.KJConfirmPay(result, param, order, payMert);
//        //            break;
//        //    }

//        //    #region 创建订单+写响应日志
//        //    if (result.code == RespCodeConfig.Normal)
//        //    {
//        //        //创建订单，写响应日志
//        //        order.MerNo = payMert.MerNo;
//        //        order.SignNo = param.SignNo;
//        //        ibll.PayOrder.Update(order,null);
//        //        ServiceLog log = new ServiceLog();
//        //        log.BillNo = param.BillNo;
//        //        log.ReqType = LogTypeConfig.RespMC;
//        //        log.RespStr = result.data.ToString();
//        //        log.AddDate = DateTime.Now;
//        //        AddServiceLog(log);
//        //    }
//        //    #endregion
//        //}
//        #endregion

//        #region 收银台方式对接

//        /// <summary>
//        /// 2010-获取订单信息
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_GetOrderInfo(ReturnModel result, RequestParamsM param)
//        {
//            #region 参数验证+签名验证
//            if (string.IsNullOrEmpty(param.BillToken))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "不存在该订单";
//                return;
//            }
//            //验证billToken
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "不存在该订单";
//                return;
//            }
//            if (!string.IsNullOrEmpty(order.OrderNo))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "订单已失效";
//                return;
//            }
//            //获取当前订单的使用的通道,如果不是钱进支付则处理订单金额
//            int amount = 0;
//            string signno = string.IsNullOrEmpty(param.SignNo) ? order.SignNo : param.SignNo;
//            View_IPayChannelList paywayMert = ibll.View_IPayChannelList.Single(s => s.SignNo == signno && s.CustomerNo == order.CustomerNo&&s.UIType==order.MediaType);
//            if (null == paywayMert)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "支付通道异常，请刷新重试！";
//                return;
//            }
//            else
//            {
//                if (!paywayMert.SerNo.Equals("XS0038")&&!string.IsNullOrEmpty(param.SignNo))//非钱进支付，处理小数，钱进支付保留原值
//                {
//                    decimal m = decimal.Round(((decimal)order.Amount/100), 0, MidpointRounding.AwayFromZero);//四舍五入处理
//                    amount = Convert.ToInt32(m*100);//单位分
//                }
//            }

//            if (order.MediaType != param.MediaType && param.MediaType > 0)
//            {
//                if (order.Amount != amount&&amount>0)
//                {
//                    order.Amount = amount;
//                }
//                order.MediaType = param.MediaType;
//                ibll.PayOrder.Update(order, new string[] { "MediaType", "Amount" });
//                ibll.PayOrder.SaveChanges();
//            }
//            else if (order.Amount != amount && amount > 0)
//            {
//                order.Amount = amount;
//                ibll.PayOrder.Update(order, new string[] {"Amount" });
//                ibll.PayOrder.SaveChanges();
//            }

//            #endregion
//            result.data = new {
//                BillNo=order.BillNo,
//                AddDate=order.AddDate.ToString("yyyy-MM-dd HH:mm:ss"),
//                Amount= String.Format("{0:N2}",(Decimal) order.Amount / 100)
//            };
//            result.code = RespCodeConfig.Normal;
//        }
//        /// <summary>
//        /// 2000-初始化订单
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_OrderInit(ReturnModel result, RequestParamsM param)
//        {
//            #region 参数验证+签名验证
//            if (string.IsNullOrEmpty(param.CustomerNo))
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "参数异常";
//                return;
//            }
//            if (string.IsNullOrEmpty(param.Subject))
//            {
//                List<Goods> glist = ibll.Goods.where(a => a.IsAvailable == 1).ToList();
//                if (glist.Count > 0)
//                {
//                    int index = Common.GetRandomSequence2(glist.Count(), 1)[0];
//                    param.Subject = glist[index].GoodsName;
//                }
//                else
//                {
//                    result.code = RespCodeConfig.ArgumentExp;
//                    result.msg = "参数异常";
//                    return;
//                }
//            }
//            PayCustomer custm = ibll.PayCustomer.Single(a => a.CustmCode == param.CustomerNo);
//            if (custm == null)
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "数据异常";
//                return;
//            }

//            //拼接签名字符串 No={0}&CustomerNo={1}&Account={2}&BillNo={3}&Amount={4}{5}
//            string signStr = string.Format("No={0}&CustomerNo={1}&Account={2}&BillNo={3}&Amount={4}{5}",
//                param.No,param.CustomerNo,param.Account,param.BillNo,param.Amount, DESEncrypt.Decrypt(custm.MEncryKey, DESEncrypt.info)
//                );
//            if (!param.Sign.Equals("djw")&& !SignUtils.MD5(signStr, Encoding.UTF8).Equals(param.Sign))
//            {
//                result.code = RespCodeConfig.SignError;
//                result.msg = "签名错误";
//                return;
//            }
//            #endregion

//            int c = ibll.PayOrder.where(a => a.BillNo == param.BillNo).Count();
//            if (c>0)
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "订单已存在";
//                return;
//            }
//            //支付宝和微信策略，返回URL地址不再直接返回billtoken
//            View_IPayChannelList channel = null;

//            #region 支付通道选择策略
//            //20180508按通道顺序执行，先获取上一家订单使用商户
//            PayOrder preorder = null;
//            List<PayOrder> templist = ibll.PayOrder.where(s => s.CustomerNo == param.CustomerNo && !string.IsNullOrEmpty(s.OrderNo)).OrderByDescending(s => s.ID).Take(1).ToList();
//            if (templist.Count==1)
//            {
//                preorder = templist[0];
//            }
//            //1.0 根据用户注资金额，选用通道，获取金额范围之内的通道，选取当天使用次数最少的给客户用
//            var plist = ibll.View_IPayChannelList.where(a => a.CustomerNo == param.CustomerNo && a.UIType == param.MediaType && !string.IsNullOrEmpty(a.CompSite) && a.CustomType.Contains(param.AccountType.ToString())).ToList();
//            if (plist.Count > 0)
//            {
//                if (preorder == null||!plist.Exists(s=>s.SignNo==preorder.SignNo))
//                {
//                    #region 最小策略
//                    int money = Convert.ToInt32(param.Amount);
//                    plist = plist.Where(s => s.MinLimit <= money && s.MaxLimit >= money).ToList();
//                    if (plist.Count > 0)
//                    {
//                        channel = plist.OrderBy(s => s.SumTimes).First();
//                    }
//                    else
//                    {
//                        var m = plist.Max(a => a.MaxLimit);
//                        int maxLimit = m.HasValue ? m.Value : 0;
//                        result.code = RespCodeConfig.ArgumentExp;
//                        result.msg = maxLimit <= 0 ? "超过通道单笔限额" : string.Format("超过通道单笔限额{0}", maxLimit / 100);
//                        return;
//                    } 
//                    #endregion
//                }
//                else
//                {
//                    #region 顺序策略
//                    View_IPayChannelList preChannel = plist.SingleOrDefault(a => a.SignNo == preorder.SignNo);
//                    int money = Convert.ToInt32(param.Amount);
//                    plist = plist.Where(s => s.MinLimit <= money && s.MaxLimit >= money).ToList();
//                    if (plist.Count > 0)
//                    {
//                        int index = plist.IndexOf(preChannel);
//                        if (index + 1 >= plist.Count)
//                        {
//                            index = 0;

//                        }
//                        else if (index + 1 < plist.Count)
//                        {
//                            index = index + 1;
//                        }
//                        channel = plist[index];
//                    }
//                    else
//                    {
//                        var m = plist.Max(a => a.MaxLimit);
//                        int maxLimit = m.HasValue?m.Value:0;
//                        result.code = RespCodeConfig.ArgumentExp;
//                        result.msg =maxLimit<=0?"超过通道单笔限额":string.Format("超过通道单笔限额{0}",maxLimit/100);
//                        return;
//                    }
//                    #endregion
//                }
                
//            }
//            else
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "无可用通道";
//                return;
//            }
//            #endregion

//            //1-创建订单 2-返回订单识别码OrderAction
//            PayOrder model = new PayOrder();
//            model.BillNo = param.BillNo;
//            model.Amount =Convert.ToInt32(param.Amount);//单位分
//            model.Account = param.Account;
//            model.CustomerNo = param.CustomerNo;
//            model.BillToken = Common.BillToken(param.Account);
//            model.AccountType =param.AccountType;
//            model.MediaType = param.MediaType;
//            model.SignNo = channel.SignNo;
//            model.Subject = param.Subject;
//            model.SubjectDesrc = param.SubjectDesrc;
//            model.AddDate = DateTime.Now;
//            model.Status = 0;
//            model.IsNotify = 0;
//            model.IsDel = 0;
//            ibll.PayOrder.Add(model);
//            int n=ibll.PayOrder.SaveChanges();
//            if (n > 0)
//            {
//                string payurl = param.MediaType == 1 ? channel.CompSite + "/paycenter/pc/index.html?billtoken=" : channel.CompSite + "/paycenter/wap/index.html?billtoken=";
//                if (SysConfig.IsTest)
//                {//如果是测试环境则配置为测试
//                    payurl = param.MediaType == 1 ? "http://tpaycent.mc-forex.net:4526" + "/paycenter/pc/index.html?billtoken=" : "http://tpaycent.mc-forex.net:4526" + "/paycenter/wap/index.html?billtoken=";
//                }
//                switch (param.Ver)
//                {
//                    case 100:
//                        result.data = new { url = payurl + model.BillToken, channel = channel.MerName };
//                        break;
//                    default:
//                        result.data = payurl + model.BillToken;
//                        break;
//                }
                
//                result.code = RespCodeConfig.Normal;
//            }
//            else
//            {
//                result.code = RespCodeConfig.Faild;
//            }

//        }
//        /// <summary>
//        /// 2001-获取支付通道列表
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_GetChannelList(ReturnModel result, RequestParamsM param)
//        {
//            //根据用户所属Customer，用户类型，当前注资平台 返回支付通道
//#region 参数验证+签名验证
//            //验证billToken
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "不存在该订单";
//                return;
//            }
//            if (!string.IsNullOrEmpty(order.OrderNo))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "订单已失效";
//                return;
//            }
//            #endregion
//            if (order.MediaType != param.MediaType && param.MediaType > 0)
//            {
//                order.MediaType = param.MediaType;
//                ibll.PayOrder.Update(order, new string[] { "MediaType"});
//                ibll.PayOrder.SaveChanges();
//            }
//            List<View_IPayChannelList> currentlist = null;
//            var list = ibll.View_IPayChannelList.where(a =>a.CustomerNo==order.CustomerNo&& a.UIType==order.MediaType&&a.CustomType.Contains(order.AccountType.ToString())).ToList();
//            if (list.Count > 0)
//            {
//                if (!string.IsNullOrEmpty(param.MerNo))
//                {
//                    currentlist= list.Where(a => a.MerNo==param.MerNo).ToList();
//                    if (currentlist.Count == 0)
//                    {
//                        currentlist = list.OrderBy(s => s.SumTimes).Take(1).ToList();
//                    }
//                }
//                else
//                {
//                    currentlist = list.Where(a => a.SignNo == order.SignNo).ToList();
//                    if (currentlist.Count == 0)
//                    {
//                        currentlist = list.OrderBy(s => s.SumTimes).Take(1).ToList();
//                    }
//                }
//            }
//            else
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "无可用支付通道";
//                return;
//            }
//            if (currentlist!=null&&currentlist.Count > 0)
//            {
//                var rlist = currentlist.Select(s => new
//                {
//                    s.SignNo,
//                    s.PayWay,
//                    s.DataType,
//                    s.Orderby,
//                });
//                result.code = RespCodeConfig.Normal;
//                result.data = rlist;
//            }
//            else
//            {
//                result.code = RespCodeConfig.ServerError;
//                result.msg = "无可用支付通道";
//            }
//        }
//        /// <summary>
//        /// 2002-根据支付通道获取银行列表
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_GetChannelBankList(ReturnModel result, RequestParamsM param)
//        {
//#region 参数验证+签名验证
//            //验证billToken
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            if (!string.IsNullOrEmpty(order.OrderNo))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "订单已失效";
//                return;
//            }
//            if (string.IsNullOrEmpty(param.SignNo))
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "参数异常";
//                return;
//            }
//#endregion
//            Expression<Func<View_IPayChannelBankList,bool>> exp = null;
//            if (order.MediaType == 1)
//            {
//                exp = s => s.SignNo == param.SignNo && s.IsSuptPC == 1;
//            }
//            else
//            {
//                exp = s => s.SignNo == param.SignNo && s.IsSuptWap == 1;
//            }
//            var list = ibll.View_IPayChannelBankList.where(exp).ToList();
//            if (list.Count > 0)
//            {
//                var rlist = list.Select(s => new
//                {
//                    s.SignNo,
//                    s.ID,
//                    s.BankName,
//                    s.BankCode,
//                    s.OnceLimit,
//                    s.DayLimit,
//                    s.IsJumpBank
//                });
//                result.code = RespCodeConfig.Normal;
//                result.data = rlist;
//            }
//            else
//            {
//                result.code = RespCodeConfig.ServerError;
//                result.msg = "无支持银行";
//            }
//        }
//        /// <summary>
//        /// 2003-网银支付
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_WebPay(ReturnModel result, RequestParamsM param)
//        {
//            //1.0 验证签名
//            //2.0 验证参数
//            //3.0 记录--订单创建，C请求M日志，M响应C日志
//#region 签名验证+参数验证
//            //验证billToken
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            if (!string.IsNullOrEmpty(order.OrderNo))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "订单已失效";
//                return;
//            }
//            View_PayMerSignWay payMert = ibll.View_PayMerSignWay.Single(a => a.SignNo == param.SignNo);
//            if (payMert==null)
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "数据异常";
//                return;
//            }
//            PayWayCodeType paywayType = PayWayCodeType.DinPay;
//            bool r = Enum.TryParse(payMert.PayWayCode, out paywayType);
//            if (!r)
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "数据异常";
//                return;
//            }

//            //验证是否支持该银行
//            PaySerPBank bankModel = null;
//            if (!string.IsNullOrEmpty(param.BankCode))
//            {
//                bankModel= ibll.PaySerPBank.Single(s =>s.IsAvailable==1& s.BankCode == param.BankCode & s.SerNo == payMert.SerNo & s.PayType == payMert.DataType);
//            }
//            else
//            {
//                List<PaySerPBank> banklist = ibll.PaySerPBank.where(s => s.IsAvailable == 1 & s.IsJumpBank == 2 & s.SerNo == payMert.SerNo & s.PayType == payMert.DataType).ToList();
//                if (banklist.Count>0)
//                {
//                    bankModel = banklist[0];
//                }
//            }

//            if (bankModel == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "不支持该银行";
//                return;
//            }
//#endregion
//            param.BankValue = bankModel.BankValue;
//            order.IP = Common.GetIP();
//            switch (paywayType)
//            {
//                case PayWayCodeType.ZFTWebPay:
//                    ZFTPayService.WebPay(result, param,order, payMert);
//                    break;
//                case PayWayCodeType.KLTWebPay:
//                    KLTPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.QBPWebPay:
//                    QBFuPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.WXFWebPay:
//                    WXFPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.JBFWebPay:
//                    JLFPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.QLBLFWebPay:
//                    QLBLPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.ZHFuWebPay:
//                    ZHFuPayService.WebPay(result, param, order, payMert);
//                    //GetBase64ZHFuWebForm(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.ChinagpayWYPay:
//                    AiNongPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.YunZFWebPay:
//                    YunZFPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.YWFWebPay:
//                    YunWeiPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.QJFWebPay:
//                    QianJinPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.JHFWebPay:
//                    JuHePayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.Synergy:
//                    HuiNengPayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.ShanDeWebPay:
//                    ShanDePayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.HXFuWebPay:
//                    HuanXunFuPayService.WebPay(result, param, order, payMert);
//                    break;
//            }

//#region 创建订单+写响应日志
//            if (result.code == RespCodeConfig.Normal)
//            {
//                //创建订单，写响应日志
//                order.BankCode = bankModel.BankCode;
//                order.MerNo = payMert.MerNo;
//                order.SignNo = param.SignNo;
//                order.PNo = payMert.PNo;
                
//                ibll.PayOrder.Update(order,new string[] { "BankCode", "MerNo", "SignNo", "OrderNo", "IP" , "PNo" });
//                ibll.SaveChanges();
//                ServiceLog log = new ServiceLog();
//                log.BillNo = order.BillNo;
//                log.ReqType = LogTypeConfig.PayMC;
//                log.ReqStr = JsonConvert.SerializeObject(param);
//                log.RespStr = JsonConvert.SerializeObject(result.data);
//                log.AddDate = DateTime.Now;
//                AddServiceLog(log);
//                //推送订单信息异步通知给业务系统
//                PushOrderInfo(order);
//            }
//#endregion
//        }
//        public static void GetBase64ZHFuWebForm(ReturnModel result, RequestParamsM param, PayOrder order, View_PayMerSignWay payMert)
//        {
//            //ReturnModel rm =ZHFuPayService.WebPay(result, param, order, payMert);
//            ////判断是否需要跳转中转地址
//            //if (!string.IsNullOrEmpty(payMert.TransFormURL))
//            //{
//            //    if (rm.code == 0)
//            //    {
//            //        //请求正常
//            //        string transUrl = payMert.TransFormURL;
//            //        string formHTML = string.Format("<form name=\"form1\" id=\"form1\" method=\"post\" action=\"{0}\"><input name = 'payForm' id='payForm' type = 'hidden' value = '{1}' /> </form >", transUrl, DESEncrypt.Encrypt(rm.msg, DESEncrypt.info));
//            //        result.data = new PayContent() { Type = PayContentTypeConfig.FormStr, FormStr = formHTML, Url = "" };
//            //        result.code = RespCodeConfig.Normal;
//            //        result.ok = RespCodeConfig.OK;
//            //    }
//            //}
//        }
//        /// <summary>
//        /// 2004-快捷签约支付接口
//        /// 跳转商户或银联快捷
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_KJSignUp(ReturnModel result, RequestParamsM param)
//        {
//#region 签名验证+参数验证
//            //验证billToken
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            if (!string.IsNullOrEmpty(order.OrderNo))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "订单已失效";
//                return;
//            }
//            View_PayMerSignWay payMert = ibll.View_PayMerSignWay.Single(a => a.SignNo == param.SignNo);
//            PayWayCodeType paywayType = PayWayCodeType.DinPay;
//            bool r = Enum.TryParse(payMert.PayWayCode, out paywayType);
//            if (!r)
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "数据异常";
//                return;
//            }
//#endregion

//            //验证是否支持该银行
//            PaySerPBank bankModel = null;
//            if (!string.IsNullOrEmpty(param.BankCode))
//            {
//                bankModel = ibll.PaySerPBank.Single(s => s.IsAvailable == 1 & s.BankCode == param.BankCode & s.SerNo == payMert.SerNo & s.PayType == payMert.DataType);
//            }
//            else
//            {
//                List<PaySerPBank> banklist = ibll.PaySerPBank.where(s => s.IsAvailable == 1 & s.IsJumpBank == 2 & s.SerNo == payMert.SerNo & s.PayType == payMert.DataType).ToList();
//                if (banklist.Count > 0)
//                {
//                    bankModel = banklist[0];
//                }
//            }
//            if (bankModel == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "不支持该银行";
//                return;
//            }
//            order.IP = Common.GetIP();
//            switch (paywayType)
//            {
//                case PayWayCodeType.ZFTPayKJ:
//                    ZFTPayService.KJPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.KLTPayKJ:
//                    KLTPayService.KJWebSignPay(result,param,order, payMert);
//                    break;
//                case PayWayCodeType.WXFPayKJ:
//                    WXFPayService.KJPay(result, param,order, payMert);
//                    break;
//                case PayWayCodeType.JBFPayKJ:
//                    JLFPayService.KJPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.QLBLFPayKJ:
//                    QLBLPayService.KJPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.YZFuPayKJ:
//                    YZFuPayService.KJPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.YunZFKJPay:
//                    YunZFPayService.KJPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.YWFPayKJ:
//                    YunWeiPayService.KJPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.QJFPayKJ:
//                    QianJinPayService.KJPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.HXFuKJPay:
//                    HuanXunFuPayService.KJPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.ShanDeKJPay:
//                    ShanDePayService.KJPay(result, param, order, payMert);
//                    break;
//                default:
//                    //如果接口走API模式
//                    result.code = RespCodeConfig.Normal;
//                    result.data = new PayContent() { Type = PayContentTypeConfig.APIKJ, Url = "", FormStr = "" };
//                    break;
//            }

//#region 创建订单+写响应日志
//            if (result.code == RespCodeConfig.Normal)
//            {
//                //创建订单，写响应日志
//                order.BankCode = bankModel.BankCode;
//                order.MerNo = payMert.MerNo;
//                order.SignNo = param.SignNo;
//                order.PNo = payMert.PNo;
//                ibll.PayOrder.Update(order, new string[] {"OrderNo", "BankCode", "MerNo", "SignNo" , "IP", "PNo" });
//                ibll.SaveChanges();
//                ServiceLog log = new ServiceLog();
//                log.BillNo = order.BillNo;
//                log.ReqType = LogTypeConfig.PayMC;
//                log.ReqStr= JsonConvert.SerializeObject(param);
//                log.RespStr = JsonConvert.SerializeObject(result.data);
//                log.AddDate = DateTime.Now;
//                AddServiceLog(log);

//                //推送订单信息异步通知给业务系统
//                PushOrderInfo(order);
//            }
//#endregion
//        }
//        /// <summary>
//        /// 2005-快捷API支付，下单接口
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_KJCreateOrder(ReturnModel result, RequestParamsM param)
//        {
//            //1.0 验证签名
//            //2.0 验证参数
//            //3.0 记录--创建订单，C请求M日志，M请求S日志，S响应M日志，M响应C日志
//#region 签名验证+参数验证
//            //验证billToken
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            if (!string.IsNullOrEmpty(order.OrderNo))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "订单已失效";
//                return;
//            }
//            View_PayMerSignWay payMert = ibll.View_PayMerSignWay.Single(a => a.SignNo == order.SignNo);
//            PayWayCodeType paywayType = PayWayCodeType.DinPay;
//            bool r = Enum.TryParse(payMert.PayWayCode, out paywayType);
//            if (!r)
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "数据异常";
//                return;
//            }
//#endregion
//            switch (paywayType)
//            {
//                case PayWayCodeType.KLTPayKJ:
//                    KLTPayService.KJCreateOrder(result, param, order, payMert);
//                    break;
//            }

//#region 创建订单+写响应日志
//            if (result.code == RespCodeConfig.Normal)
//            {
//                //创建订单，写响应日志
//                ibll.PayOrder.Update(order, new string[]{ "BankCode", "KJSignNo" , "KJSmsNo" });
//                ibll.SaveChanges();
//                ServiceLog log = new ServiceLog();
//                log.BillNo = order.BillNo;
//                log.ReqType = LogTypeConfig.PayMC;
//                log.ReqStr= JsonConvert.SerializeObject(param);
//                log.RespStr = JsonConvert.SerializeObject(result.data);
//                log.AddDate = DateTime.Now;
//                AddServiceLog(log);
//            }
//#endregion
//        }
//        /// <summary>
//        /// 2006-快捷API支付确认支付
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_KJConfirmPay(ReturnModel result,RequestParamsM param)
//        {
//#region 签名验证+参数验证
//            //验证billToken
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            if (!string.IsNullOrEmpty(order.OrderNo))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "订单已失效";
//                return;
//            }
//            View_PayMerSignWay payMert = ibll.View_PayMerSignWay.Single(a => a.SignNo == order.SignNo);
//            PayWayCodeType paywayType = PayWayCodeType.DinPay;
//            bool r = Enum.TryParse(payMert.PayWayCode, out paywayType);
//            if (!r)
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "数据异常";
//                return;
//            }
//#endregion
            
//            switch (paywayType)
//            {
//                case PayWayCodeType.KLTPayKJ:
//                    KLTPayService.KJConfirmPay(result, param, order, payMert);
//                    break;
//            }

//#region 创建订单+写响应日志
//            if (result.code == RespCodeConfig.Normal)
//            {
//                ServiceLog log = new ServiceLog();
//                log.BillNo = order.BillNo;
//                log.ReqType = LogTypeConfig.PayMC;
//                log.ReqStr=JsonConvert.SerializeObject(param);
//                log.RespStr = JsonConvert.SerializeObject(result.data);
//                log.AddDate = DateTime.Now;
//                AddServiceLog(log);
//            }
//#endregion
//        }
//        /// <summary>
//        /// 2007-扫码支付下单
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_ScanPay(ReturnModel result, RequestParamsM param)
//        {
//#region 签名验证+参数验证
//            //验证billToken
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            if (!string.IsNullOrEmpty(order.OrderNo))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "订单已失效";
//                return;
//            }
//            View_PayMerSignWay payMert = ibll.View_PayMerSignWay.Single(a => a.SignNo == param.SignNo);
//            PayWayCodeType paywayType = PayWayCodeType.DinPay;
//            bool r = Enum.TryParse(payMert.PayWayCode, out paywayType);
//            if (!r)
//            {
//                result.code = RespCodeConfig.ArgumentExp;
//                result.msg = "数据异常";
//                return;
//            }
//#endregion
//            //创建订单，写响应日志
//            order.MerNo = payMert.MerNo;
//            order.SignNo = param.SignNo;
//            order.PNo = payMert.PNo;
//            order.IP = Common.GetIP();


//            switch (paywayType)
//            {
//                case PayWayCodeType.KLTScanAliPay:
//                    //扫码支付
//                    KLTPayService.ScanPay(result, param, order, payMert,4);
//                    break;
//                case PayWayCodeType.KLTScanWxPay:
//                    KLTPayService.ScanPay(result, param, order, payMert,3);
//                    break;
//                case PayWayCodeType.AlipayWebPay:
//                    AlipayService.WebPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.WechatScanPay:
//                    WechatPayService.ScanPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.HSScanPay:
//                    HuaSPayService.ScanPay(result, param, order, payMert);
//                    break;
//                case PayWayCodeType.AlipayScanPay:
//                    AlipayService.ScanPay(result, param, order, payMert);
//                    break;
//            }

//#region 创建订单+写响应日志
//            if (result.code == RespCodeConfig.Normal)
//            {
//                //生成二维码
//                PayContent d = result.data as PayContent;
//                if (d.Type==PayContentTypeConfig.ScanURL&&!string.IsNullOrEmpty(d.Url))
//                {
//                    string url = d.Url;
//                    string filename = order.BillToken;
//                    QRCodeHandler qr = new QRCodeHandler();
//                    string path = HttpContext.Current.Server.MapPath("/files/wxqrcode/");  //文件目录  
//                    string qrString = url;                                                            //二维码字符串  
//                    string logoFilePath = HttpContext.Current.Server.MapPath(@"/images/") + "qrlogo.png"; //商家Logo文件  
//                    string filePath = path + filename + ".jpg";                                             //二维码文件名  
//                    if (qr.CreateQRCode(qrString, "Byte", 6, 6, "H", filePath, true, logoFilePath))//生成二维码
//                    {
//                        d.Url = @"/files/wxqrcode/" + filename + ".jpg";
//                        result.data = d;
//                    }
//                }
//                ibll.PayOrder.Update(order, new string[] {"MerNo", "SignNo", "OrderNo" , "IP" , "PNo", "TradeNo" });
//                ibll.SaveChanges();
//                ServiceLog log = new ServiceLog();
//                log.BillNo = order.BillNo;
//                log.ReqType = LogTypeConfig.PayMC;
//                log.ReqStr = JsonConvert.SerializeObject(param);
//                log.RespStr = JsonConvert.SerializeObject(result.data);
//                log.AddDate = DateTime.Now;
//                AddServiceLog(log);

//                //推送订单信息异步通知给业务系统
//                PushOrderInfo(order);
//            }
//#endregion
//        }

//        /// <summary>
//        /// 2008-扫码支付状态确认
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_ScanPayStatus(ReturnModel result, RequestParamsM param)
//        {
//#region 签名验证+参数验证
//            //验证billToken
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//#endregion
//            if (order.Status == 1)
//            {
//                result.code = RespCodeConfig.Normal;
//                result.msg = "支付成功";
//                return;
//            }
//            else
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "待支付";
//                return;
//            }
//        }
//        /// <summary>
//        ///  2011-获取用户信息
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_GetUserInfo(ReturnModel result, RequestParamsM param)
//        {
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            //绑卡支付，获取订单信息中的用户信息
//            string userInfo=order.UserInfo;
//            if (!string.IsNullOrEmpty(userInfo))
//            {
//                userInfo = DESEncrypt.Decrypt(userInfo, DESEncrypt.info);
//                List<string> ulist = userInfo.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
//                if (ulist.Count >= 2)
//                {
//                    result.code = RespCodeConfig.Normal;
//                    result.data = new { uName = ulist[0], uIdCard = ulist[1] };
//                }
//                else if (ulist.Count >= 1)
//                {
//                    result.code = RespCodeConfig.Normal;
//                    result.data = new { uName = ulist[0], uIdCard ="" };
//                }
//                else
//                {
//                    result.code = RespCodeConfig.Normal;
//                    result.data = new { uName = "", uIdCard = "" };
//                }
//            }
//            else
//            {
//                result.code = RespCodeConfig.Normal;
//                result.data = new { uName="",uIdCard=""};
//            }
//        }
//        /// <summary>
//        /// 2012-根据银行卡号获取银行卡相关信息
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_GetUserBindBankInfo(ReturnModel result, RequestParamsM param)
//        {
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            if (string.IsNullOrEmpty(param.BankCard))
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "请输入银行卡号";
//                return;
//            }
//            List<string> bankIdentify = GetBankCodeIdentity(param.BankCard);

//            List<BankBase> banks = ibll.BankBase.where(a => bankIdentify.Contains(a.CardBins)).OrderByDescending(s=>s.Lengths).ToList();

//            if (null != banks && banks.Count > 0)
//            {
//                BankBase bank = banks[0];
//                List<View_IPayWayBank> banklist = ibll.View_IPayWayBank.where(a => a.PNo == order.PNo).ToList();
//                var model = banklist.FirstOrDefault(a => a.BankCode.ToLower() == bank.BankCode.ToLower());
//                if (model != null)
//                {
//                    result.data = new { BankName = bank.BankName, CardsID = bank.CardType, OnceLimit = model.OnceLimit, DayLimit = model.DayLimit };
//                    result.code = RespCodeConfig.Normal;
//                }
//                else
//                {
//                    result.code = RespCodeConfig.Faild;
//                    result.msg = "该通道不支持您的银行卡";
//                    return;
//                }
//            }
//            else
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "不支持该银行卡";
//                return;
//            }
//        }
//        /// <summary>
//        /// 2013-用户已绑定的银行卡列表
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_GetUserBindBankList(ReturnModel result, RequestParamsM param)
//        {
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            //绑卡支付，获取订单信息中的用户信息
//            string userInfo = order.UserInfo;
//            if (!string.IsNullOrEmpty(userInfo))
//            {
//                userInfo = DESEncrypt.Decrypt(userInfo, DESEncrypt.info);
//                List<string> ulist = userInfo.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
//                string uIDCard = ulist[1];
//                List<UserBindCards> bindcards= ibll.UserBindCards.where(a => a.IDCardNo == uIDCard&&a.IsAvailable==1&&a.IsDel==0).ToList();
//                result.data = bindcards.Select(s => new {
//                    s.ID,
//                    s.IsSafe,
//                    s.BankType,
//                    BankCardNo=s.BankCardNo.Substring(s.BankCardNo.Length-5,4)
//                }).ToList();
//                result.code = RespCodeConfig.Normal;
//            }
//            else
//            {
//                result.code = RespCodeConfig.Normal;
//                result.data = null;
//            }

//        }
//        /// <summary>
//        /// 2014-保存用户绑定卡信息
//        /// </summary>
//        /// <param name="result"></param>
//        /// <param name="param"></param>
//        public static void PayCenter_GetUserBindSaveBank(ReturnModel result, RequestParamsM param)
//        {
//            PayOrder order = ibll.PayOrder.Single(s => s.BillToken == param.BillToken);
//            if (order == null)
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
//            //绑卡支付，获取订单信息中的用户信息
//            string userInfo = order.UserInfo;
//            if (!string.IsNullOrEmpty(userInfo))
//            {
//                userInfo = DESEncrypt.Decrypt(userInfo, DESEncrypt.info);
//                List<string> ulist = userInfo.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
//                UserBindCards model = new UserBindCards();
//                model.Account = order.Account;
//                model.Mobile = param.Mobile;
//                model.Name = ulist[0];
//                model.IDCardNo = ulist[1];
//                model.BankCardNo = param.BankCard;
//                model.BankType = param.BankCode;
//                model.IsAvailable = 1;
//                model.IsDefault = 0;
//                model.IsDel = 0;
//                model.IsSafe = 0;
//                model.VaildState = 0;
//                model.VaildType = "未验证";
//                ibll.UserBindCards.Add(model);
//                int n=ibll.SaveChanges();
//                if (n>0)
//                {
//                    UserBindCards bind = ibll.UserBindCards.Single(a => a.BankCardNo == param.BankCard && a.Mobile == param.Mobile);
//                    result.code = RespCodeConfig.Normal;
//                    result.data =new {bindID= bind .ID};
//                    return;
//                }
//            }
//            else
//            {
//                result.code = RespCodeConfig.Faild;
//                result.msg = "数据异常";
//                return;
//            }
            
//        }

//        /// <summary>
//        /// 中途推送订单信息给业务系统
//        /// </summary>
//        /// <param name="order"></param>
//        public static void PushOrderInfo(PayOrder order)
//        {
//            View_PayMert paywayMer = ibll.View_PayMert.Single(s => s.MerNo == order.MerNo);
//            PayCustomer cust = ibll.PayCustomer.Single(s => s.CustmCode == order.CustomerNo);

//            NotifyModel model = new NotifyModel();
//            model.BillNo = order.BillNo;
//            model.Amount = order.Amount.ToString();//单位 分
//            model.CustomerNo = order.CustomerNo;
//            model.Status = "0";
//            model.Sign = "";
//            model.Source = paywayMer.CompShotName;
//            model.Channel = paywayMer.MerName;
//            model.Charge_val = "0";//手续费费率
//            model.Service_charge = "0";//手续费
//            model.Charge_type = "2";//手续费计算方式
//            model.Calculation_type = "1";//手续费计算规则
//            model.OrderNo = order.OrderNo;
//            string signStr = string.Format("CustomerNo={0}&BillNo={1}&Amount={2}&Status={3}{4}",
//    model.CustomerNo, model.BillNo, model.Amount, model.Status, DESEncrypt.Decrypt(cust.MEncryKey, DESEncrypt.info));
//            string sign = SignUtils.MD5(signStr, System.Text.Encoding.UTF8);
//            model.Sign = sign;
//            string p = JsonConvert.SerializeObject(model);
//            ServiceLog log = new ServiceLog();
//            log.BillNo = order.BillNo;
//            log.ReqType = LogTypeConfig.NotifyMC;
//            log.ReqStr = JsonConvert.SerializeObject(model);
//            try
//            {
//                string t = HttpUtils.HttpPost_JSON(cust.NotifyUrl, p);
//                log.RespStr = JsonConvert.SerializeObject(t);
//                bool resp = true;
//                if (t.ToUpper().Equals("SUCCESS"))
//                {
//                    resp = true;
//                }
//                else
//                {
//                    //log -通知失败
//                    resp = false;
//                }

//            }
//            catch (Exception ex)
//            {
//                log.RespStr = ex.Message;
//            }
//            log.AddDate = DateTime.Now;
//            ibll.ServiceLog.Add(log);
//            ibll.SaveChanges();
//        }
//        /// <summary>
//        /// 信用卡Luhn算法具体的校验过程如下：
//        ///1、从卡号最后一位数字开始，逆向将奇数位(1、3、5等等)相加。
//        ///2、从卡号最后一位数字开始，逆向将偶数位数字，先乘以2（如果乘积为两位数，则将其减去9），再求和。
//        ///3、将奇数位总和加上偶数位总和，结果应该可以被10整除。
//        /// </summary>
//        /// <param name="cardNo">银行卡号</param>
//        /// <returns></returns>
//        private bool Huhn(string cardNo)
//        {
//            try
//            {
//                int odd = 0, even = 0;
//                for (int i = cardNo.Length; i > 0; i--)
//                {
//                    int n = Convert.ToInt32(cardNo[i - 1].ToString());
//                    if ((cardNo.Length - i + 1) % 2 == 1)//倒序奇数位
//                    {
//                        odd += n;
//                    }
//                    else
//                    {
//                        even += n * 2 >= 10 ? n * 2 - 9 : n * 2;
//                    }
//                }
//                int result = odd + even;
//                return (result % 10) == 0;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }

//        }
//        /// <summary>
//        /// 根据银行卡号前几位标识
//        /// </summary>
//        /// <param name="bankNo"></param>
//        /// <returns></returns>
//        private static List<string> GetBankCodeIdentity(string bankNo)
//        {
//            List<string> bankIdentify = new List<string>();
//            bankIdentify.Add(bankNo.Substring(0, 3));
//            bankIdentify.Add(bankNo.Substring(0, 4));
//            bankIdentify.Add(bankNo.Substring(0, 5));
//            bankIdentify.Add(bankNo.Substring(0, 6));
//            bankIdentify.Add(bankNo.Substring(0, 7));
//            bankIdentify.Add(bankNo.Substring(0, 8));
//            bankIdentify.Add(bankNo.Substring(0, 9));
//            bankIdentify.Add(bankNo.Substring(0, 10));
//            return bankIdentify;
//        }
//        #endregion



//        #region 订单+日志处理
//        /// <summary>
//        /// 创建订单
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        public static bool CreateOrder(PayOrder model)
//        {
//            ibll.PayOrder.Add(model);
//            return ibll.SaveChanges() > 0;
//        }
//        /// <summary>
//        /// 新增交互日志
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        public static bool AddServiceLog(ServiceLog model)
//        {
//            ibll.ServiceLog.Add(model);
//            return ibll.SaveChanges()>0;
//        }
//#endregion
    }
}
