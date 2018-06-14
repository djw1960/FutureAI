using EF.Common;
using WinService.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinService
{
    class Program
    {
        private static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };
            ServiceBase.Run(ServicesToRun);
        }


        //private static void Main(string[] args)
        //{
        //    Console.WriteLine("this is a demo");
        //    ServiceTest serv = new ServiceTest();
        //    #region 任务列表
        //    serv.ScheduleJob_Repeat<FirstJob>("FirstJob", serv.GetCronExpression(5, 0)).GetAwaiter().GetResult();

        //    IScheduleBuilder schedule = CronScheduleBuilder.DailyAtHourAndMinute(12, 07);
        //    serv.ScheduleJob_Once<PushOrderNotifyJob>("PushOrderNotifyJob", schedule).GetAwaiter().GetResult();
        //    #endregion

        //    Console.Read();

        //}

    }
}
