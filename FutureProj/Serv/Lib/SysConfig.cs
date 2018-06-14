using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv
{
    public class SysConfig
    {
        /// <summary>
        /// 是否为测试环境
        /// </summary>
        public static bool IsTest
        {
            get
            {
                bool result = false;//默认是正式
                ConfigurationManager.RefreshSection("appSettings");
                string v = ConfigurationManager.AppSettings["IsTest"];
                bool.TryParse(v, out result);
                return result;
            }
        }
    }
}
