using EF.Entitys;
using Serv;
using Serv.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Future.Serv
{
    public class SXControl
    {
        private static EF.IService.IServiceSession ibll = OperationContext.BLLSession;

        public static void SERVICE_GetCateList(ReturnModel result, RequestParamsM param)
        {
            var list = ibll.SX_SiteConfig.where(s=>true).OrderBy(s=>s.Type).ToList();

            result.code = RespCodeConfig.Normal;
            result.data = list.Select(s => new {
                s.ID,
                s.SiteName,
                s.Type
            });
        }
        public static void SERVICE_GetSX_NewsList(ReturnModel result, RequestParamsM param)
        {
            PageContent page = new PageContent();
            page.index = param.page;
            page.size = 50;
            var p = PredicateBuilder.True<SX_News>();
            if (!string.IsNullOrEmpty(param.Type))
            {
                page.size = 20;
                p = p.And(s => s.SiteName.Contains(param.Type));//根据分类获取某个交易所的信息
            }
            
            var list = ibll.SX_News.where(p).OrderByDescending(ss => ss.AddDate).Skip(page.index * page.size).Take(page.size).ToList();

            result.code = RespCodeConfig.Normal;
            result.data = new ListContent() { list = list.Select(s => new
            {
                ID = s.ID,
                SiteName = s.SiteName,
                Title = s.Title,
                Url =s.Url.Contains("http")?s.Url:s.Site + s.Url,
                DT = s.AddDate.HasValue ? s.AddDate.Value.ToString("yyyy-MM-dd") : ""
            }
                ), page = page };
        }
    }
}
