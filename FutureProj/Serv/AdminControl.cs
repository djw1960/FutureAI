using EF.Common;
using EF.Entitys;
using Serv;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv
{
    public class AdminControl
    {
        private static EF.IService.IServiceSession ibll = OperationContext.BLLSession;

        /// <summary>
        /// 2000 后台登录操作
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void Admin_SignIn(ReturnModel result,AdminParamsM param)
        {
            #region 参数验证
            if (string.IsNullOrEmpty(param.Account) || string.IsNullOrEmpty(param.Pwd))
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "参数错误";
                return;
            }
            string account = Base64Util.DecodeBase64(Encoding.UTF8, param.Account);
            string pwd = Base64Util.DecodeBase64(Encoding.UTF8, param.Pwd);
            if (account == param.Account || pwd == param.Pwd)
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "参数错误";
                return;
            }
            #endregion

            var model = ibll.View_AdminUser.Single(a => a.Login == account&&a.RoleCode==SysRuleType.manager.ToString());
            if (model != null && model.Pwd.ToLower() == MD5Encrypt.MD5(pwd, Encoding.UTF8))
            {
                //写入登录LoginSession
                int minute =Convert.ToInt32(ibll.FSys_Config.Single(a => a.SKey == SysConfigType.TokenTimeout.ToString()).SValue);
                FSys_LoginSession login = new FSys_LoginSession();
                login.UID = model.ID;
                login.Token = Common.BillToken();
                login.Source = Common.GetIP();
                login.TimeOut = DateTime.Now.AddMinutes(minute);
                login.UserType = SysRuleType.manager.ToString();
                ibll.FSys_LoginSession.Add(login);
                if (ibll.FSys_LoginSession.SaveChanges() > 0)
                {
                    result.data = new { token = login.Token, username = model.UserName, account = model.Login };
                    result.code = RespCodeConfig.Normal;
                }
                else
                {
                    result.code = RespCodeConfig.Faild;
                    result.msg = "网络错误，请稍后再试";
                    return;
                }
            }
            else
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "用户名或密码错误";
                return;
            }
        }
        /// <summary>
        /// 2010 审批资讯列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void Admin_NewsList(ReturnModel result, AdminParamsM param)
        {
            if (IsLogin(result, param.Token))
            {
                result.code = RespCodeConfig.Normal;
                result.msg = "登录状态正常";
            }
        }

        private static bool IsLogin(ReturnModel result,string token)
        {
            var tokensession=ibll.FSys_LoginSession.Single(a => a.Token == token);
            if (tokensession!=null)
            {
                var loginsession = ibll.FSys_LoginSession.where(a => a.UID == tokensession.UID).OrderByDescending(a => a.ID).Take(1).ToList();
                if (loginsession.Count() > 0)
                {
                    var model = loginsession[0];
                    if (model.Token.Equals(token) && model.TimeOut > DateTime.Now)
                    {
                        result.code = RespCodeConfig.Normal;
                        model.TimeOut = DateTime.Now.AddHours(8);
                        ibll.FSys_LoginSession.SaveChanges();
                        return true;
                    }
                    else
                    {
                        result.code = RespCodeConfig.Faild;
                        result.msg = "token失效";
                        return false;
                    }
                }
                else
                {
                    result.code = RespCodeConfig.Faild;
                    result.msg = "Login Error";
                    return false;
                }
            }
            else
            {
                result.code = RespCodeConfig.Faild;
                result.msg = "Login Error";
                return false;
            }
        }
    }
}
