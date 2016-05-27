using System.Web;
using System.Web.Optimization;

namespace Memoirs.Web2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/node_modules/jquery/dist/jquery.js",
                "~/node_modules/bootstrap/dist/bootstrap.js",
                "~/node_modules/underscore/underscore.js",
                "~/node_modules/backbone/backbone.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/App/Main.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/node_modules/bootstrap/dist/css/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
