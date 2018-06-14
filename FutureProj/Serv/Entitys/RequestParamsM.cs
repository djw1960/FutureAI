using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.Entitys
{
    public class RequestParamsM
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

        #region 业务参数
        /// <summary>
        /// 通用，各个接口中含义不同
        /// </summary>
        public string ID { get; set; }
        public string Account { get; set; }
        /// <summary>
        /// 用户账号类型 1-优质，2-普通，3-首次注资，4-风险
        /// </summary>
        public int AccountType { get; set; }
        /// <summary>
        /// 当前注资设备类型
        /// 1-PC，2-WAP ，3-WX,4-APP
        /// </summary>
        public int MediaType { get; set; }
        /// <summary>
        /// 支付通道id
        /// </summary>
        public string SignNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 订单token
        /// </summary>
        public string BillToken { get; set; }
        /// <summary>
        /// 银行编码
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 通道特定银行编码
        /// </summary>
        public string BankValue { get; set; }
        /// <summary>
        /// 金额-人民币单位分
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// 商家返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 商家异步通知地址
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 快捷支付短信验证码
        /// </summary>
        public string SmsCode { get; set; }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string SubjectDesrc { get; set; }
        /// <summary>
        /// 测试用参数，指定商户号测试
        /// </summary>
        public string MerNo { get; set; }

        #endregion
        /// <summary>
        /// 支付通道编码
        /// </summary>
        //public string PayWayCode { get; set; }

        #region 签约绑卡业务参数
        //绑卡四要素-身份证号
        public string IDCard { get; set; }
        //绑卡四要素-姓名
        public string UserName { get; set; }
        //绑卡四要素-银行卡号
        public string BankCard { get; set; }
        //绑卡四要素-手机号
        public string Mobile { get; set; }
        #endregion
        /// <summary>
        /// 支付类型
        /// 1-网银
        /// 2-快捷
        /// 3-微信扫码
        /// 4-支付宝扫码
        /// </summary>
        public int PayType { get; set; }
    }
}
