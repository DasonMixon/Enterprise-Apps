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

            /*< !--Bootstrap Core CSS -->
            < link href = "../vendor/bootstrap/css/bootstrap.min.css" rel = "stylesheet" >
   
            < !--MetisMenu CSS-- >
            < link href = "../vendor/metisMenu/metisMenu.min.css" rel = "stylesheet" >
      
            < !--Custom CSS-- >
            < link href = "../dist/css/sb-admin-2.css" rel = "stylesheet" >
         
            < !--Morris Charts CSS -->
            < link href = "../vendor/morrisjs/morris.css" rel = "stylesheet" >
            
            < !--Custom Fonts-- >
            < link href = "../vendor/font-awesome/css/font-awesome.min.css" rel = "stylesheet" type = "text/css" >*/


            bundles.Add(new StyleBundle("~/Content/sb").Include(
                      "~/SB/vendor/bootstrap/css/bootstrap.min.css",
                      "~/SB/vendor/metisMenu/metisMenu.min.css",
                      "~/SB/dist/css/sb-admin-2.css",
                      "~/SB/dist/css/sb-admin-2.min.css",
                      "~/SB/vendor/morrisjs/morris.css",
                      "~/SB/vendor/font-awesome/css/font-awesome.min.css"));

            /*< !--jQuery-- >
            < script src = "../vendor/jquery/jquery.min.js" ></ script >

            < !--Bootstrap Core JavaScript -->
            < script src = "../vendor/bootstrap/js/bootstrap.min.js" ></ script >

            < !--Metis Menu Plugin JavaScript-- >
            < script src = "../vendor/metisMenu/metisMenu.min.js" ></ script >

            < !--Morris Charts JavaScript -->
            < script src = "../vendor/raphael/raphael.min.js" ></ script >
            < script src = "../vendor/morrisjs/morris.min.js" ></ script >
            < script src = "../data/morris-data.js" ></ script >

            < !--Custom Theme JavaScript -->
            < script src = "../dist/js/sb-admin-2.js" ></ script >*/

            bundles.Add(new ScriptBundle("~/bundles/sb").Include(
                "~/SB/vendor/jquery/jquery.min.js",
                "~/SB/vendor/bootstrap/js/bootstrap.min.js",
                "~/SB/vendor/metisMenu/metisMenu.min.js",
                "~/SB/vendor/raphael/raphael.min.js",
                "~/SB/vendor/morrisjs/morris.min.js",
                "~/SB/data/morris-data.js",
                "~/SB/dist/js/sb-admin-2.js",
                "~/SB/data/js/sb-admin-2.min.js"));
        }
    }
}
