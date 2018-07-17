using EF.Common;
using EF.Entitys;
using Newtonsoft.Json;
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

        private static bool IsLogin(ReturnModel result, string token)
        {
            var tokensession = ibll.FSys_LoginSession.Single(a => a.Token == token);
            if (tokensession != null)
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
                        result.code = RespCodeConfig.TokenTimeOut;
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
        /// 2002 登录获取菜单列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void Admin_GetMenuList(ReturnModel result, AdminParamsM param)
        {
            if (IsLogin(result, param.Token))
            {
                var menulist= ibll.FMenus.where(s =>s.IsAvailable&&s.Type=="2").OrderBy(s=>s.OrderBy).Select(ss=>new {
                    ss.MenuTitle,
                    ss.MenuIcon,
                    ss.MenuCode,
                    ss.MenuType,
                    ss.Mode,
                    ss.Url,
                    ss.OrderBy
                }).ToList();
                result.code = RespCodeConfig.Normal;
                result.data = menulist;
            }
        }
        /// <summary>
        /// 2003 登录获取菜单列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void Admin_GetCategoryList(ReturnModel result, AdminParamsM param)
        {
            if (IsLogin(result, param.Token))
            {
                var catelist = ibll.FSys_Category.where(s => s.IsAvailable && !s.IsDel &&s.Type==param.Cate).Select(ss => new {
                    ss.ID,
                    ss.CateKey,
                    ss.CateValue
                }).ToList();
                result.code = RespCodeConfig.Normal;
                result.data = catelist;
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
        /// <summary>
        /// 2060 获取预测列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void Admin_AIList(ReturnModel result, AdminParamsM param)
        {
            if (IsLogin(result, param.Token))
            {
                var exp = PredicateBuilder.True<FAI>();
                switch (param.Cate)
                {
                    case "n"://new
                             //获取今天的数据
                        {
                            exp = exp.And<FAI>(s => s.DT >= DateTime.Today);
                        }
                        break;
                    case "s"://someone
                             //查询某一天的数据
                        if (!string.IsNullOrEmpty(param.StartT))
                        {
                            var dt = Convert.ToDateTime(param.StartT);
                            exp = exp.And<FAI>(s => s.DT >= dt);
                        }
                        else
                        {
                            result.msg = "参数错误";
                            result.code = RespCodeConfig.ArgumentExp;
                            return;
                        }
                        break;
                    case "m"://month
                        if (param.Number > 0)
                        {
                            var dt = DateTime.Now.AddMonths(0 - param.Number);
                            exp = exp.And<FAI>(s => s.DT >= dt);
                        }
                        else
                        {
                            result.msg = "参数错误";
                            result.code = RespCodeConfig.ArgumentExp;
                            return;
                        }
                        break;
                    default:
                        {
                            result.msg = "参数错误";
                            result.code = RespCodeConfig.ArgumentExp;
                            return;
                        }
                        break;
                }
                var list = ibll.FAI.where(exp).OrderByDescending(s => s.DT).ToList();
                result.code = RespCodeConfig.Normal;
                result.data = list;
            }

        }
        /// <summary>
        /// 2061 获取某条预测详情
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void Admin_AIItemInfo(ReturnModel result, AdminParamsM param)
        {
            if (IsLogin(result, param.Token))
            {
                var model = ibll.FAI.Single(a => a.ID == param.ID);
                if (model==null)
                {
                    result.code = RespCodeConfig.ServerError;
                    result.msg = "数据不存在";
                    return;
                }
                result.code = RespCodeConfig.Normal;
                result.data = model;
            }
        }
        /// <summary>
        /// 2062 新增一条预测记录
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void Admin_AIItemAdd(ReturnModel result, AdminParamsM param)
        {
            if (IsLogin(result, param.Token))
            {
                if (string.IsNullOrEmpty(param.Content))
                {
                    result.code = RespCodeConfig.ServerError;
                    result.msg = "内容为空";
                    return;
                }
                if (param.Type == "edit")
                {
                    //编辑
                    FAI model = JsonConvert.DeserializeObject<FAI>(param.Content);
                    FAI obj = ibll.FAI.Single(a => a.ID == model.ID);
                    if (obj!=null)
                    {
                        obj.Cate = model.Cate;
                        obj.CateType = model.CateType;
                        obj.Status = model.Status;
                        obj.TurnType = model.TurnType;
                        obj.DT = model.DT;
                        obj.NPrice = model.NPrice;
                        obj.Remark = model.Remark;
                        obj.ResultClose = model.ResultClose;
                        obj.ResultLow = model.ResultLow;
                        obj.ResultHight = model.ResultHight;
                        obj.IsPublish = model.IsPublish;
                        ibll.FAI.Update(obj, new string[] {
                            "Cate", "CateType", "Status", "TurnType","IsPublish",
                            "DT","NPrice","Remark","ResultClose","ResultLow","ResultHight"});
                        int n=ibll.SaveChanges();
                        if (n > 0)
                        {
                            result.code = RespCodeConfig.Normal;
                            result.data = "保存成功";
                        }
                        else
                        {
                            result.code = RespCodeConfig.ServerError;
                            result.data = "保存失败";
                        }
                    }
                }
                else if(param.Type=="add")
                {
                    //新增
                    FAI model = JsonConvert.DeserializeObject<FAI>(param.Content);
                    if (model.AValue > 0 && model.Star > 0 && model.NPrice > 0)
                    {
                        model.ReviseLV = (model.AValue * 100) / model.NPrice;
                        model.ReviseStar = model.ReviseLV * model.Star;
                        model.IsPublish = false;
                        model.Status = 0;
                        model.AddDate = DateTime.Now;
                        ibll.FAI.Add(model);
                        int n = ibll.SaveChanges();
                        if (n > 0)
                        {
                            result.code = RespCodeConfig.Normal;
                            result.data = "保存成功";
                        }
                        else
                        {
                            result.code = RespCodeConfig.ServerError;
                            result.data = "保存失败";
                        }
                    }
                    else
                    {
                        result.code = RespCodeConfig.ArgumentExp;
                        result.data = "数据有误";
                    }
                }
            }
        }
    }
}
