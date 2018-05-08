using System.Web.Optimization;

namespace PhotoManager
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/mustache").Include(
                "~/Scripts/mustache.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment.js",
                "~/Scripts/moment-with-locales.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/photoManager")
                .IncludeDirectory("~/Scripts/App/Shared", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/Home/Index").Include(
                "~/Scripts/App/Home/Index.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Album/Add").Include(
                "~/Scripts/App/Album/Add.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Album/Edit").Include(
                "~/Scripts/App/Album/Edit.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Album/Index").Include(
                "~/Scripts/App/Album/Index.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Album/Manage").Include(
                "~/Scripts/App/Album/Manage.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Gallery/AdvancedSearch").Include(
                "~/Scripts/App/Gallery/AdvancedSearch.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Gallery/Index").Include(
                "~/Scripts/App/Gallery/Index.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Gallery/Search").Include(
                "~/Scripts/App/Gallery/Search.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Photo/Add").Include(
                "~/Scripts/App/Photo/Add.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Photo/Edit").Include(
                "~/Scripts/App/Photo/Edit.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Photo/Index").Include(
                "~/Scripts/App/Photo/Index.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker-scripts").Include(
                "~/Scripts/bootstrap-datepicker.min.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/PhotoManager.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap-datepicker")
                .IncludeDirectory("~/Content/bootstrap-datepicker", "*.css", true));
            bundles.Add(new StyleBundle("~/bundles/bootbox").Include(
                "~/Scripts/bootbox.min.js"));
        }
    }
}