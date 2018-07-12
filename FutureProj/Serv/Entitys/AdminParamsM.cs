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
        public int ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 分类
        /// 新闻分类：对应tradehouse
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 数据分类
        /// </summary>
        public string Cate { get; set; }
        /// <summary>
        /// 交易品种Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartT { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndT { get; set; }
        /// <summary>
        /// 数字：月份
        /// </summary>
        public int Number { get; set; }
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
