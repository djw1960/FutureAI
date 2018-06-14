using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv
{
    public class OperationContext
    {
        static EF.IService.IServiceSession bllSession = null;
        public static EF.IService.IServiceSession BLLSession
        {
            get
            {
                if (null == bllSession)
                {
                    return EF.Common.DI.ObjectFactory.GetObj<EF.IService.IServiceSession>("bllSession");
                }
                return bllSession;
            }
        }
    }
}
