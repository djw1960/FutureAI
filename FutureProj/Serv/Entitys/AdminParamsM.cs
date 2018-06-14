using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.Entitys
{
    public class AdminParamsM
    {

        #region 接口系统参数
        /// <summary>
        /// 系统-接口号
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 系统-接口版本号
        /// </summary>
        public int Ver { get; set; }
        /// <summary>
        /// 系统--客户号
        /// </summary>
        public string CustomerNo { get; set; }
        /// <summary>
        /// 接口签名方式 TYPE:MD5|RSA
        /// </summary>
        public string SignType { get; set; }
        /// <summary>
        /// 签名字符串
        /// </summary>
        public string Sign { get; set; }
        #endregion

        /// <summary>
        /// 登录token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 通用类型ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        #region 分页参数
        /// <summary>
        /// 当前页码 1开始
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int Size { get; set; }
        #endregion
    }
}
