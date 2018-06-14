using EF.Entitys;
using PayService.Serv.Entitys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace PayService.Serv.ServiceProvider
{
    public class DinPayService
    {
        /// <summary>
        /// 网关支付
        /// </summary>
        /// <param name="result"></param>
        /// <param name="model"></param>
        /// <param name="order"></param>
        /// <param name="paywayMer"></param>
        public static void WebPay(ReturnModel result, RequestParamsM model, PayOrder order, View_PayMerSignWay paywayMer)
        {
            //入口，处理订单
            string orderno = Common.GetBillNo(32);//生成32位订单号
            order.OrderNo = orderno;
            string amount = ((decimal)order.Amount / 100).ToString("0.00");
            string returnUrl = order.MediaType == 1 ? paywayMer.ReturnUrl : paywayMer.ReturnUrlM;
            string formHTML = IWebPay(paywayMer, amount, order.OrderNo, model.BankValue, returnUrl);
            result.data = new PayContent() { Type = PayContentTypeConfig.FormStr, FormStr = formHTML, Url = "" };
            result.code = RespCodeConfig.Normal;
        }
        /// <summary>
        /// 开联通签约查询
        /// </summary>
        /// <param name="result"></param>
        /// <param name="model"></param>
        /// <param name="order"></param>
        /// <param name="paywayMer"></param>
        public static void KJSignPay(ReturnModel result, RequestParamsM model, PayOrder order, View_PayMerSignWay paywayMer)
        {
            string billno = Common.GetBillNo(32);
            string amount = order.Account.ToString();//单位分
            string returnUrl = order.MediaType == 1 ? paywayMer.ReturnUrl : paywayMer.ReturnUrlM;
            BindCardModel bindModel = new BindCardModel()
            {
                IDCard = model.IDCard,
                BankCard = model.BankCard,
                Mobile = model.Mobile,
                UserName = model.UserName
            };
            DinPayResultMsg r = IKJSign_query(paywayMer, model.BankValue, bindModel);
            
        }

        #region 开联通支付接口

        /// <summary>
        /// 提交方式
        /// </summary>
        /// <param name="order_money">金额</param>
        /// <param name="strOrderNum">(已方)订单号</param>  
        /// <param name="strBankID">银行ID</param>
        /// <param name="strNotifyUrl">服务器异步通知地址</param>
        /// <returns></returns>
        public static string IWebPay(View_PayMerSignWay paywayMer, string amount, string billno, string bankcode, string returnUrl)
        {
            /////////////////////////////////接收表单提交参数//////////////////////////////////////
            ////////////////////////To receive the parameter form HTML form//////////////////////

            string input_charset1 = "UTF-8";//参数编码字符集
            string interface_version1 = "V3.0";//接口版本
            string merchant_code1 = paywayMer.MerNo;// SettingHelper.strDinPayMemberID;//商户号;
            string notify_url1 = paywayMer.NotifyUrlUS;//服务器异步通知地址
            string order_amount1 = amount;//商家订单金额（精确到小数点后两位.例如：12.01）
            string order_no1 = billno;//商家订单号（已方生成）
            string order_time1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//商家订单时间
            string sign_type1 = "RSA-S";//签名方式（取值为：RSA 或RSA-S）
            string product_code1 = "";//商品编号
            string product_desc1 = "";//商品描述
            string product_name1 = "智付方式注资";//商品名称
            string product_num1 = "";//商品数量
            string return_url1 = returnUrl;// SettingHelper.PageRetUrl;//页面跳转同步通知地址 
            string service_type1 = "direct_pay";///服务类型（固定值：direct_pay）
            string show_url1 = "";//商品展示URL
            string extend_param1 = "";//业务扩展参数
            string extra_return_param1 = "";//回传参数
            string bank_code1 = bankcode;//网银直连银行代码（当该参数为空或与对照表中银行编码不一致时，直接跳转到智付收银台选择银行页面）
            string client_ip1 = "";//客户端IP
            string redo_flag1 = "1";//是否允许重复订单（当值为1 时不允许商户订单号重复提交；当值为0或空时允许商户订单号重复提交）
            string pay_type1 = "";//支付类型
            string sign = "";//智付返回签名数据

            ////////////////组装签名参数//////////////////

            string signSrc = "";

            //组织订单信息
            if (bank_code1 != "")
            {
                signSrc = signSrc + "bank_code=" + bank_code1 + "&";
            }
            if (client_ip1 != "")
            {
                signSrc = signSrc + "client_ip=" + client_ip1 + "&";
            }
            if (extend_param1 != "")
            {
                signSrc = signSrc + "extend_param=" + extend_param1 + "&";
            }
            if (extra_return_param1 != "")
            {
                signSrc = signSrc + "extra_return_param=" + extra_return_param1 + "&";
            }
            if (input_charset1 != "")
            {
                signSrc = signSrc + "input_charset=" + input_charset1 + "&";
            }
            if (interface_version1 != "")
            {
                signSrc = signSrc + "interface_version=" + interface_version1 + "&";
            }
            if (merchant_code1 != "")
            {
                signSrc = signSrc + "merchant_code=" + merchant_code1 + "&";
            }
            if (notify_url1 != "")
            {
                signSrc = signSrc + "notify_url=" + notify_url1 + "&";
            }
            if (order_amount1 != "")
            {
                signSrc = signSrc + "order_amount=" + order_amount1 + "&";
            }
            if (order_no1 != "")
            {
                signSrc = signSrc + "order_no=" + order_no1 + "&";
            }
            if (order_time1 != "")
            {
                signSrc = signSrc + "order_time=" + order_time1 + "&";
            }
            if (pay_type1 != "")
            {
                signSrc = signSrc + "pay_type=" + pay_type1 + "&";
            }
            if (product_code1 != "")
            {
                signSrc = signSrc + "product_code=" + product_code1 + "&";
            }
            if (product_desc1 != "")
            {
                signSrc = signSrc + "product_desc=" + product_desc1 + "&";
            }
            if (product_name1 != "")
            {
                signSrc = signSrc + "product_name=" + product_name1 + "&";
            }
            if (product_num1 != "")
            {
                signSrc = signSrc + "product_num=" + product_num1 + "&";
            }
            if (redo_flag1 != "")
            {
                signSrc = signSrc + "redo_flag=" + redo_flag1 + "&";
            }
            if (return_url1 != "")
            {
                signSrc = signSrc + "return_url=" + return_url1 + "&";
            }
            if (service_type1 != "")
            {
                signSrc = signSrc + "service_type=" + service_type1;
            }
            if (show_url1 != "")
            {
                signSrc = signSrc + "&show_url=" + show_url1;
            }

            if (sign_type1 == "RSA-S")//RSA-S签名方法
            {
                /**  merchant_private_key，商户私钥，商户按照《密钥对获取工具说明》操作并获取商户私钥。获取商户私钥的同时，也要
                    获取商户公钥（merchant_public_key）并且将商户公钥上传到智付商家后台"公钥管理"（如何获取和上传请看《密钥对获取工具说明》），
                    不上传商户公钥会导致调试的时候报错“签名错误”。
               */

                //demo提供的merchant_private_key是测试商户号1111110166的商户私钥，请自行获取商户私钥并且替换。

                string merchant_private_key = paywayMer.MerPrivateKey;// SettingHelper.strDinPayMerchantPrivateKey;
                //私钥转换成C#专用私钥
                merchant_private_key = RSASignUtils.RSAPrivateKeyJava2DotNet(merchant_private_key);
                //签名
                string signData = RSASignUtils.RSASign(signSrc, merchant_private_key);
                sign = signData;
            }
            else  //RSA签名方法（暂不起用）
            {
                //RSAWithHardware rsa = new RSAWithHardware();
                //string merPubKeyDir = "D:/1111110166.pfx";   //证书路径
                //string password = "87654321";                //证书密码
                //RSAWithHardware rsaWithH = new RSAWithHardware();
                //rsaWithH.Init(merPubKeyDir, password, "D:/dinpayRSAKeyVersion");//初始化
                //string signData = rsaWithH.Sign(signSrc);    //签名
                //sign = signData;
            }

            #region 
            var strForm = new StringBuilder();

            strForm.AppendFormat("<form name=\"dinpayForm\" id=\"dinpayForm\" method=\"post\" action=\"{0}\">", paywayMer.MerWebApiUrl);

            strForm.AppendFormat("<input name = 'sign' id='sign' type = 'hidden' value = \"{0}\" />", sign);

            strForm.AppendFormat("<input name = 'merchant_code' id='merchant_code' type = 'hidden' value = \"{0}\" />", merchant_code1);

            strForm.AppendFormat("<input name = 'bank_code' id='bank_code' type = 'hidden' value = \"{0}\" />", bank_code1);

            strForm.AppendFormat("<input name = 'order_no' id='order_no' type = 'hidden' value = \"{0}\" />", order_no1);

            strForm.AppendFormat("<input name = 'order_amount' id='order_amount' type = 'hidden' value = \"{0}\" />", order_amount1);

            strForm.AppendFormat("<input name = 'service_type' id='service_type' type = 'hidden' value = \"{0}\" />", service_type1);

            strForm.AppendFormat("<input name = 'input_charset' id='input_charset' type = 'hidden' value = \"{0}\" />", input_charset1);

            strForm.AppendFormat("<input name = 'notify_url' id='notify_url' type = 'hidden' value = \"{0}\" />", notify_url1);

            strForm.AppendFormat("<input name = 'interface_version' id='interface_version' type = 'hidden' value = \"{0}\" />", interface_version1);

            strForm.AppendFormat("<input name = 'sign_type' id='sign_type' type = 'hidden' value = \"{0}\" />", sign_type1);

            strForm.AppendFormat("<input name = 'order_time' id='order_time' type = 'hidden' value = \"{0}\" />", order_time1);

            strForm.AppendFormat("<input name = 'product_name' id='product_name' type = 'hidden' value = \"{0}\" />", product_name1);

            strForm.AppendFormat("<input name = 'client_ip' id='client_ip' type = 'hidden' value = \"{0}\" />", client_ip1);

            strForm.AppendFormat("<input name = 'extend_param' id='extend_param' type = 'hidden' value = \"{0}\" />", extend_param1);

            strForm.AppendFormat("<input name = 'extra_return_param' id='extra_return_param' type = 'hidden' value = \"{0}\" />", extra_return_param1);

            strForm.AppendFormat("<input name = 'product_code' id='product_code' type = 'hidden' value = \"{0}\" />", product_code1);

            strForm.AppendFormat("<input name = 'product_desc' id='product_desc' type = 'hidden' value = \"{0}\" />", product_desc1);

            strForm.AppendFormat("<input name = 'product_num' id='product_num' type = 'hidden' value = \"{0}\" />", product_num1);

            strForm.AppendFormat("<input name = 'return_url' id='return_url' type = 'hidden' value = \"{0}\" />", return_url1);

            strForm.AppendFormat("<input name = 'show_url' id='show_url' type = 'hidden' value = \"{0}\" />", show_url1);

            strForm.AppendFormat("<input name = 'redo_flag' id='redo_flag' type = 'hidden' value = \"{0}\" />", redo_flag1);

            strForm.AppendFormat("<input name = 'pay_type' id='pay_type' type = 'hidden' value = \"{0}\" />", pay_type1);

            strForm.AppendFormat("</form >");

            strForm.AppendFormat("<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('dinpayForm').submit();\",10);</script>");
            #endregion
            return strForm.ToString();
        }
        /// <summary>
        /// 智付快捷支付--签约查询接口
        /// 根据用户卡信息，查询该卡是否签约快捷支付
        /// </summary>
        /// <returns></returns>
        public static DinPayResultMsg IKJSign_query(View_PayMerSignWay paywayMer, string bankCode, BindCardModel bindModel)
        {
            DinPayResultMsg result = new DinPayResultMsg() { is_success = false };
            try
            {
                string interface_version = "V3.0";
                string input_charset = "UTF-8";
                string service_type = "sign_query";
                string sign_type = "RSA-S";
                string merchant_code = paywayMer.MerNo;//商户号;
                string bank_code = bankCode;//支付通道银行编码
                string card_type = "0";//0-借记卡，1-信用卡
                string card_no = bindModel.BankCard;//银行卡号
                string mobile = bindModel.Mobile;//开卡手机
                string merchant_sign_id = "";//签约号，可选，与card_no 二选一

                ////////////////组装签名/////////////////
                //银行卡和签约号选输其一
                string signStr = "";
                signStr = "bank_code=" + bank_code + "&card_no=" + card_no + "&card_type=" + card_type + "&input_charset=" + input_charset + "&interface_version=" + interface_version + "&merchant_code=" + merchant_code;
                if (merchant_sign_id != "")
                {
                    signStr = signStr + "&merchant_sign_id=" + merchant_sign_id + "&service_type=" + service_type;
                }
                else
                {
                    signStr = signStr + "&mobile=" + mobile + "&service_type=" + service_type;
                }

                if (sign_type == "RSA-S")//RSA-S签名方法
                {
                    //商家私钥
                    string merPriKey = paywayMer.MerPrivateKey;// SettingHelper.strDinPayMerchantPrivateKey;
                    //私钥转换成C#专用私钥
                    merPriKey =RSASignUtils.RSAPrivateKeyJava2DotNet(merPriKey);
                    //签名
                    string signData = RSASignUtils.RSASign(signStr, merPriKey);
                    //将signData进行UrlEncode编码
                    signData = HttpUtility.UrlEncode(signData);

                    //组装字符串
                    string para = signStr + "&sign_type=" + sign_type + "&sign=" + signData;
                    //将字符串发送到Dinpay网关
                    string _xml =HttpUtils.HttpPost(paywayMer.MerKJApiUrl, para);

                    //将同步返回的xml中的参数提取出来
                    var el = XElement.Load(new StringReader(_xml));
                    GetResultResp(_xml, result);
                }
            }
            catch (Exception ex)
            {
                result.error_msg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 智付快捷支付--网页签约支付页面
        /// </summary>
        /// <param name="order_money"></param>
        /// <param name="order_num"></param>
        /// <param name="bankCode"></param>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public static string GetKJWeb_Pay(View_PayMerSignWay paywayMer, string order_money, string order_num, string strNotifyUrl, string ordertime, string bankCode, BindCardModel bindModel)
        {

            string interface_version = "V3.0";
            string input_charset = "UTF-8";
            string service_type = "express_web_sign_pay";
            string sign_type = "RSA-S";
            string merchant_code = paywayMer.MerNo;// SettingHelper.strDinPayMemberID;//商户号;
            string order_no = order_num;
            string order_amount = order_money;
            string order_time = ordertime;
            string notify_url = strNotifyUrl;
            string card_type = "0";
            string mobile = bindModel.Mobile;
            string bank_code = bankCode;//支付通道银行编码
            string product_name = "货款";

            string card_no = bindModel.BankCard;
            string card_name = bindModel.UserName;
            string id_no = bindModel.IDCard;
            string encrypt_info = card_no + "|" + card_name + "|" + id_no; //组装敏感数据，信用卡需将信用卡信息加入
            ////使用加密密钥对卡号和卡密加密【加密密钥需从商家后台-公钥管理中取出】//////////
            string dinpayPubKey = paywayMer.MerVerifyPublicKey;// SettingHelper.strDinPayPublicKey;
            //////////将公钥转换成C#专用格式///////////
            dinpayPubKey =RSASignUtils.RSAPublicKeyJava2DotNet(dinpayPubKey);
            //加密后的卡号密码
            string encrypt_info2 =RSASignUtils.RSAEncrypt(encrypt_info, dinpayPubKey);
            ////////////////组装签名/////////////////

            string signStr = "bank_code=" + bank_code + "&card_type=" + card_type + "&encrypt_info=" + encrypt_info2 + "&input_charset=" + input_charset + "&interface_version=" + interface_version + "&merchant_code=" + merchant_code + "&mobile=" + mobile + "&notify_url=" + notify_url + "&order_amount=" + order_amount + "&order_no=" + order_no + "&order_time=" + order_time + "&product_name=" + product_name + "&service_type=" + service_type;
            string signData = "";

            if (sign_type == "RSA-S")//RSA-S签名方法
            {
                //商家私钥
                string merPriKey = paywayMer.MerPrivateKey;// SettingHelper.strDinPayMerchantPrivateKey;
                //私钥转换成C#专用私钥
                merPriKey = RSASignUtils.RSAPrivateKeyJava2DotNet(merPriKey);
                //签名
                signData = RSASignUtils.RSASign(signStr, merPriKey);
            }

            #region
            var strForm = new StringBuilder();

            strForm.AppendFormat("<form action=\"{0}\" id=\"dinpayForm\" name=\"dinpayForm\" method=\"POST\">", paywayMer.MerKJApiUrl);

            strForm.AppendFormat("<input type=\"hidden\" name=\"sign\" id=\"sign\" value=\"{0}\" />", signData);

            strForm.AppendFormat("<input type=\"hidden\" name=\"interface_version\" id=\"interface_version\" value=\"{0}\" />", interface_version);

            strForm.AppendFormat("<input type=\"hidden\" name=\"input_charset\" id=\"input_charset\" value=\"{0}\" />", input_charset);

            strForm.AppendFormat("<input type=\"hidden\" name=\"service_type\" id=\"service_type\" value=\"{0}\" />", service_type);

            strForm.AppendFormat("<input type=\"hidden\" name=\"sign_type\" id=\"sign_type\" value=\"{0}\" />", sign_type);

            strForm.AppendFormat("<input type=\"hidden\" name=\"merchant_code\" id=\"merchant_code\" value=\"{0}\" />", merchant_code);

            strForm.AppendFormat("<input type=\"hidden\" name=\"order_no\" id=\"order_no\" value=\"{0}\" />", order_no);

            strForm.AppendFormat("<input type=\"hidden\" name=\"order_amount\" id=\"order_amount\" value=\"{0}\" />", order_amount);

            strForm.AppendFormat("<input type=\"hidden\" name=\"order_time\" id=\"order_time\" value=\"{0}\" />", order_time);

            strForm.AppendFormat("<input type=\"hidden\" name=\"notify_url\" id=\"notify_url\" value=\"{0}\" />", notify_url);

            strForm.AppendFormat("<input type=\"hidden\" name=\"card_type\" id=\"card_type\" value=\"{0}\" />", card_type);

            strForm.AppendFormat("<input type=\"hidden\" name=\"mobile\" id=\"mobile\"  value=\"{0}\" />", mobile);

            strForm.AppendFormat("<input type=\"hidden\" name=\"bank_code\" id=\"bank_code\" value=\"{0}\"  />", bank_code);

            strForm.AppendFormat("<input type=\"hidden\" name=\"product_name\" id=\"product_name\" value=\"{0}\" />", product_name);

            strForm.AppendFormat("<input type=\"hidden\" name=\"encrypt_info\" id=\"encrypt_info\" value=\"{0}\" />", encrypt_info2);

            strForm.AppendFormat("</form >");

            strForm.AppendFormat("<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('dinpayForm').submit();\",10);</script>");
            #endregion

            return strForm.ToString();
        }
        /// <summary>
        /// 智付快捷下单获取短信验证码
        /// </summary>
        /// <param name="order_money"></param>
        /// <param name="order_num"></param>
        /// <param name="bankCode"></param>
        /// <param name="strNotifyUrl"></param>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public static DinPayResultMsg GetKJSignPaySmsCode(string signid, string order_money, string order_num, string bankCode, string strNotifyUrl, BindCardModel bindModel, View_PayMerSignWay paywayMer)
        {
            DinPayResultMsg result = new DinPayResultMsg() { is_success = false };
            try
            {
                string interface_version = "V3.0";
                string input_charset = "UTF-8";
                string service_type = "sign_pay_sms_code";
                string sign_type = "RSA-S";
                string merchant_code = paywayMer.MerNo;
                string order_no = order_num;
                string order_amount = order_money;
                string sms_type = "1";//0-签约+支付验证码 1-支付验证码
                string send_type = "0";//0-平台下发
                string merchant_sign_id = signid;//快捷支付签约号 已签约用户必输
                string card_type = "0";//0-借记卡 1-信用卡 未签约用户必输
                string mobile =bindModel.Mobile;//待签约的手机号
                string bank_code = bankCode;//未签约用户必输

                string card_no = bindModel.BankCard;
                string card_name = bindModel.UserName;
                string id_no = bindModel.IDCard;
                string encrypt_info = card_no + "|" + card_name + "|" + id_no; //组装敏感数据
                ////使用智付公钥对卡号和卡密加密【智付公钥需从商家后台-公钥管理中取出】//////////
                string dinpayPubKey = paywayMer.MerVerifyPublicKey;
                //////////将加密密钥转换成C#专用格式///////////
                dinpayPubKey = RSASignUtils.RSAPublicKeyJava2DotNet(dinpayPubKey);
                //加密后的卡号密码
                string encrypt_info_result = RSASignUtils.RSAEncrypt(encrypt_info, dinpayPubKey);
                ////////////////组装签名/////////////////

                string signStr = "bank_code=" + bank_code + "&card_type=" + card_type + "&encrypt_info=" + encrypt_info_result + "&input_charset=" + input_charset + "&interface_version=" + interface_version + "&merchant_code=" + merchant_code + "&merchant_sign_id=" + merchant_sign_id + "&mobile=" + mobile + "&order_amount=" + order_amount + "&order_no=" + order_no + "&send_type=" + send_type + "&service_type=" + service_type + "&sms_type=" + sms_type;

                if (sign_type == "RSA-S")//RSA-S签名方法
                {
                    //商家私钥
                    string merPriKey = paywayMer.MerPrivateKey;
                    //私钥转换成C#专用私钥
                    merPriKey = RSASignUtils.RSAPrivateKeyJava2DotNet(merPriKey);
                    //签名
                    string signData = RSASignUtils.RSASign(signStr, merPriKey);
                    //将signData进行UrlEncode编码
                    signData = HttpUtility.UrlEncode(signData);
                    //将加密后的卡号卡密进行UrlEncode编码
                    encrypt_info_result = HttpUtility.UrlEncode(encrypt_info_result);

                    //组装字符串
                    //string para = signStr + "&sign_type=" + sign_type + "&sign=" + signData;
                    string para = "bank_code=" + bank_code + "&card_type=" + card_type + "&encrypt_info=" + encrypt_info_result + "&input_charset=" + input_charset + "&interface_version=" + interface_version + "&merchant_code=" + merchant_code + "&merchant_sign_id=" + merchant_sign_id + "&mobile=" + mobile + "&order_amount=" + order_amount + "&order_no=" + order_no + "&send_type=" + send_type + "&service_type=" + service_type + "&sms_type=" + sms_type + "&sign_type=" + sign_type + "&sign=" + signData;
                    //将字符串发送到Dinpay网关
                    string _xml =HttpUtils.HttpPost(paywayMer.MerKJApiUrl, para);
                    GetResultResp(_xml, result);
                    if (result.is_success)
                    {
                        ////组装验签字符串
                        //string signsrc = "is_success=" + (result.is_success ? "T" : "F") + "&merchant_code=" + result.merchant_code + "&order_no=" + result.order_no + "&sms_trade_no=" + result.sms_trade_no;
                        ////使用智付公钥对返回的数据验签
                        //bool validateResult = HttpHelp.ValidateRsaSign(signsrc, dinpayPubKey, result.sign);

                        //if (validateResult == false)
                        //{
                        //    result.error_msg = "验签失败";
                        //}
                    }

                }
            }
            catch (Exception ex)
            {
                result.error_msg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 智付快捷支付-输入短信验证码确认支付
        /// </summary>
        /// <param name="order_money"></param>
        /// <param name="order_num"></param>
        /// <param name="ordertime"></param>
        /// <param name="bankCode"></param>
        /// <param name="strNotifyUrl"></param>
        /// <param name="sms_tradeno"></param>
        /// <param name="smscode"></param>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public static DinPayResultMsg GetKJPayConfirm(string signno, string order_money, string order_num, string ordertime, string bankCode, string strNotifyUrl, string sms_tradeno, string smscode, BindCardModel bindModel, View_PayMerSignWay paywayMer)
        {
            DinPayResultMsg result = new DinPayResultMsg() { is_success = false };
            try
            {
                string interface_version = "V3.0";
                string input_charset = "UTF-8";
                string service_type = "express_sign_pay";
                string sign_type = "RSA-S";
                string merchant_code = paywayMer.MerNo;//商户号;
                string order_no = order_num;
                string order_amount = order_money;
                string order_time = ordertime;
                string notify_url = strNotifyUrl;
                string merchant_sign_id = signno;
                string mobile =bindModel.Mobile;
                string sms_trade_no = sms_tradeno;
                string sms_code = smscode;
                string product_name = "hk00a";

                string card_no = bindModel.BankCard;
                string card_name = bindModel.UserName;
                string id_no =bindModel.IDCard;
                string card_cvv2 = "";
                string card_exp_date = "";
                string encrypt_info = card_no + "|" + card_name + "|" + id_no; //组装敏感数据，信用卡用户需加入信用卡信息
                ////使用智付公钥对卡号和卡密加密【智付公钥需从商家后台-公钥管理中取出】//////////
                string dinpayPubKey = paywayMer.MerVerifyPublicKey;
                //////////将公钥转换成C#专用格式///////////
                dinpayPubKey = RSASignUtils.RSAPublicKeyJava2DotNet(dinpayPubKey);
                //加密后的卡号密码
                string encrypt_info_result = RSASignUtils.RSAEncrypt(encrypt_info, dinpayPubKey);
                ////////////////组装签名/////////////////
                string signStr = "";

                signStr = "encrypt_info=" + encrypt_info_result + "&input_charset=" + input_charset + "&interface_version=" + interface_version + "&merchant_code=" + merchant_code + "&merchant_sign_id=" + merchant_sign_id + "&mobile=" + mobile + "&notify_url=" + notify_url + "&order_amount=" + order_amount + "&order_no=" + order_no + "&order_time=" + order_time + "&product_name=" + product_name + "&service_type=" + service_type + "&sms_code=" + sms_code + "&sms_trade_no=" + sms_trade_no;

                if (sign_type == "RSA-S")//RSA-S签名方法
                {
                    //商家私钥
                    string merPriKey = paywayMer.MerPrivateKey;
                    //私钥转换成C#专用私钥
                    merPriKey = RSASignUtils.RSAPrivateKeyJava2DotNet(merPriKey);
                    //签名
                    string signData = RSASignUtils.RSASign(signStr, merPriKey);
                    //将signData进行UrlEncode编码
                    signData = HttpUtility.UrlEncode(signData);
                    //将加密后的卡号卡密进行UrlEncode编码
                    encrypt_info_result = HttpUtility.UrlEncode(encrypt_info_result);

                    //组装字符串
                    string para = "encrypt_info=" + encrypt_info_result + "&input_charset=" + input_charset + "&interface_version=" + interface_version + "&merchant_code=" + merchant_code + "&merchant_sign_id=" + merchant_sign_id + "&mobile=" + mobile + "&notify_url=" + notify_url + "&order_amount=" + order_amount + "&order_no=" + order_no + "&order_time=" + order_time + "&product_name=" + product_name + "&service_type=" + service_type + "&sms_code=" + sms_code + "&sms_trade_no=" + sms_trade_no + "&sign_type=" + sign_type + "&sign=" + signData;
                    //将字符串发送到Dinpay网关
                    string _xml = HttpUtils.HttpPost(paywayMer.MerKJApiUrl, para);
                    GetResultResp(_xml, result);

                    if (result.is_success)
                    {
                        ////组装验签字符串
                        //string signsrc = "is_success=" + (result.is_success?"T":"F") + "&merchant_code=" + result.merchant_code + "&merchant_sign_id=" + result.merchant_sign_id + "&order_no=" + result.order_no + "&trade_no=" + result.trade_no + "&trade_status=" + result.trade_status + "&trade_time=" + result.trade_time;
                        ////使用智付公钥对返回的数据验签
                        //bool validateResult = HttpHelp.ValidateRsaSign(signsrc, dinpayPubKey, result.sign);

                        //if (validateResult == false)
                        //{
                        //    result.is_success = false;
                        //    result.error_msg = "验签失败";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                result.error_msg = ex.Message;
            }
            return result;
        } 
        #endregion

        /// <summary>
        /// 响应xml的节点集合
        /// </summary>
        private static string[] RespArr = { "is_success", "error_code", "error_msg", "sign_type", "sign", "merchant_code",
                                              "merchant_sign_id", "sign_status", "pay_model", "order_no", "sms_trade_no",
                                              "sms_code", "order_amount", "trade_no", "trade_time", "trade_status" };
        /// <summary>
        /// 快捷支付实名认证消息返回
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="result"></param>
        public static void GetResultResp(string xml, DinPayResultMsg result)
        {
            XElement dinpay = XElement.Load(new StringReader(xml));
            XElement respose = dinpay.Element("response");
            foreach (string item in RespArr)
            {
                XElement itemElement = respose.Element(item);
                if (itemElement != null)
                {
                    switch (item)
                    {
                        case "is_success":
                            result.is_success = itemElement.Value == "T";
                            break;
                        case "error_code":
                            result.error_code = itemElement.Value;
                            break;
                        case "error_msg":
                            result.error_msg = itemElement.Value;
                            break;
                        case "sign_type":
                            result.sign_type = itemElement.Value;
                            break;
                        case "sign":
                            result.sign = itemElement.Value;
                            break;
                        case "merchant_code":
                            result.merchant_code = itemElement.Value;
                            break;
                        case "merchant_sign_id":
                            result.merchant_sign_id = itemElement.Value;
                            break;
                        case "sign_status":
                            result.sign_status = itemElement.Value;
                            break;
                        case "pay_model":
                            result.pay_model = itemElement.Value;
                            break;
                        case "order_no":
                            result.order_no = itemElement.Value;
                            break;
                        case "sms_trade_no":
                            result.sms_trade_no = itemElement.Value;
                            break;
                        case "sms_code":
                            result.sms_code = itemElement.Value;
                            break;
                        case "order_amount":
                            result.order_amount = itemElement.Value;
                            break;
                        case "trade_time":
                            result.trade_time = itemElement.Value;
                            break;
                        case "trade_status":
                            result.trade_status = itemElement.Value;
                            break;
                    }
                }

            }
        }
    }
    public class DinPayResultMsg
    {
        /// <summary>
        /// 仅表示本次交易是否成功，不代表支付状态 T 代表成功 F 代表失败
        /// </summary>
        public bool is_success { get; set; }
        /// <summary>
        /// 错误信息编码 (出现异常时返回)
        /// </summary>
        public string error_code { get; set; }
        /// <summary>
        /// 错误码信息提示 (出现异常时返回)
        /// </summary>
        public string error_msg { get; set; }
        /// <summary>
        /// 参数名称：签名方式 取值为：RSA或RSA-S
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 参数名称：签名数据 该字段不参与签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string merchant_code { get; set; }
        /// <summary>
        /// 快捷支付签约号
        /// </summary>
        public string merchant_sign_id { get; set; }
        /// <summary>
        /// 签约状态 0：已签约 1：已解约 2：未签约
        /// </summary>
        public string sign_status { get; set; }
        /// <summary>
        /// 签约支付方式 0：API签约支付 1：WEB签约支付
        /// </summary>
        public string pay_model { get; set; }
        /// <summary>
        /// 商户系统订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 短信验证码流水号 (调用签约支付接口需要上送此流水 号)
        /// </summary>
        public string sms_trade_no { get; set; }
        /// <summary>
        /// 生成的短信验证码 (已发送至手机时不返回)
        /// </summary>
        public string sms_code { get; set; }
        /// <summary>
        /// 商户订单金额
        /// </summary>
        public string order_amount { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 智 付 订 单 交 易 时 间 ， 格 式 为 ： yyyy-MM-dd HH:mm:ss ， 举 例 ： 2013-12-1 12:23:34。 当支付状态为处理中，订单时间为受理成功时间
        /// </summary>
        public string trade_time { get; set; }
        /// <summary>
        /// 该笔订单支付状态 SUCCESS 支付成功 DOING 受理成功，订单处理中 FAILED 支付失败
        /// </summary>
        public string trade_status { get; set; }
    }
}
