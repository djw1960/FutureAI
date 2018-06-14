using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.Entitys
{
    /// <summary>
    /// 支付通道操作编码
    /// </summary>
    public enum PayWayCodeType
    {
        /// <summary>
        /// 上海银联
        /// </summary>
        ChinaPay,
        /// <summary>
        /// 易宝
        /// </summary>
        YeePay,
        /// <summary>
        /// 广州银联
        /// </summary>
        Upop,
        /// <summary>
        /// 快钱
        /// </summary>
        Mpay,
        /// <summary>
        /// 联网通
        /// </summary>
        GatePayWay,
        /// <summary>
        /// 财付通
        /// </summary>
        TenPay,
        /// <summary>
        /// 汇能银联
        /// </summary>
        Synergy,
        /// <summary>
        /// 上海宝付
        /// </summary>
        BfoPay,
        /// <summary>
        /// 智付网银支付
        /// </summary>
        DinPay,
        /// <summary>
        /// 智付微信扫码支付
        /// </summary>
        DinPayScan,
        /// <summary>
        /// 手动入金
        /// </summary>
        SDRUJIN,
        /// <summary>
        /// 乘势科技微信扫码
        /// </summary>
        CsPayScanWXPay,
        /// <summary>
        /// 乘势科技支付宝
        /// </summary>
        CsPayScanAliPay,
        /// <summary>
        /// 爱农网银
        /// </summary>
        ChinagpayWYPay,
        /// <summary>
        /// 爱农快捷
        /// </summary>
        ChinagpayKJPay,
        /// <summary>
        /// 商银信
        /// </summary>
        YinsPay,
        /// <summary>
        /// 钱通快捷
        /// </summary>
        QianTongKJ,
        /// <summary>
        /// 智付快捷
        /// </summary>
        DinPayKJ,
        /// <summary>
        /// 高汇通快捷
        /// </summary>
        GHTPayKJ,
        /// <summary>
        /// 开联通快捷
        /// </summary>
        KLTPayKJ,
        /// <summary>
        /// 畅捷快捷
        /// </summary>
        CJPayKJ,
        /// <summary>
        /// 高汇通网银
        /// </summary>
        GHTWYPay,
        /// <summary>
        /// 易通快捷
        /// </summary>
        ETOneKJPay,
        /// <summary>
        /// 开联通
        /// </summary>
        KLTWebPay,
        /// <summary>
        /// 华融快捷-鑫圣金业
        /// </summary>
        HRKJPay,
        /// <summary>
        /// 多得宝
        /// </summary>
        DbBillPay,
        /// <summary>
        /// 华融网银-鑫圣金业
        /// </summary>
        HRWebPay,
        /// <summary>
        /// 畅捷网银
        /// </summary>
        CJWebPay,
        /// <summary>
        /// 连连快捷支付
        /// </summary>
        LLYKJPay,
        /// <summary>
        /// 易宝支付 微笑支付 网银
        /// </summary>
        SmileWebPay,

        /// <summary>
        /// 天付宝网银
        /// </summary>
        TFBWebPay,
        /// <summary>
        /// 畅捷1 微信扫码支付
        /// </summary>
        CJWXScanPay,
        /// <summary>
        /// 畅捷1 支付宝扫码支付
        /// </summary>
        CJAliScanPay,
        /// <summary>
        /// 银盛微信扫码
        /// </summary>
        YSWXScanPay,
        /// <summary>
        /// 银盛支付宝扫码
        /// </summary>
        YSAliScanPay,
        /// <summary>
        /// 开联通扫码支付
        /// </summary>
        KLTScanWxPay,
        /// <summary>
        /// 开联通扫码支付
        /// </summary>
        KLTScanAliPay,
        /// <summary>
        /// 前海米提斯快捷支付
        /// </summary>
        MeTisPayKJ,
        /// <summary>
		/// 前海米提斯网银支付
		/// </summary>
		MeTisWebPay,
        /// <summary>
        /// 盛灿(鑫圣商贸)快捷支付
        /// </summary>
        ShengCanPayKJ,
        /// <summary>
        /// 盛灿(鑫圣商贸)网银支付
        /// </summary>
        ShengCanWebPay,
        /// <summary>
        /// Globe网银支付
        /// </summary>
        GlobeWebPay,
        /// <summary>
        /// 以色列-ZotaPay支付
        /// </summary>
        ZotaPayWebPay,
        /// <summary>
        /// 华银网银-鑫圣金业
        /// </summary>
        HYPXJWebPay,
        /// <summary>
        /// 支付通网银
        /// </summary>
        ZFTWebPay,
        /// <summary>
        /// 支付通快捷
        /// </summary>
        ZFTPayKJ,
        /// <summary>
        /// 国付宝网银
        /// </summary>
        GFBWebPay,
        /// <summary>
        /// 亿宝通网银
        /// </summary>
        YBTWebPay,
        RPNWebPay,
        /// <summary>
        /// 皇冠支付-网银
        /// </summary>
        HGuanWebPay,
        /// <summary>
        /// 皇冠支付-快捷
        /// </summary>
        HGuanKJPay,
        /// <summary>
        /// 睿付通网银
        /// </summary>
        RFTWebPay,
        /// <summary>
        /// 掌付网银
        /// </summary>
        ZFPWebPay,
        /// <summary>
        /// 掌付快捷
        /// </summary>
        ZFPKJPay,
        /// <summary>
        /// 九派网银
        /// </summary>
        JPaiWebPay,
        /// <summary>
        /// 九派快捷
        /// </summary>
        JPaiKJPay,
        /// <summary>
        /// 杉德网银
        /// </summary>
        ShanDeWebPay,
        /// <summary>
        /// 杉德快捷
        /// </summary>
        ShanDeKJPay,
        /// <summary>
        /// 钱宝网银
        /// </summary>
        QBPWebPay,
        /// <summary>
        /// 新杉德网银
        /// </summary>
        SDPWebPay,
        /// <summary>
        /// 邦仁网银 
        /// </summary>
        BRFWebPay,
        /// <summary>
        /// 聚宝付网银
        /// </summary>
        JBFWebPay,
        /// <summary>
        /// 聚宝付快捷
        /// </summary>
        JBFPayKJ,
        /// <summary>
        /// 邦仁快捷
        /// </summary>
        BRFPayKJ,
        /// <summary>
        /// 钱进网银
        /// </summary>
        QJFWebPay,
        /// <summary>
        /// 钱进快捷
        /// </summary>
        QJFPayKJ,
        /// <summary>
        /// 云韦网银
        /// </summary>
        YWFWebPay,
        /// <summary>
        /// 云韦快捷
        /// </summary>
        YWFPayKJ,
        /// <summary>
        /// 五星网银
        /// </summary>
        WXFWebPay,
        /// <summary>
        /// 五星快捷
        /// </summary>
        WXFPayKJ,
        /// <summary>
        /// 顺捷网银
        /// </summary>
        SJFWebPay,
        /// <summary>
        /// 顺捷快捷
        /// </summary>
        SJFKJPay,
        /// <summary>
        /// 聚合网银
        /// </summary>
        JHFWebPay,
        /// <summary>
        /// 聚合快捷
        /// </summary>
        JHFPayKJ,
        /// <summary>
        /// 柒零捌零网银
        /// </summary>
        QLBLFWebPay,
        /// <summary>
        /// 柒零捌零快捷
        /// </summary>
        QLBLFPayKJ,
        /// <summary>
        /// 支付宝原生收银台扫码支付
        /// </summary>
        AlipayWebPay,
        /// <summary>
        /// 微信原生扫码支付
        /// </summary>
        WechatScanPay,
        /// <summary>
        /// 智汇付网银
        /// </summary>
        ZHFuWebPay,
        /// <summary>
        /// 易旨快捷支付
        /// </summary>
        YZFuPayKJ,
        /// <summary>
        /// 华势扫码支付
        /// </summary>
        HSScanPay,
        /// <summary>
        /// 云支付网银
        /// </summary>
        YunZFWebPay,
        /// <summary>
        /// 云支付快捷
        /// </summary>
        YunZFKJPay,
        /// <summary>
        /// 环迅网银
        /// </summary>
        HXFuWebPay,
        /// <summary>
        /// 环迅快捷
        /// </summary>
        HXFuKJPay,
        /// <summary>
        /// 支付宝扫码（当面付）
        /// </summary>
        AlipayScanPay

    }
    /// <summary>
    /// 支付平台查询接口编码
    /// </summary>
    public enum QueryCodeType
    {
        Default,
        TianXiaQuery,
        SmaileQuery,
        DuodbQuery,
        YinShengQuery,
        HuaRongQuery,
        GHTQuery,
        KLTQuery,
        ChangJieQuery,
        QianTongQuery,
        AiNongQuery,
        DinPayQuery,
        BaoFuQuery,
        SHYLQuery,
        CFTQuery,
        HuiNengQuery,
        LianLianQuery,
        YiTongQuery,
        MTSQuery,
        ShengCanQuery,
        GlobePaysQuery,
        ZotaPayQuery,
        HuaYinQuery,
        ZFTQuery,
        GFBQuery,
        YBTQuery,
        RPNPAYQuery,
        RFTQuery,
        HuangGuanQuery,
        ZhangFuQuery,
        JiuPaiQuery,
        ShanDeQuery,
        QianBaoQuery,
        NShanDeQuery,
        BangRenQuery,
        JBFQuery,
        LDFQuery,
        TengFuQuery,
        QianJinQuery,
        QLBLQuery,
        ZHFuQuery,
        YZFuQuery,
        AlipayQuery,
        WechatQuery,
        HXFuQuery
    }

    public enum CategorysType
    {
        PaySerPType,
        GoodsType
    }
    public enum GoodsType
    {
        手机=1,
        平板=2,
        电脑=3
    }
}
