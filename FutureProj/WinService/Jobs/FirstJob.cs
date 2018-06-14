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
    public class FirstJob : IJob
    {
        
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Task tk = Task.Factory.StartNew(() => {
                    LogHelper.Info<FirstJob>("this is a FirstJob");
                });
                return tk;
            }
            catch (Exception ex)
            {
                LogHelper.Error<FirstJob>(ex);
                return null;
            }
            
        }
    }
}
