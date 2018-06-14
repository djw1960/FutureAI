using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serv.Lib.Wechat
{
    public class WxPayConfig
    {
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置） wx1e53a40d38e900b6
        * MCHID：商户号（必须配置） 1501277701
        * KEY：商户支付密钥，参考开户邮件设置（必须配置） api支付秘钥 5CBD1785551853BD074D5377288E009D
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置） 98567ad74827cd6f34792dcbe5d64660
        */
        public string APPID { get; set; }
        public string MCHID { get; set; }
        public string KEY { get; set; }
        public string APPSECRET { get; set; }

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public string SSLCERT_PATH { get; set; }
        public string SSLCERT_PASSWORD { get; set; }
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 接口Api地址 :https://api.mch.weixin.qq.com 
        /// </summary>
        public string ApiUrl { get; set; }

        public string NOTIFY_URL { get; set; }

        public int REPORT_LEVENL = 0;
    }
}
