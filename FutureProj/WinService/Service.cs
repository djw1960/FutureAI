using EF.Common;
using WinService.Helpers;
using WinService.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinService
{
    partial class Service : ServiceBase
    {
        public Service() {
            InitializeComponent();
        }
        StdSchedulerFactory factory = null;

        /// <summary>
        /// 开始执行事件
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("windows服务启动");
            LogHelper.Info<Service>("windows服务启动");

            // 从调度工厂获取调度对象
            NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
            factory = new StdSchedulerFactory(props);

            #region 任务列表

            //ScheduleJob_Repeat<FirstJob>("FirstJob", GetCronExpression(20, 0)).GetAwaiter().GetResult();

            //IScheduleBuilder schedule = CronScheduleBuilder.DailyAtHourAndMinute(18, 25);
            //ScheduleJob_Once<PushOrderNotifyJob>("PushOrderNotifyJob", schedule).GetAwaiter().GetResult();

            if (ConfigurationHelper.PushOrderNotifyJob_Switch)
            {
                int s = Convert.ToInt32(ConfigurationHelper.PushOrderNotifyJob_Time);
                ScheduleJob_Repeat<PushOrderNotifyJob>("PushOrderNotifyJob", GetCronExpression(s, 0)).GetAwaiter().GetResult();
            }
            if (ConfigurationHelper.QueryOrderStatusJob_Switch)
            {
                int s = Convert.ToInt32(ConfigurationHelper.QueryOrderStatusJob_Time);
                ScheduleJob_Repeat<QueryOrderStatusJob>("QueryOrderStatusJob", GetCronExpression(s, 0)).GetAwaiter().GetResult();
            }


            #endregion

        }
        #region 调度任务
        /// <summary>
        /// 1.0 调度任务分两种，1-重复执行任务，间隔一段时间  2-每天的某一时刻执行一次
        /// 1-重复不停循环执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="cronExpression"></param>
        /// <returns></returns>
        private async Task ScheduleJob_Repeat<T>(string name, string cronExpression) where T : IJob
        {
            try
            {
                //1.0 从调度工厂中获取一个调度对象
                IScheduler scheduler = await factory.GetScheduler();
                // 开启调度
                await scheduler.Start();
                //定义一个任务
                IJobDetail job = JobBuilder.Create<T>()
                    .WithIdentity(name + "job", name + "group")
                    .Build();

                //定义一个任务触发器，每10s执行一次 Trigger the job to run now, and then repeat every 10 seconds 
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(name + "trigger", name + "group")
                    .StartNow()
                    .WithCronSchedule(cronExpression)
                    .Build();

                // 把任务和任务触发器一起给调度创建任务
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        /// <summary>
        /// //1.0 调度任务分两种，1-重复执行任务，间隔一段时间  2-每天的某一时刻执行一次
        /// 2-每日执行一次
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="schebuilder"></param>
        /// <returns></returns>
        private async Task ScheduleJob_Once<T>(string name, IScheduleBuilder schebuilder) where T : IJob
        {
            try
            {
                //1.0 从调度工厂中获取一个调度对象
                IScheduler scheduler = await factory.GetScheduler();
                // 开启调度
                await scheduler.Start();
                //定义一个任务
                IJobDetail job = JobBuilder.Create<T>()
                    .WithIdentity(name + "job", name + "group")
                    .Build();

                //定义一个任务触发器，每10s执行一次 Trigger the job to run now, and then repeat every 10 seconds 
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(name + "trigger", name + "group")
                    .StartNow()
                    .WithSchedule(schebuilder)
                    .Build();

                // 把任务和任务触发器一起给调度创建任务
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        /// <summary>
        /// 得到CronExpression表达式
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <param name="hour">小时</param>
        /// <returns></returns>
        private string GetCronExpression(int seconds, int hour = 0)
        {
            if (seconds > 0)
            {
                if (0 < seconds && seconds < 60) return string.Format("0/{0} * * * * ?", seconds);
                if (60 <= seconds && seconds < 3600) return string.Format("0 0/{0} * * * ?", seconds / 60);
                else return "0 * * * * ?";
            }
            if (hour > 0)
            {
                return string.Format("0 0 0/{0} * * ?", hour);
            }
            return "0 * * * * ?";
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStop()
        {
            EventLog.WriteEntry("windows服务停止");
            LogHelper.Info<Service>("windows服务停止");
        }
    }
}
