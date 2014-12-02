namespace CatalogueNew.Web
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/Site.css",
                      "~/Content/catalogue.css"));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery-1.9.0.min.js",
                      "~/Scripts/jquery-1.9.0.min.map"));
            bundles.Add(new ScriptBundle("~/Scripts/jquery.validate").Include(
                      "~/Scripts/jquery.validate.min.js"));
        }
    }
}