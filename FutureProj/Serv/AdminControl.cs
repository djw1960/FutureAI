using Serv;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv
{
    public class AdminControl
    {
        private static EF.IService.IServiceSession ibll = OperationContext.BLLSession;

        /// <summary>
        /// 后台登录操作
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void Admin_SignIn(ReturnModel result,AdminParamsM param)
        {

        }
    }
}
