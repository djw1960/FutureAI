using EF.Common;
using Serv;
using Serv.Entitys;
using System.Web.Http;

namespace Api.Doc.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("v1")]
    public class PCServiceApiController : ApiController
    {
        private string apiUrl = System.Configuration.ConfigurationManager.AppSettings["paycenterApiUrl"];// "http://192.168.1.60:8089";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("A301")]
        public IHttpActionResult Test()
        {
            return Json<dynamic>(new
            {
                Code = 0,
                Result = ""
            });

        }
        /// <summary>
        /// 接口号：2010
        /// 根据token获取订单详情信息
        /// </summary>
        /// <param name="billtoken"></param>
        /// <param name="signno"></param>
        /// <returns></returns>
        [HttpGet, Route("2010")]
        public IHttpActionResult PayCenter_GetOrderInfo(string billtoken,string signno)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                No = ApiConfig.PayCenter_GetOrderInfo,
                SignNo= signno
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2000
        /// 业务系统注资下单
        /// </summary>
        /// <param name="account">用户账号：string</param>
        /// <param name="accounttype">账户分类：优质，正常，首次</param>
        /// <param name="billno">当前交易单号</param>
        /// <param name="amount">注资金额</param>
        /// <param name="cno">业务系统客户号</param>
        /// <param name="sign">签名</param>
        /// <param name="mediatype">支付平台：1-pc,2-wap，3-wx，4-alipay </param>
        /// <param name="subject">商品标题 </param>
        /// <param name="subjectdesrc">商品描述 </param>
        /// <returns></returns>
        [HttpGet, Route("2000")]
        public IHttpActionResult PayCenter_Init(string account,int accounttype, string billno,string amount, string cno,string sign,int mediatype,string subject,string subjectdesrc)
        {
            var model = new RequestParamsM() {
                Account=account,
                AccountType= accounttype,
                BillNo =billno,
                Amount=amount,
                CustomerNo=cno,
                Sign=sign,
                MediaType=mediatype,
                No = ApiConfig.PayCenter_OrderInit,
                Subject=subject,
                SubjectDesrc= subjectdesrc
            };

            string merkey = "5CBD1785551853BD074D5377288E009D";
            //拼接签名字符串 No={0}&CustomerNo={1}&Account={2}&BillNo={3}&Amount={4}{5}
            string signStr = string.Format("No={0}&CustomerNo={1}&Account={2}&BillNo={3}&Amount={4}{5}",
                model.No, model.CustomerNo, model.Account, model.BillNo, model.Amount, DESEncrypt.Decrypt(merkey, DESEncrypt.info)
                );
            model.Sign =SignUtils.MD5(signStr,System.Text.Encoding.UTF8);
            string urlStr = apiUrl + "/api/paycenter";
            string postdata =Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result=HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }

        #region PC注资接口
        /// <summary>
        /// 接口号：2001
        /// 获取支付通道列表
        /// </summary>
        /// <param name="billtoken"></param>
        /// <returns></returns>
        [HttpGet, Route("2001")]
        public IHttpActionResult PayCenter_GetChannelList(string billtoken)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                No = ApiConfig.PayCenter_GetChannelList,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2002
        /// 获取支付通道银行列表
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="signno">支付通道编号</param>
        /// <returns></returns>
        [HttpGet, Route("2002")]
        public IHttpActionResult PayCenter_GetChannelBankList(string billtoken, string signno)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                No = ApiConfig.PayCenter_GetChannelBankList,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2003
        /// 网银支付
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="signno">支付通道编号</param>
        /// <param name="bankcode">银行bankcode</param>
        /// <returns></returns>
        [HttpGet, Route("2003")]
        public IHttpActionResult PayCenter_WebPay(string billtoken, string signno, string bankcode)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                BankCode = bankcode,
                No = ApiConfig.PayCenter_WebPay,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2004
        /// 跳转快捷
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="signno">支付通道编号</param>
        /// <param name="bankcode">银行bankcode</param>
        /// <returns></returns>
        [HttpGet, Route("2004")]
        public IHttpActionResult PayCenter_KJSignUp(string billtoken, string signno, string bankcode)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                BankCode = bankcode,
                No = ApiConfig.PayCenter_KJSignUp,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2005
        /// API快捷，发短信
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="signno">支付通道编号</param>
        /// <param name="bankcode">银行bankcode</param>
        /// <returns></returns>
        [HttpGet, Route("2005")]
        public IHttpActionResult PayCenter_KJCreateOrder(string billtoken, string signno, string bankcode)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                BankCode = bankcode,
                No = ApiConfig.PayCenter_KJCreateOrder,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2006
        /// API快捷，确认支付
        /// </summary>
        /// <param name="billtoken">订单token</param>
        /// <param name="smscode">短信验证码</param>
        /// <returns></returns>
        [HttpGet, Route("2006")]
        public IHttpActionResult PayCenter_KJConfirmPay(string billtoken, string smscode)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SmsCode = smscode,
                No = ApiConfig.PayCenter_KJConfirmPay,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2007
        /// 扫码支付接口
        /// </summary>
        /// <param name="billtoken"></param>
        /// <param name="signno"></param>
        /// <returns></returns>
        [HttpGet, Route("2007")]
        public IHttpActionResult PayCenter_ScanPay(string billtoken, string signno)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                SignNo = signno,
                No = ApiConfig.PayCenter_ScanPay,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        /// <summary>
        /// 接口号：2008
        /// 扫码支付接口状态查询
        /// </summary>
        /// <param name="billtoken"></param>
        /// <returns></returns>
        [HttpGet, Route("2008")]
        public IHttpActionResult PayCenter_ScanPayStatus(string billtoken)
        {
            var model = new RequestParamsM()
            {
                BillToken = billtoken,
                No = ApiConfig.PayCenter_ScanPayStatus,
            };
            string urlStr = apiUrl + "/api/paycenter";
            string postdata = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string result = HttpUtils.HttpPost_JSON(urlStr, postdata);
            return Json<dynamic>(Newtonsoft.Json.JsonConvert.DeserializeObject(result));
        }
        #endregion
    }
}
    