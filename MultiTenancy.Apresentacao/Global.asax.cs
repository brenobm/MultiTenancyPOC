using AutoMapper;
using MultiTenancy.Infraestrutura.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MultiTenancy.Apresentacao
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg =>
            {
                cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                cfg.CreateMissingTypeMaps = true;
            });


            MultiTenancyHelper multiTenancy = DependencyResolver.Current.GetService<MultiTenancyHelper>();

            multiTenancy.CarregarClientesCadastrados();
        }

        protected void Application_PreRequestHandlerExecute()
        {
            MultiTenancyHelper.CarregarConfiguracaoCliente();
        }
    }
}
