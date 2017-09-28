using MultiTenancy.Infrastructure.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MultiTenancyPOC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MultiTenancyHelper.CarregarClientesCadastrados(this);

        }

        protected void Application_PreRequestHandlerExecute()
        {
            MultiTenancyHelper.CarregarConfiguracaoCliente(this);
        }        
    }
}

