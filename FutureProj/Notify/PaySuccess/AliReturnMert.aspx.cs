using EF.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Notify.PaySuccess
{
    public partial class AliReturnMert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            var forms = request.Form.ToString();
            if (!string.IsNullOrEmpty(forms))
            {
                LogHelper.Info<Return>("同步返回信息Forms：{0}", forms);
            }
            var qs = request.QueryString.ToString();
            if (!string.IsNullOrEmpty(qs))
            {
                LogHelper.Info<Return>("同步返回信息qs：{0}", qs);
            }
            StreamReader sr = new StreamReader(request.InputStream);
            string streamParams = System.Web.HttpUtility.UrlDecode(sr.ReadToEnd(), System.Text.Encoding.UTF8);
            if (!string.IsNullOrEmpty(streamParams))
            {
                LogHelper.Info<Return>("同步返回信息streamParams：{0}", streamParams);
            }
            Response.Write("对不起，自动返回商户失败，请关闭当前页面手动返回商户站点");
        }
    }
}