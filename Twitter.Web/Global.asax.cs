using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Twitter.Web.App_Start;
using Mappings;
using AutoMapper;

namespace Twitter.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEnginesConfig.RegisterViewEngines(ViewEngines.Engines);

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute();
        }
    }
}
