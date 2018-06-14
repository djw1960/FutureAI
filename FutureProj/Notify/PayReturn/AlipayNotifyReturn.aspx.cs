using Notify.PayHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Notify.PayReturn
{
    public partial class AlipayNotifyReturn : System.Web.UI.Page
    {
        private static AlipayReturnHelper payReturnHelper = new AlipayReturnHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            //payReturnHelper.DealWithPayReturn();
        }
    }
}