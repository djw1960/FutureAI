using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Notify.PaySuccess
{
    public partial class WechatReturnMert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string returnUrl = System.Configuration.ConfigurationManager.AppSettings["returnUrl"];
            //1.0 获取返回信息，

            //2.0 主动查询订单状态
            //如何判断请求是属于自动跳转还是手动输入 如ref？
            var uri = Request.UrlReferrer;//避免手动直接访问站点
            if (uri != null)
            {
                Response.Redirect(returnUrl, true);
            }
            else
            {
                string parms = Request.QueryString.ToString();
                if (string.IsNullOrEmpty(parms))
                {
                    Response.Write("对不起，自动返回商户失败，请关闭当前页面手动返回商户站点");
                }
                else
                {
                    Response.Redirect(returnUrl, true);
                }
            }
        }
    }
}