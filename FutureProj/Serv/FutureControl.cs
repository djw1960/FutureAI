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
        /// <summary>
        /// 1010获取资讯列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        public static void SERVICE_GetNewsList(ReturnModel result,RequestParamsM param)
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
            result.data = new ListContent() { list=list, page=page };
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
    }
}
