using EF.Entitys;
using Serv;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayService.Serv
{
    public class FutureControl
    {
        private static EF.IService.IServiceSession ibll = OperationContext.BLLSession;

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
            }
            //最长查询6个月内数据
            int dlimit = Convert.ToInt32(DateTime.Now.AddMonths(-6).ToString("yyyyMMdd"));
            if (!string.IsNullOrEmpty(param.Code))//按照品种获取的时候，必须传时间
            {
                exp = exp.And<FDataRepository>(s => s.CateCode == param.Code);
                //单个品种查询默认一个月数据
                int d = Convert.ToInt32(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));
                if (!string.IsNullOrEmpty(param.StartT))
                {
                    d = Convert.ToInt32(param.StartT);
                }
                d = d > dlimit ? d : dlimit;
                exp = exp.And<FDataRepository>(s => s.Date >= d);
            }
            else if (!string.IsNullOrEmpty(param.StartT))
            {
                int d = Convert.ToInt32(param.StartT);
                d = d > dlimit ? d : dlimit;
                exp = exp.And<FDataRepository>(s => s.Date == d);
            }
            else
            {
                int d = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                exp = exp.And<FDataRepository>(s => s.Date == d);
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

        #endregion
    }
}
