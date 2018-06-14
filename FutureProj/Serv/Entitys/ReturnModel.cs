using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.Entitys
{
    public class ReturnModel
    {
        /// <summary>
        /// 1-成功，0-失败
        /// </summary>
        public int ok { get; set; }
        /// <summary>
        /// 通讯状态
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回内容主体
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 接口执行耗时
        /// </summary>
        public string Timespan { get; set; }
    }
    public class ListContent
    {
        public object list { get; set; }
        public PageContent page { get; set; }
    }
    public class PageContent
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int index { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int count { get; set; }
    }

    public class PayContent
    {
        /// <summary>
        /// 1-querystring链接跳转，取Url
        /// 2-Form表单提交跳转，取FormStr
        /// 3-扫码支付，返回Url,需要客户端根据返回生成二维码
        /// 4-需调用下个接口发短信，返回签约号，取SignNo，不一定有值，转API接口
        /// 
        /// 1-3表示流程已结束，4表示未结束需要调用下一个接口继续
        /// </summary>
        public int Type { get; set; }
        public string FormStr { get; set; }
        public string Url { get; set; }

    }
}
