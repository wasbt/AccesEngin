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
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

   
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/assets/plugins/morris/morris.css",
                     "~/assets/css/bootstrap.min.css",
                     "~/assets/css/core.css",
                     "~/assets/css/components.css",
                     "~/assets/css/icons.css",
                     "~/assets/css/pages.css",
                     "~/assets/css/menu.css",
                     "~/assets/css/responsive.css",
                     "~/assets/plugins/bootstrap-sweetalert/sweet-alert.css",
                     "~/Content/bootstrapdatetimepicker.min.css",
                     "~/Content/bootstrap-tagsinput.css",
                     "~/Content/ghse-overrides.css"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/timejs").Include(
                     "~/Scripts/bootstrapdatetimepicker.js",
                      "~/Scripts/bootstrapdatetimepicker.fr.js"
                  ));
            bundles.Add(new ScriptBundle("~/bundles/Alljavascript").Include(
                            "~/assets/js/detect.js",
                            "~/assets/js/fastclick.js",
                            "~/assets/js/jquery.slimscroll.js",
                            "~/assets/js/jquery.blockUI.js",
                            "~/assets/js/waves.js",
                            "~/assets/js/wow.min.js",
                            "~/assets/js/jquery.nicescroll.js",
                            "~/assets/js/jquery.scrollTo.min.js",
                            "~/assets/plugins/bootstrap-sweetalert/sweet-alert.min.js",
                            "~/assets/plugins/jquery-knob/jquery.knob.js",
                            "~/assets/plugins/morris/morris.min.js",
                            "~/assets/plugins/raphael/raphael-min.js",
                            "~/assets/pages/jquery.dashboard.js",
                            "~/assets/plugins/select2/dist/js/select2.min.js"

                        ));
            bundles.Add(new StyleBundle("~/Content/style").Include(
                    "~/assets/plugins/select2/dist/css/select2.css",
                    "~/assets/plugins/select2/dist/css/select2-bootstrap.css"
                    ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
