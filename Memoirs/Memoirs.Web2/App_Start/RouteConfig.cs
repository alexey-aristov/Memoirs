using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Memoirs.Web2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Add(new Route("fonts/{file}", new FontsRouteHadler()));
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }


    }
    class FontsRouteHadler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var fileName = requestContext.RouteData.GetRequiredString("file");
            return new FontsHttpHandler(fileName);
        }
    }

    class FontsHttpHandler : IHttpHandler
    {
        private string _file;
        public FontsHttpHandler(string file)
        {
            _file = file;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.WriteFile(Path.Combine(context.Request.PhysicalApplicationPath, "node_modules/bootstrap/dist/fonts", _file));
        }

        public bool IsReusable => true;
    }
}
