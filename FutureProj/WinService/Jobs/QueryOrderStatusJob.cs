using EF.Common;
using Serv;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinService.Jobs
{
    public class QueryOrderStatusJob:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            LogHelper.Info<QueryOrderStatusJob>("订单状态查询服务启动");
            try
            {
                Task tk = Task.Factory.StartNew(() => {
                    WindowControl.QueryOrderStatusFromService();
                });
                return tk;
            }
            catch (Exception ex)
            {
                LogHelper.Error<PushOrderNotifyJob>(ex);
                return null;
            }
        }
    }
}
