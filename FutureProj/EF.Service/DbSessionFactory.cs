using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EF.Service
{
    public static class DbSessionFactory
    {
        public static IRespository.IRespositorySession GetDbSession()
        { 
            IRespository.IRespositorySession dbSession=CallContext.GetData("IRespository.IRespositorySession") as IRespository.IRespositorySession;
            if(dbSession==null)
            {
                dbSession= Common.DI.ObjectFactory.GetObj<IRespository.IRespositorySession>("dalSession");
                CallContext.SetData("IRespository.IRespositorySession",dbSession);
            }
            return dbSession;
        }
    }
}
