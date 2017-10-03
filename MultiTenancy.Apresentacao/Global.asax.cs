using AutoMapper;
using MultiTenancy.Infraestrutura.Helpers;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

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
            if (!this.Request.Url.ToString().Contains("Acesso/AcessoNegado"))
            {
                try
                {
                    MultiTenancyHelper.CarregarConfiguracaoCliente();
                }
                catch (Exception ex)
                {

                    this.Response.RedirectToRoute("Default",
                        new RouteValueDictionary
                        {
                            { "area", "" },
                            { "controller", "Acesso" },
                            { "action", "AcessoNegado" },
                            { "mensagem", ex.Message }
                        });
                }
            }
        }

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }
}
