namespace CatalogueNew.Web
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/jquery-ui-{version}.js",
            "~/Scripts/app.js",
            "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                //"~/Content/bootstrap.readable.min.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/catalogue.css",
                      "~/Content/star-rating.min.css",
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new ScriptBundle("~/bundles/sliderjs").Include(
                     "~/Scripts/jquery.easing.1.3.js",
                     "~/Scripts/jquery.fitvids.js",
                     "~/Scripts/jquery.bxslider.js"
                ));

            bundles.Add(new ScriptBundle("~/bundels/product").Include(
                    "~/Scripts/like-dislike.js",
                    "~/Scripts/appwishlist.js",
                    "~/Scripts/comments.js",
                    "~/Scripts/star-rating.min.js",
                    "~/Scripts/rating.js",
                    "~/Scripts/moment.min.js",
                    "~/Scripts/moment-with-locales.min.js",
                    "~/Scripts/jquery.scrollUp.min.js"));
        }
    }
}