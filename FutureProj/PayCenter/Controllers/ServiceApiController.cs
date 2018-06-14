using Serv;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PayCenter.Controllers
{
    public class ServiceApiController : ApiController
    {
        public string Index()
        {
            return "welcome to API";
        }
        /// <summary>
        /// 支付相关接口
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet]
        public ReturnModel Pay(RequestParamsM parm)
        {
            ReturnModel result = new ReturnModel() { code = RespCodeConfig.Normal };
            long startT = DateTime.Now.Ticks;
            try
            {
                switch (parm.No)
                {
                    //case ApiConfig.SERVICE_GetChannelList:
                    //    PayControl.GetChannelList(result, parm);
                    //    break;
                    //case ApiConfig.SERVICE_GetChannelBankList:
                    //    PayControl.GetChannelBankList(result, parm);
                    //    break;
                    //case ApiConfig.SERVICE_WebPay:
                    //    PayControl.WebPay(result, parm);
                    //    break;
                    //case ApiConfig.SERVICE_KJSignUp:
                    //    PayControl.KJSignUp(result, parm);
                    //    break;
                    //case ApiConfig.SERVICE_KJCreateOrder:
                    //    PayControl.KJCreateOrder(result, parm);
                    //    break;
                    //case ApiConfig.SERVICE_KJConfirmPay:
                    //    PayControl.KJConfirmPay(result, parm);
                    //    break;

                }
            }
            catch (NotImplementedException notImp)
            {
                result.code = RespCodeConfig.ServerError;
                result.msg = "支付通道未开通";
            }
            catch (Exception ex)
            {
                result.code = RespCodeConfig.ServerError;
                result.msg = ex.Message;
            }
            long endT = DateTime.Now.Ticks;
            result.Timespan = TimeSpan.FromTicks(endT - startT).TotalMilliseconds.ToString();

            return result;
        }
    }
}
