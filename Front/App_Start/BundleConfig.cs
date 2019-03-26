using System.Web;
using System.Web.Optimization;

namespace Front
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/assets/js/modernizr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/assets/css/bootstrap.min.css",
                    "~/assets/css/icons.css",
                    "~/assets/css/style.css",
                    "~/Content/ocp-overrides.css",
                    "~/assets/plugins/sweet-alert/sweetalert2.min.css",
                    "~/assets/plugins/select2/css/select2.min.css",
                    "~/assets/plugins/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/templateJS").Include(
                            "~/assets/js/jquery.min.js",
                            "~/assets/js/popper.min.js",
                            "~/assets/js/bootstrap.min.js",
                            "~/assets/js/waves.js",
                            "~/assets/js/jquery.slimscroll.js",
                            "~/Scripts/canvasjs.min.js",
                            "~/assets/plugins/sweet-alert/sweetalert2.min.js",
                            "~/assets/pages/jquery.sweet-alert.init.js",
                            "~/assets/plugins/select2/js/select2.min.js",
                            "~/assets/plugins/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"
                        ));


            BundleTable.EnableOptimizations = true;
        }
    }
}
