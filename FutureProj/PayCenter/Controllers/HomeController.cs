using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayCenter.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string id)
        {
            //1.0 根据ID获取订单信息
            //2.0 打开注资收银台
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error");
            }
            ViewBag.ID = id;
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}