﻿using EF.Common;
using Newtonsoft.Json;
using Serv;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class ServiceApiController : ApiController
    {
        public string Index()
        {
            return "welcome to API";
        }

        #region Future-前端接口
        /// <summary>
        /// 前端接口
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public ReturnModel FutureCenter(RequestParamsM parm)
        {
            ReturnModel result = new ReturnModel() { code = RespCodeConfig.Normal };
            if (null== parm)
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "参数错误";
                return result;
            }
            long startT = DateTime.Now.Ticks;
            LogHelper.Info<ServiceApiController>("请求：" + JsonConvert.SerializeObject(parm));
            try
            {
                switch (parm.No)
                {
                    case ApiConfig.SERVICE_SignIn:
                        FutureControl.SERVICE_SignIn(result, parm);
                        break;
                    case ApiConfig.SERVICE_GetNewsList:
                        FutureControl.SERVICE_GetNewsList(result, parm);
                        break;
                    case ApiConfig.SERVICE_GetNewsInfo:
                        FutureControl.SERVICE_GetNewsInfo(result,parm);
                        break;
                    case ApiConfig.SERVICE_GetReposList:
                        FutureControl.SERVICE_GetReposList(result, parm);
                        break;
                    case ApiConfig.SERVICE_GetMaterialList:
                        FutureControl.SERVICE_GetMaterialList(result,parm);
                        break;
                    case ApiConfig.SERVICE_GetMaterList_TwoCate:
                        FutureControl.SERVICE_GetMaterList_TwoCate(result, parm);
                        break;
                    case ApiConfig.SERVICE_GetMomentList:
                        FutureControl.SERVICE_GetMomentList(result, parm);
                        break;
                    case ApiConfig.SERVICE_GetMomentDetail:
                        FutureControl.SERVICE_GetMomentDetail(result, parm);
                        break;
                    case ApiConfig.SERVICE_GetAIList:
                        FutureControl.SERVICE_GetAIList(result, parm);
                        break;

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
                result.msg = "";
                LogHelper.Error<ServiceApiController>(ex);
            }
            long endT = DateTime.Now.Ticks;
            result.Timespan = TimeSpan.FromTicks(endT - startT).TotalMilliseconds.ToString();
            if (string.IsNullOrEmpty(result.msg))
            {
                result.msg = "";
            }
            LogHelper.Info<ServiceApiController>("返回：" + JsonConvert.SerializeObject(result));
            return result;
        }

        #endregion

        #region Pay-支付接口
        /// <summary>
        /// PC收银台方式接口
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public ReturnModel PayCenter(RequestParamsM parm)
        {
            ReturnModel result = new ReturnModel() { code = RespCodeConfig.Normal };
            long startT = DateTime.Now.Ticks;
            LogHelper.Info<ServiceApiController>("请求：" + JsonConvert.SerializeObject(parm));
            try
            {
                switch (parm.No)
                {
                    //case ApiConfig.PayCenter_GetOrderInfo:
                    //    PayControl.PayCenter_GetOrderInfo(result, parm);
                    //    break;
                    //case ApiConfig.PayCenter_OrderInit:
                    //    PayControl.PayCenter_OrderInit(result, parm);
                    //    break;
                    //case ApiConfig.PayCenter_GetChannelList:
                    //    PayControl.PayCenter_GetChannelList(result, parm);
                    //    break;
                    //case ApiConfig.PayCenter_GetChannelBankList:
                    //    PayControl.PayCenter_GetChannelBankList(result, parm);
                    //    break;
                    //case ApiConfig.PayCenter_WebPay:
                    //    PayControl.PayCenter_WebPay(result, parm);
                    //    break;
                    //case ApiConfig.PayCenter_KJSignUp:
                    //    PayControl.PayCenter_KJSignUp(result, parm);
                    //    break;
                    //case ApiConfig.PayCenter_KJCreateOrder:
                    //    PayControl.PayCenter_KJCreateOrder(result, parm);
                    //    break;
                    //case ApiConfig.PayCenter_KJConfirmPay:
                    //    PayControl.PayCenter_KJConfirmPay(result, parm);
                    //    break;
                    //case ApiConfig.PayCenter_ScanPay:
                    //    PayControl.PayCenter_ScanPay(result, parm);
                    //    break;
                    //case ApiConfig.PayCenter_ScanPayStatus:
                    //    PayControl.PayCenter_ScanPayStatus(result, parm);
                    //    break;
                    //case ApiConfig.PayCenterM_GetChannelList:
                    //    PayControl.PayCenter_GetChannelList(result, parm);
                    //    break;
                    //case ApiConfig.PayCenterM_GetChannelBankList:
                    //    PayControl.PayCenter_GetChannelBankList(result, parm);
                    //    break;
                    //case ApiConfig.PayCenterM_WebPay:
                    //    PayControl.PayCenter_WebPay(result, parm);
                    //    break;
                    //case ApiConfig.PayCenterM_KJSignUp:
                    //    PayControl.PayCenter_KJSignUp(result, parm);
                    //    break;
                    //case ApiConfig.PayCenterM_KJCreateOrder:
                    //    PayControl.PayCenter_KJCreateOrder(result, parm);
                    //    break;
                    //case ApiConfig.PayCenterM_KJConfirmPay:
                    //    PayControl.PayCenter_KJConfirmPay(result, parm);
                    //    break;
                    //case ApiConfig.PayCenterM_ScanPay:
                    //    PayControl.PayCenter_ScanPay(result, parm);
                    //    break;
                    //case ApiConfig.PayCenterM_ScanPayStatus:
                    //    PayControl.PayCenter_ScanPayStatus(result, parm);
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
                result.msg = "通道异常，请联系客服";
                LogHelper.Error<ServiceApiController>(ex);
            }
            long endT = DateTime.Now.Ticks;
            result.Timespan = TimeSpan.FromTicks(endT - startT).TotalMilliseconds.ToString();
            if (string.IsNullOrEmpty(result.msg))
            {
                result.msg = "";
            }
            LogHelper.Info<ServiceApiController>("返回：" + JsonConvert.SerializeObject(result));
            return result;
        }

        #endregion

        #region Future-后台接口
        /// <summary>
        /// 后台接口
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public ReturnModel FutureAdmin(AdminParamsM parm)
        {
            ReturnModel result = new ReturnModel() { code = RespCodeConfig.Normal };
            if (null == parm)
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "参数错误";
                return result;
            }
            long startT = DateTime.Now.Ticks;
            LogHelper.Info<ServiceApiController>("请求：" + JsonConvert.SerializeObject(parm));
            try
            {
                switch (parm.No)
                {
                    case ApiConfig.SERVICE_SignIn:
                        AdminControl.Admin_SignIn(result, parm);
                        break;

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
                result.msg = "";
                LogHelper.Error<ServiceApiController>(ex);
            }
            long endT = DateTime.Now.Ticks;
            result.Timespan = TimeSpan.FromTicks(endT - startT).TotalMilliseconds.ToString();
            if (string.IsNullOrEmpty(result.msg))
            {
                result.msg = "";
            }
            LogHelper.Info<ServiceApiController>("返回：" + JsonConvert.SerializeObject(result));
            return result;
        }

        #endregion

    }
}
