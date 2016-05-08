using System.Web;
using System.Web.Optimization;

namespace Memoirs.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/common").Include(
                "~/node_modules/jquery/dist/jquery.js",
                "~/node_modules/bootstrap/dist/bootstrap.js",
                "~/node_modules/react/dist/react.js",
                "~/node_modules/react-dom/dist/react-dom.js"));
            bundles.Add(new StyleBundle("~/bundles/style/main").Include(
                "~/node_modules/bootstrap/dist/css/bootstrap.css",
                "~/Content/site.css"));
        }
    }
}
