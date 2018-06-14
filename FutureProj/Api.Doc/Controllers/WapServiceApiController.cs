using EF.Common;
using Serv;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Doc.Controllers
{
    [RoutePrefix("v1")]
    public class WapServiceApiController : ApiController
    {
        private string apiUrl = System.Configuration.ConfigurationManager.AppSettings["paycenterApiUrl"];// "http://192.168.1.60:8089";

        #region WAP注资接口
        /// <summary>
        /// 接口号：2021
        /// 获取支付通道列表
        /// </summary>
        /// <param name="billtoken"></param>
        /// <returns></returns>
        [HttpGet, Route("2021")]
        public IHttpActionResult PayCenterM_GetChannelList(string billtoken)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                No = ApiConfig.PayCenterM_GetChannelList,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2022
        /// 获取支付通道银行列表
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="signno">支付通道编号</param>
        /// <returns></returns>
        [HttpGet, Route("2022")]
        public IHttpActionResult PayCenterM_GetChannelBankList(string billtoken, string signno)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                No = ApiConfig.PayCenterM_GetChannelBankList,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2023
        /// 网银支付
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="signno">支付通道编号</param>
        /// <param name="bankcode">银行bankcode</param>
        /// <returns></returns>
        [HttpGet, Route("2023")]
        public IHttpActionResult PayCenterM_WebPay(string billtoken, string signno, string bankcode)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                BankCode = bankcode,
                No = ApiConfig.PayCenterM_WebPay,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2024
        /// 跳转快捷
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="signno">支付通道编号</param>
        /// <param name="bankcode">银行bankcode</param>
        /// <returns></returns>
        [HttpGet, Route("2024")]
        public IHttpActionResult PayCenterM_KJSignUp(string billtoken, string signno, string bankcode)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                BankCode = bankcode,
                No = ApiConfig.PayCenterM_KJSignUp,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2025
        /// API快捷，发短信
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="signno">支付通道编号</param>
        /// <param name="bankcode">银行bankcode</param>
        /// <returns></returns>
        [HttpGet, Route("2025")]
        public IHttpActionResult PayCenterM_KJCreateOrder(string billtoken, string signno, string bankcode)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                BankCode = bankcode,
                No = ApiConfig.PayCenterM_KJCreateOrder,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2026
        /// API快捷，确认支付
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="smscode">短信验证码</param>
        /// <returns></returns>
        [HttpGet, Route("2026")]
        public IHttpActionResult PayCenterM_KJConfirmPay(string billtoken, string smscode)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SmsCode = smscode,
                No = ApiConfig.PayCenterM_KJConfirmPay,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2027
        /// 扫码支付接口
        /// </summary>
        /// <param name="billtoken"></param>
        /// <param name="signno"></param>
        /// <returns></returns>
        [HttpGet, Route("2027")]
        public IHttpActionResult PayCenterM_ScanPay(string billtoken, string signno)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                No = ApiConfig.PayCenterM_ScanPay,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2028
        /// 扫码支付接口
        /// </summary>
        /// <param name="billtoken"></param>
        /// <returns></returns>
        [HttpGet, Route("2028")]
        public IHttpActionResult PayCenterM_ScanPayStatus(string billtoken)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                No = ApiConfig.PayCenterM_ScanPayStatus,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        #endregion
    }
}