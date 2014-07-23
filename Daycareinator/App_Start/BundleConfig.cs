using System.Web;
using System.Web.Optimization;

namespace Daycareinator
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

           
            bundles.Add(new ScriptBundle("~/bundles/third-party").Include(
                "~/Content/bootstrap/js/bootstrap.js",
                "~/Content/bootstrap/js/bootstrap-datepicker.js",
                "~/Scripts/jquery.dataTables.js",
                "~/Scripts/knockout-2.2.0.debug.js",
                "~/Scripts/knockout.mapping-latest.debug.js",
                "~/Scripts/knockout.validation.js",
                "~/Scripts/knockout-datatables.js",
                "~/Scripts/numeral.js",
                "~/Scripts/toastr.js",
                "~/Scripts/alertify.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/jquery.maskedinput-1.3.1.js",
                "~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/marketing").Include(
                "~/Scripts/jquery.form.js",
                "~/Scripts/toastr.js",
                "~/Scripts/App/notification.js"
                ));
            /*bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/App/notifications.js",
                "~/Scripts/App/daycareinator.add-user.js",
                "~/Scripts/App/daycareinator.forgot-password"));*/
            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory("~/Scripts/app", "*.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/marketing/css/bundles").Include(
                "~/Content/marketing/css/base.css",
                "~/Content/marketing/css/grid.css",
                "~/Content/marketing/css/layout.css",
                "~/Content/marketing/css/colors/blue.css",
                "~/Content/shared/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/css/bundles").Include(
                "~/Content/bootstrap/css/bootstrap.css",
                "~/Content/bootstrap/css/theme.css",
                "~/Content/bootstrap/css/font-awesome.css",
                "~/Content/shared/toastr.css",
                "~/Content/bootstrap/css/alertify.css",
                "~/Content/bootstrap/css/datepicker.css",
                "~/Content/bootstrap/css/daycareinator.css"));

            /* bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                         "~/Content/themes/base/jquery.ui.core.css",
                         "~/Content/themes/base/jquery.ui.resizable.css",
                         "~/Content/themes/base/jquery.ui.selectable.css",
                         "~/Content/themes/base/jquery.ui.accordion.css",
                         "~/Content/themes/base/jquery.ui.autocomplete.css",
                         "~/Content/themes/base/jquery.ui.button.css",
                         "~/Content/themes/base/jquery.ui.dialog.css",
                         "~/Content/themes/base/jquery.ui.slider.css",
                         "~/Content/themes/base/jquery.ui.tabs.css",
                         "~/Content/themes/base/jquery.ui.datepicker.css",
                         "~/Content/themes/base/jquery.ui.progressbar.css",
                         "~/Content/themes/base/jquery.ui.theme.css"));*/
        }
    }
}