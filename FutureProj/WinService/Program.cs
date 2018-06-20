using System.ServiceProcess;

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
