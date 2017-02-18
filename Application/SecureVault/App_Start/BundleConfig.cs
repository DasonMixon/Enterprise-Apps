using System.Web;
using System.Web.Optimization;

namespace SecureVault
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/sb").Include(
                      "~/Content/SB/bootstrap.min.css",
                      "~/Content/SB/metisMenu.min.css",
                      "~/Content/SB/sb-admin-2.css",
                      "~/Content/SB/morris.css",
                      "~/Content/SB/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/sb").Include(
                "~/Scripts/SB/jquery.min.js",
                "~/Scripts/SB/bootstrap.min.js",
                "~/Scripts/SB/metisMenu.min.js",
                "~/Scripts/SB/raphael.min.js",
                "~/Scripts/SB/morris.min.js",
                "~/Scripts/SB/morris-data.js",
                "~/Scripts/SB/sb-admin-2.js"));
        }
    }
}
