namespace CatalogueNew.Web
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/lib/jquery-{version}.js",
            "~/Scripts/lib/jquery-ui-{version}.js",
            "~/Scripts/app.js",
            "~/Scripts/lib/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/input.js").Include(
                        "~/Scripts/lib/jquery.validate*",
                        "~/Scripts/lib/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/validator").Include(
                        "~/Scripts/validator.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/lib/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/catalogue.css",
                      "~/Content/star-rating.min.css",
                      "~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/bundles/dropzone.css").Include(
                      "~/Scripts/lib/dropzone/css/basic.css",
                      "~/Scripts/lib/dropzone/css/dropzone.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/lib/dropzone/dropzone.js"));

            bundles.Add(new ScriptBundle("~/bundles/sliderjs").Include(
                     "~/Scripts/lib/jquery.easing.1.3.js",
                     "~/Scripts/lib/jquery.fitvids.js",
                     "~/Scripts/lib/jquery.bxslider.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/product").Include(
                    "~/Scripts/tag.js",
                    "~/Scripts/like-dislike.js",
                    "~/Scripts/comments.js",
                    "~/Scripts/star-rating.min.js",
                    "~/Scripts/rating.js",
                    "~/Scripts/wishlist.js",
                    "~/Scripts/lib/moment.min.js",
                    "~/Scripts/lib/moment-with-locales.min.js",
                    "~/Scripts/lib/jquery.scrollUp.min.js",
                    "~/Scripts/scrollUp.js"));

            bundles.Add(new ScriptBundle("~/bundles/editUser").Include(
                "~/Scripts/checkboxes.js",
                "~/Scripts/radio-buttons.js"));

            bundles.Add(new ScriptBundle("~/bundles/images.js").Include(
                "~/Scripts/imagesUploadDelete.js"));
        }
    }
}