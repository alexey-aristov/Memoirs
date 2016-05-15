using System.Web;
using System.Web.Mvc;
using Memoirs.Web.Filters;

namespace Memoirs.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}