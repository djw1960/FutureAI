using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinService.Helpers
{
   static class ConfigurationHelper
    {
        /// <summary>
        /// 异步结果通知
        /// 开关
        /// </summary>
        public static bool PushOrderNotifyJob_Switch
        {
            get
            {
                bool IsOpen = false;
                ConfigurationManager.RefreshSection("appSettings");
                string v = ConfigurationManager.AppSettings["OpenPushOrderNotifyJob"];
                List<string> list = v.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                bool.TryParse(list[0], out IsOpen);
                return IsOpen;
            }
        }
        /// <summary>
        /// 异步结果通知
        /// 开启频率
        /// </summary>
        public static string PushOrderNotifyJob_Time
        {
            get
            {
                ConfigurationManager.RefreshSection("appSettings");
                string v = ConfigurationManager.AppSettings["OpenPushOrderNotifyJob"];
                List<string> list = v.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                return list[1];
            }
        }
        /// <summary>
        /// 主动查询
        /// 开关
        /// </summary>
        public static bool QueryOrderStatusJob_Switch
        {
            get
            {
                bool IsOpen = false;
                ConfigurationManager.RefreshSection("appSettings");
                string v = ConfigurationManager.AppSettings["OpenQueryOrderStatusJob"];
                List<string> list = v.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                bool.TryParse(list[0], out IsOpen);
                return IsOpen;
            }
        }
        /// <summary>
        /// 主动查询
        /// 开启频率
        /// </summary>
        public static string QueryOrderStatusJob_Time
        {
            get
            {
                ConfigurationManager.RefreshSection("appSettings");
                string v = ConfigurationManager.AppSettings["OpenQueryOrderStatusJob"];
                List<string> list = v.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                return list[1];
            }
        }
    }
}
