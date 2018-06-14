﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{action}/{id}",
                defaults: new { controller= "ServiceApi", id = RouteParameter.Optional }
            );
        }
    }
}
