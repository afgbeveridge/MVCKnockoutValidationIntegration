using System.Web.Optimization;

namespace MVCKnockoutValidationIntegration {

    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {

            bundles.Add(new ScriptBundle("~/bundles/example").Include(
                "~/Scripts/jquery-2.2.3.js",
                "~/Scripts/knockout-3.4.0.js",
                "~/Scripts/knockout.mapping-latest.js",
                "~/Scripts/knockout.validation.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/ValidationMetadataInterpreter.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/testingSupport").Include(
                "~/Scripts/test-support.js")
            );

            bundles.Add(new StyleBundle("~/css/bootstrap").Include("~/Content/bootstrap.css"));


        }
    }
}