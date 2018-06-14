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
    public class PushOrderNotifyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                LogHelper.Info<PushOrderNotifyJob>("异步结果推送服务启动");
                Task tk = Task.Factory.StartNew(() => {
                    WindowControl.PushOrderStatusToCustomer();
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
