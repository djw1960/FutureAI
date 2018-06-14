using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.Entitys
{
    /// <summary>
    /// 基础配置 0-10 公用
    /// </summary>
    public class ApiConfig
    {
        /// <summary>
        /// 获取支付通道列表
        /// </summary>
        public const int SERVICE_GetChannelList = 1000;
        /// <summary>
        /// 获取通道支持银行列表
        /// </summary>
        public const int SERVICE_GetChannelBankList = 1001;
        /// <summary>
        /// 网银支付下单
        /// </summary>
        public const int SERVICE_WebPay = 1010;
        /// <summary>
        /// 跳转类型快捷
        /// </summary>
        public const int SERVICE_KJSignUp = 1015;
        /// <summary>
        /// 快捷支付api下单发短信
        /// </summary>
        public const int SERVICE_KJCreateOrder = 1016;
        /// <summary>
        /// 快捷支付api确认支付
        /// </summary>
        public const int SERVICE_KJConfirmPay = 1017;
        /// <summary>
        /// 出金，代付
        /// </summary>
        public const int SERVICE_OutPut = 1020;
        #region 收银台模式接口
        /// <summary>
        /// 收银台模式下单 ，account-amount-billno-
        /// </summary>
        public const int PayCenter_OrderInit = 2000;
        /// <summary>
        /// 当前客户可用的支付通道
        /// </summary>
        public const int PayCenter_GetChannelList = 2001;
        /// <summary>
        /// 获取通道支持银行列表
        /// </summary>
        public const int PayCenter_GetChannelBankList = 2002;
        /// <summary>
        /// 网银支付下单
        /// </summary>
        public const int PayCenter_WebPay = 2003;
        /// <summary>
        /// 跳转类型快捷
        /// </summary>
        public const int PayCenter_KJSignUp = 2004;
        /// <summary>
        /// 快捷支付api下单发短信
        /// </summary>
        public const int PayCenter_KJCreateOrder = 2005;
        /// <summary>
        /// 快捷支付api确认支付
        /// </summary>
        public const int PayCenter_KJConfirmPay = 2006;
        /// <summary>
        /// 扫码支付下单
        /// 返回二维码
        /// </summary>
        public const int PayCenter_ScanPay = 2007;
        /// <summary>
        /// 扫码状态查询
        /// 返回扫码订单支付状态
        /// </summary>
        public const int PayCenter_ScanPayStatus = 2008;

        /// <summary>
        /// 获取订单信息
        /// </summary>
        public const int PayCenter_GetOrderInfo = 2010;
        /// <summary>
        /// 获取用户绑卡基础信息
        /// 姓名，身份证
        /// </summary>
        public const int PayCenter_GetUserInfo = 2011;
        /// <summary>
        /// 根据银行卡号获取银行卡相关信息
        /// </summary>
        public const int PayCenter_GetUserBindBankInfo = 2012;
        /// <summary>
        /// 用户已绑定的银行卡列表
        /// </summary>
        public const int PayCenter_GetUserBindBankList = 2013;
        /// <summary>
        /// 保存用户绑定卡信息
        /// </summary>
        public const int PayCenter_GetUserBindSaveBank = 2014;

        /// <summary>
        /// 当前客户可用的支付通道
        /// </summary>
        public const int PayCenterM_GetChannelList = 2021;
        /// <summary>
        /// 获取通道支持银行列表
        /// </summary>
        public const int PayCenterM_GetChannelBankList = 2022;
        /// <summary>
        /// 网银支付下单
        /// </summary>
        public const int PayCenterM_WebPay = 2023;
        /// <summary>
        /// 跳转类型快捷
        /// </summary>
        public const int PayCenterM_KJSignUp = 2024;
        /// <summary>
        /// 快捷支付api下单发短信
        /// </summary>
        public const int PayCenterM_KJCreateOrder = 2025;
        /// <summary>
        /// 快捷支付api确认支付
        /// </summary>
        public const int PayCenterM_KJConfirmPay = 2026;
        /// <summary>
        /// 扫码支付下单
        /// 返回二维码
        /// </summary>
        public const int PayCenterM_ScanPay = 2027;
        /// <summary>
        /// 扫码状态查询
        /// 返回扫码订单支付状态
        /// </summary>
        public const int PayCenterM_ScanPayStatus = 2028;
        #endregion
        #region 收银台后台管理接口
        /// <summary>
        /// 后台登录
        /// </summary>
        public const int Admin_Login = 3000;
        #endregion
        #region 产品管理
        /// <summary>
        /// 产品列表展现
        /// </summary>
        public const int Admin_ProductList = 3080;
        /// <summary>
        /// 产品详情查询
        /// </summary>
        public const int Admin_ProductModel = 308001;
        /// <summary>
        /// 产品新增
        /// </summary>
        public const int Admin_ProductAdd = 3081;
        /// <summary>
        /// 产品修改
        /// </summary>
        public const int Admin_ProductEdit = 3082;
        /// <summary>
        /// 产品删除
        /// </summary>
        public const int Admin_ProductDel = 3083;
        /// <summary>
        /// 获取当前产品支付通道信息
        /// </summary>
        public const int Admin_ProductPayChannelInfo = 3084;
        /// <summary>
        /// 设置当前产品支付通道信息 针对保存
        /// </summary>
        public const int Admin_ProductPayChannelSave = 3085;
        /// <summary>
        /// 设置当前产品支付通道信息 针对重置
        /// </summary>
        public const int Admin_ProductPayChannelReset = 3086;
        /// <summary>
        /// 设置产品启用禁用
        /// </summary>
        public const int Admin_ProductIsEnable = 3087;
        /// <summary>
        /// 获取产品平台支付通道
        /// </summary>
        public const int Admin_ProductPlatChannelInfo = 3088;
        #endregion
        #region 支付通道管理
        /// <summary>
        /// 设置支付通道展现
        /// </summary>
        public const int Admin_PaymentChannelList = 3090;
        /// <summary>
        /// 设置支付通道详情
        /// </summary>
        public const int Admin_PaymentChannelModel = 309001;
        /// <summary>
        /// 设置支付通道新增
        /// </summary>
        public const int Admin_PaymentChannelAdd = 3091;
        /// <summary>
        /// 设置支付通道修改
        /// </summary>
        public const int Admin_PaymentChannelEdit = 3092;
        /// <summary>
        /// 设置支付通道删除
        /// </summary>
        public const int Admin_PaymentChannelDel = 3093;
        /// <summary>
        /// 设置支付通道是否启用禁用
        /// </summary>
        public const int Admin_PaymentChannelIsEnable = 3094;
        #endregion
        #region 组别管理
        /// <summary>
        /// 列表展现
        /// </summary>
        public const int Admin_GroupList = 3201;
        /// <summary>
        /// 详情查询
        /// </summary>
        public const int Admin_GroupModel = 320101;
        /// <summary>
        /// 新增
        /// </summary>
        public const int Admin_GroupAdd = 3202;
        /// <summary>
        /// 修改
        /// </summary>
        public const int Admin_GroupEdit = 3203;
        /// <summary>
        /// 删除
        /// </summary>
        public const int Admin_GroupDel = 3204;
        #endregion
        #region 用户列表管理
        /// <summary>
        /// 用户列表展现
        /// </summary>
        public const int Admin_SysUserList = 3001;
        /// <summary>
        /// 用户新增
        /// </summary>
        public const int Admin_SysUserAdd = 3002;
        /// <summary>
        /// 用户编辑
        /// </summary>
        public const int Admin_SysUserEdit = 3003;
        /// <summary>
        /// 用户删除
        /// </summary>
        public const int Admin_SysUserDel = 3004;
        #endregion
        #region 客户管理
        /// <summary>
        /// 客户列表
        /// </summary>
        public const int Admin_CustomerList = 3010;
        /// <summary>
        /// 客户信息详情
        /// </summary>
        public const int Admin_CustomerDetails = 3011;
        /// <summary>
        /// 客户信息编辑
        /// </summary>
        public const int Admin_CustomerEdit = 3012;
        /// <summary>
        /// 删除客户
        /// </summary>
        public const int Admin_CustomerDel = 3013;
        #endregion
        #region 签约公司管理
        /// <summary>
        /// 签约公司列表
        /// </summary>
        public const int Admin_SignCompList = 3020;
        /// <summary>
        /// 签约公司详情
        /// </summary>
        public const int Admin_SignCompModel = 302001;
        /// <summary>
        /// 签约公司-新增
        /// </summary>
        public const int Admin_SignCompAdd = 3021;
        /// <summary>
        /// 签约公司-编辑
        /// </summary>
        public const int Admin_SignCompEdit = 3022;
        /// <summary>
        /// 签约公司-删除
        /// </summary>
        public const int Admin_SignCompDel = 3023;
        /// <summary>
        /// 签约公司-是否禁用
        /// </summary>
        public const int Admin_SignCompIsEnable = 3024;
        #endregion
        #region 支付服务商管理
        /// <summary>
        /// 支付供应商列表
        /// </summary>
        public const int Admin_PaySerPList = 3030;
        /// <summary>
        /// 支付供应商详情
        /// </summary>
        public const int Admin_PaySerPModel = 303001;
        /// <summary>
        /// 支付供应商新增
        /// </summary>
        public const int Admin_PaySerPAdd=3031;
        /// <summary>
        /// 支付供应商编辑
        /// </summary>
        public const int Admin_PaySerPEdit = 3032;
        /// <summary>
        /// 支付供应商删除
        /// </summary>
        public const int Admin_PaySerPDel=3033;
        /// <summary>
        /// 支付供应商是否禁用
        /// </summary>
        public const int Admin_PaySerPIsEnable = 3034;
        #region 商户管理
        /// <summary>
        /// 商户列表
        /// </summary>
        public const int Admin_PayMertList = 3040;
        /// <summary>
        /// 商户详情
        /// </summary>
        public const int Admin_PayMertModel = 304001;
        /// <summary>
        /// 商户新增
        /// </summary>
        public const int Admin_PayMertAdd = 3041;
        /// <summary>
        /// 商户编辑
        /// </summary>
        public const int Admin_PayMertEdit = 3042;
        /// <summary>
        /// 商户删除
        /// </summary>
        public const int Admin_PayMertDel = 3043;
        /// <summary>
        /// 商户启用禁用
        /// </summary>
        public const int Admin_PayMertIsEnable = 3044;
        #endregion
        #region 银行管理
        /// <summary>
        /// 银行列表
        /// </summary>
        public const int Admin_PaySerPBankList = 3120;
        /// <summary>
        /// 银行详情
        /// </summary>
        public const int Admin_PaySerPBankModel = 312001;
        /// <summary>
        /// 银行添加
        /// </summary>
        public const int Admin_PaySerPBankAdd = 3121;
        /// <summary>
        /// 银行修改
        /// </summary>
        public const int Admin_PaySerPBankEdit = 3122;
        /// <summary>
        /// 银行删除
        /// </summary>
        public const int Admin_PaySerPBankDel = 3123;
        /// <summary>
        /// 银行是否启用禁用
        /// </summary>
        public const int Admin_PaySerPBankIsEnable = 3124;
        #endregion
        #endregion
        #region 支付平台支持通道管理
        /// <summary>
        /// 支付平台支持通道列表
        /// </summary>
        public const int Admin_PaySerPWayList = 3134;
        /// <summary>
        /// 支付平台通道新增
        /// </summary>
        public const int Admin_PaySerPWayAdd = 3135;
        /// <summary>
        /// 支付平台通道编辑
        /// </summary>
        public const int Admin_PaySerPWayDel = 3136;
        #endregion
        
        #region 支付类型管理
        /// <summary>
        /// 支付类型列表
        /// </summary>
        public const int Admin_PayTypeList = 3050;
        /// <summary>
        /// 支付类型详情
        /// </summary>
        public const int Admin_PayTypeModel = 305001;
        /// <summary>
        /// 支付类型新增
        /// </summary>
        public const int Admin_PayTypeAdd = 3051;
        /// <summary>
        /// 支付类型编辑
        /// </summary>
        public const int Admin_PayTypeEdit = 3052;
        /// <summary>
        /// 支付类型删除
        /// </summary>
        public const int Admin_PayTypeDel = 3053;
        #endregion

        #region 代付管理
        /// <summary>
        /// 代付模板列表
        /// </summary>
        public const int Admin_PayTemplateList = 3060;
        /// <summary>
        /// 代付模板添加
        /// </summary>
        public const int Admin_PayTemplateAdd = 3061;
        /// <summary>
        /// 代付模板编辑
        /// </summary>
        public const int Admin_PayTemplateEdit = 3062;
        /// <summary>
        /// 代付模板删除
        /// </summary>
        public const int Admin_PayTemplateDel = 3063;
        #endregion
        #region 交易流水管理
        /// <summary>
        /// 交易流水列表
        /// </summary>
        public const int Admin_PayOrderList = 3070;

        #endregion

        #region 巨丰后台接口
        //巨丰后台接口
        public const int Manage_BindBankList = 3310;//获取绑定银行卡列表
        #endregion
    }
    public class RespCodeConfig
    {
        public const int OK = 1;
        public const int NotOK = 0;
        /// <summary>
        /// 请求正常响应，成功
        /// </summary>
        public const int Normal = 0;
        /// <summary>
        /// 参数为空
        /// </summary>
        public const int NullParam = 1;
        /// <summary>
        /// 参数格式错误
        /// </summary>
        public const int ArgumentExp = 2;
        /// <summary>
        /// 流程错误，一般性错误
        /// </summary>
        public const int Faild = 3;
        /// <summary>
        /// 序异常
        /// </summary>
        public const int ServerError = 4;
        /// <summary>
        /// 接口资源不存在
        /// </summary>
        public const int NoInterface = 5;
        /// <summary>
        /// 请求频率过快,服务器繁忙
        /// </summary>
        public const int ServerBusy = 6;
        /// <summary>
        /// 授权码无效
        /// </summary>
        public const int TokenError = 7;
        /// <summary>
        /// 授权码已过期
        /// </summary>
        public const int TokenTimeOut = 8;
        /// <summary>
        /// 上传失败
        /// </summary>
        public const int UploadError = 9;
        /// <summary>
        /// 验证签名失败
        /// </summary>
        public const int SignError = 10;
    }
    public class LogTypeConfig
    {
        /// <summary>
        /// 产品与收银台交互
        /// </summary>
        public const int PayMC = 1;
        /// <summary>
        /// 收银台与支付服务交互
        /// </summary>
        public const int PayMS = 2;
        /// <summary>
        /// 支付服务给收银台的异步结果通知
        /// </summary>
        public const int NotifyMS = 3;
        /// <summary>
        /// 收银台给产品的异步结果通知
        /// </summary>
        public const int NotifyMC = 4;
        /// <summary>
        /// 查询
        /// </summary>
        public const int Query = 5;
    }
    public class PayContentTypeConfig
    {
        /// <summary>
        /// 返回Url信息，此URL可以在浏览器中打开
        /// </summary>
        public const int QueryString = 1;
        /// <summary>
        /// 返回Form表单信息，可以在客户端自动提交
        /// </summary>
        public const int FormStr = 2;
        /// <summary>
        /// 返回Url信息，需要客户端根据Url生成二维码
        /// </summary>
        public const int ScanURL = 3;
        /// <summary>
        /// 表示走API接口，需要接着调用下个接口
        /// </summary>
        public const int APIKJ = 4;
    }
}
