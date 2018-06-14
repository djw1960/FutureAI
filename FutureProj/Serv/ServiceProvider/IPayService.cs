using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.ServiceProvider
{
    interface IPayService
    {
        /// <summary>
        /// 网银支付
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnModel WebPay(RequestParamsM model);
        /// <summary>
        /// 快捷跳转银联或者跳转商户支付
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnModel KJSignPay(RequestParamsM model);
        /// <summary>
        /// 快捷短信接口支付
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnModel KJSmsCreateOrder(RequestParamsM model);
        /// <summary>
        /// 快捷确认支付
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnModel KJConfirmPay(RequestParamsM model);
        /// <summary>
        /// 扫码支付
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnModel ScanPay(RequestParamsM model);

    }
}
