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
    public class FutureControl
    {
        private static EF.IService.IServiceSession ibll = OperationContext.BLLSession;

        #region 用户基础模块
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_SignIn(ReturnModel result, RequestParamsM param)
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

            var model=ibll.FSysUser.Single(a => a.Login == account);
            if (model!=null&&model.Pwd.ToLower() == MD5Encrypt.MD5(pwd, Encoding.UTF8))
            {
                //写入登录session

                result.data = new {token= Common.BillToken(),username=model.UserName,account=model.Login };
                result.code = RespCodeConfig.Normal;
            }
            else
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "用户名或密码错误";
                return;
            }
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_SignUp(ReturnModel result, RequestParamsM param)
        {
            #region 参数验证

            if (string.IsNullOrEmpty(param.Account.Trim()) || string.IsNullOrEmpty(param.Pwd.Trim()) ||string.IsNullOrEmpty(param.UserName.Trim())||string.IsNullOrEmpty(param.Content))
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "参数不能为空";
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


            //验证邀请码
            var invitemodel = ibll.FSysUser_Invite.Single(a => a.InviteCode == param.Content);
            if (invitemodel != null)
            {
                var model = ibll.FSysUser.Single(a => a.Login == account);
                if (model == null)
                {
                    //注册新账号
                    FSysUser user = new FSysUser();
                    user.Login = account;
                    user.Pwd = MD5Encrypt.MD5(pwd, Encoding.UTF8);
                    user.UserName = param.UserName;
                    user.Remark = "";
                    user.RoleID = (int)SysRuleType.view;
                    user.IsAvailable = 1;
                    user.AddDate = DateTime.Now;
                    ibll.FSysUser.Add(user);
                    int n = ibll.FSysUser.SaveChanges();
                    if (n > 0)
                    {
                        result.msg = "注册成功";
                        result.code = RespCodeConfig.Normal;
                    }
                    else
                    {
                        result.code = RespCodeConfig.ArgumentExp;
                        result.msg = "网络错误";
                        return;
                    }
                }
                else
                {
                    result.code = RespCodeConfig.ArgumentExp;
                    result.msg = "该账号已被注册";
                    return;
                }
            }
            else
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "无效的邀请码";
                return;
            }
            
        }
        /// <summary>
        /// 1002 获取菜单列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetMenuList(ReturnModel result, RequestParamsM param)
        {
            //获取主菜单列表
        }
        #endregion

        #region 资讯模块
        /// <summary>
        /// 1010获取资讯列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetNewsList(ReturnModel result, RequestParamsM param)
        {
            var p = PredicateBuilder.True<FNews>();
            if (!string.IsNullOrEmpty(param.Type))
            {
                p = p.And(s => s.TradeHouse == param.Type);//根据分类获取某个交易所的信息
            }
            PageContent page = new PageContent();
            page.index = param.page;
            page.size = 10;
            var list = ibll.FNews.where(p.And(ss => !ss.IsDel && ss.IsPublish)).OrderByDescending(ss => ss.AddDate).Skip(page.index * page.size).Take(page.size).Select(
                s => new
                {
                    s.ID,
                    s.NSource,
                    s.NewsType,
                    s.NewsTitle,
                    s.PublishDate
                }
                ).ToList();
            result.code = RespCodeConfig.Normal;
            result.data = new ListContent() { list = list, page = page };
        }
        /// <summary>
        /// 1010获取新闻详情
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetNewsInfo(ReturnModel result, RequestParamsM param)
        {
            if (string.IsNullOrEmpty(param.ID))
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "ArgumentExp";
                return;
            }
            int id = Convert.ToInt32(param.ID);
            var model = ibll.FNews.Single(s => s.ID == id);
            if (model != null)
            {
                result.code = RespCodeConfig.Normal;
                result.data = new { model.ID, model.NewsTitle, model.NewsType, model.NSource, model.PublishDate, model.NewContent };
            }
            else
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "No News";
                return;
            }
        }
        #endregion

        #region 仓单模块
        /// <summary>
        /// 1020-获取仓单信息
        /// 1-每日仓单信息
        /// 2-某商品最近历史仓单信息，最长6个月内，超过6个月历史数据提需求申请
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetReposList(ReturnModel result, RequestParamsM param)
        {
            var exp= PredicateBuilder.True<FDataRepository>();
            if (!string.IsNullOrEmpty(param.Type))
            {
                //查询某交易所当天的数据
                exp = exp.And<FDataRepository>(s => s.TradeHouse == param.Type);
            }
            else
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "参数错误";
                return;
            }
            //最长查询6个月内数据
            int dlimit = Convert.ToInt32(DateTime.Now.AddMonths(-36).ToString("yyyyMMdd"));
            switch (param.Cate)
            {
                case "n"://new
                    //获取最近一期的数据
                    {
                        int maxDateTime = ibll.FDataRepository.where(a => true).Max(a => a.Date);
                        exp = exp.And<FDataRepository>(s => s.Date == maxDateTime);
                        var toplist = ibll.FDataRepository.where(exp).OrderBy(a => a.CateCode).Select(ss => new {
                            ss.Date,
                            ss.CateName,
                            ss.CateCode,
                            ss.YTDSum,
                            ss.TDSum,
                            ss.Change
                        }).ToList();
                        result.data =new {Date=maxDateTime,list=toplist };
                        result.code = RespCodeConfig.Normal;
                        return;
                    }
                    break;
                case "s"://someone
                    //查询某一天的数据
                    if (!string.IsNullOrEmpty(param.StartT))
                    {
                        int d = Convert.ToInt32(param.StartT);
                        d = d > dlimit ? d : dlimit;
                        exp = exp.And<FDataRepository>(s => s.Date == d);
                        var toplist = ibll.FDataRepository.where(exp).OrderBy(a => a.CateCode).Select(ss => new {
                            ss.Date,
                            ss.CateName,
                            ss.CateCode,
                            ss.YTDSum,
                            ss.TDSum,
                            ss.Change
                        }).ToList();
                        result.data = new { Date = d, list = toplist };
                        result.code = RespCodeConfig.Normal;
                        return;
                    }
                    else
                    {
                        result.msg = "参数错误";
                        result.code = RespCodeConfig.ArgumentExp;
                        return;
                    }
                    break;
                case "l"://list
                    //查询某一个品种的
                    if (!string.IsNullOrEmpty(param.Code)&& !string.IsNullOrEmpty(param.StartT))//按照品种获取的时候，必须传时间
                    {
                        exp = exp.And<FDataRepository>(s => s.CateCode == param.Code);
                        int d = Convert.ToInt32(param.StartT);
                        d = d > dlimit ? d : dlimit;
                        exp = exp.And<FDataRepository>(s => s.Date >= d);
                    }
                    else
                    {
                        result.msg = "参数错误";
                        result.code = RespCodeConfig.ArgumentExp;
                        return;
                    }
                    break;
                case "m"://month
                    //按月份查询某品种
                    if (!string.IsNullOrEmpty(param.Code) && param.Number > 0)//按照品种获取的时候，必须传时间
                    {
                        exp = exp.And<FDataRepository>(s => s.CateCode == param.Code);
                        //单个品种查询默认一个月数据
                        int d = Convert.ToInt32(DateTime.Now.AddMonths(0- param.Number).ToString("yyyyMMdd"));
                        d = d > dlimit ? d : dlimit;
                        exp = exp.And<FDataRepository>(s => s.Date >= d);
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
            var list = ibll.FDataRepository.where(exp).OrderBy(s => s.Date).Select(ss=>new {
                ss.Date,
                ss.CateName,
                ss.CateCode,
                ss.YTDSum,
                ss.TDSum,
                ss.Change
            }).ToList();

            result.code = RespCodeConfig.Normal;
            result.data = list;

        }
        #endregion

        #region 统计局数据模块
        /// <summary>
        /// 1030-获取统计局信息
        /// 1-获取某一期的统计信息
        /// 2-获取某品种历史统计信息，最长6个月，超过6个月历史数据提需求申请
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetMaterialList(ReturnModel result, RequestParamsM param)
        {
            var exp = PredicateBuilder.True<FDataMaterial>();
            //最长查询12个月内数据
            int dlimit = Convert.ToInt32(DateTime.Now.AddMonths(-36).ToString("yyyyMMdd"));
            switch (param.Cate)
            {
                case "n"://new
                    //获取最新一期的数据
                    {
                        int maxDateTime = ibll.FDataMaterial.where(a => true).Max(a => a.DateTime);
                        var toplist = ibll.FDataMaterial.where(a => a.DateTime == maxDateTime).OrderBy(a => a.PCode);
                        result.code = RespCodeConfig.Normal;
                        result.data = new { Date = maxDateTime, list = toplist };
                        return;
                    }
                    break;
                case "s"://someone
                    //单个品种查询默认一个月数据
                    {
                        int d = 0;
                        if (!string.IsNullOrEmpty(param.StartT))
                        {
                            d = Convert.ToInt32(param.StartT);
                        }
                        else
                        {
                            result.code = RespCodeConfig.ArgumentExp;
                            result.msg = "参数错误";
                            return;
                        }
                        var someList = ibll.FDataMaterial.where(a => a.DateTime == d).OrderBy(a => a.PCode);
                        result.code = RespCodeConfig.Normal;
                        result.data = new { Date = d, list = someList };
                        return;
                    }
                    break;
                case "l"://list
                    if (!string.IsNullOrEmpty(param.Code)&&!string.IsNullOrEmpty(param.StartT))//按照品种获取的时候，必须传时间
                    {
                        exp = exp.And<FDataMaterial>(s => s.PCode == param.Code);
                        int d = Convert.ToInt32(param.StartT);
                        d = d > dlimit ? d : dlimit;
                        exp = exp.And<FDataMaterial>(s => s.DateTime >= d);
                    }
                    else
                    {
                        result.code = RespCodeConfig.ArgumentExp;
                        result.msg = "参数错误";
                        return;
                    }
                    break;
                case "m"://month
                    if (!string.IsNullOrEmpty(param.Code) && param.Number > 0)//按照品种获取的时候，必须传时间
                    {
                        exp = exp.And<FDataMaterial>(s => s.PCode == param.Code);
                        int d = Convert.ToInt32(DateTime.Now.AddMonths(0 - param.Number).ToString("yyyyMMdd"));
                        d = d > dlimit ? d : dlimit;
                        exp = exp.And<FDataMaterial>(s => s.DateTime >= d);
                    }
                    else
                    {
                        result.code = RespCodeConfig.ArgumentExp;
                        result.msg = "参数错误";
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
            var list = ibll.FDataMaterial.where(exp).OrderBy(s=>s.DateTime).ToList();
            result.data = list;
            result.code = RespCodeConfig.Normal;
        }
        /// <summary>
        /// 1031 两个品种的数据对比
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetMaterList_TwoCate(ReturnModel result, RequestParamsM param)
        {
            var exp = PredicateBuilder.True<FDataMaterial>();
            //最长查询12个月内数据
            int dlimit = Convert.ToInt32(DateTime.Now.AddMonths(-36).ToString("yyyyMMdd"));
            string[] codes = param.Code.Split(new char[] { '|',',' }, StringSplitOptions.RemoveEmptyEntries);
            if (codes.Length != 2)
            {
                result.code = RespCodeConfig.ArgumentExp;
                result.msg = "只允许两个品种对比";
                return;
            }
            switch (param.Cate)
            {
                case "m"://两个品种对比只能按月份取数据
                    if (!string.IsNullOrEmpty(param.Code.Trim()) && param.Number > 0)
                    {
                        exp = exp.And<FDataMaterial>(s => codes.Contains(s.PCode));
                        int d = Convert.ToInt32(DateTime.Now.AddMonths(0 - param.Number).ToString("yyyyMMdd"));
                        d = d > dlimit ? d : dlimit;
                        exp = exp.And<FDataMaterial>(s => s.DateTime >= d);
                    }
                    else
                    {
                        result.code = RespCodeConfig.ArgumentExp;
                        result.msg = "参数错误";
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
            var list = ibll.FDataMaterial.where(exp).OrderBy(s => s.DateTime).ToList();
            result.data = new { list1=list.Where(a=>a.PCode==codes[0]).ToList(),list2= list.Where(a => a.PCode == codes[1]).ToList() };
            result.code = RespCodeConfig.Normal;
        }
        #endregion

        #region 固定资产数据版块

        #endregion

        #region 持仓版块

        #endregion

        #region 朋友圈版块
        /// <summary>
        /// 1040 获取朋友圈帖子列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetMomentList(ReturnModel result, RequestParamsM param)
        {

        }
        /// <summary>
        /// 1041 获取帖子详情和评论
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetMomentDetail(ReturnModel result, RequestParamsM param)
        {

        }
        #endregion

        #region AI版块
        /// <summary>
        /// 1060 获取AI预测列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetAIList(ReturnModel result, RequestParamsM param)
        {
            var exp= PredicateBuilder.True<FAI>();
            exp = exp.And<FAI>(s => s.IsPublish);
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
                        exp = exp.And<FAI>(s => s.DT>= dt);
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
                        exp = exp.And<FAI> (s =>s.DT>=dt&&s.DT<DateTime.Today);
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
            var list = ibll.FAI.where(exp).OrderByDescending(s => s.DT).ToList().Select(ss => new {
                ss.ID,
                ss.DT,
                ss.Cate,
                ss.DataType,
                ss.TurnType,
                Star=ss.Star>85?3:ss.Star>75?2:1,
                ss.NPrice,
                ss.AddDate,
                ss.Status
            });
            result.code = RespCodeConfig.Normal;
            result.data = list;

        }
        #endregion

        #region 会员版块

        #endregion
    }
}
