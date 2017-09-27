using MultiTenancyPOC.Models;
using MultiTenancyPOC.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            CarregarClientesCadastrados();

        }

        protected void Application_PreRequestHandlerExecute()
        {
            Uri url = HttpContext.Current.Request.Url;

            string host = GetSubdomain(url.DnsSafeHost.ToLower(), "mibh-0027.bh.mi");

            if (!string.IsNullOrEmpty(host))
            {
                Session["domain"] = host;
            }
            else
            {
                Session["domain"] = null;
            }
        }

        private void CarregarClientesCadastrados()
        {
            MasterContext masterContext = new MasterContext();

            Dictionary<string, Dictionary<string, object>> configClientes = new Dictionary<string, Dictionary<string, object>>();

            foreach(Cliente cliente in masterContext.Clientes.ToList())
            {
                Dictionary<string, object> configs = new Dictionary<string, object>();

                configs.Add("Codigo", cliente.Codigo);
                configs.Add("Nome", cliente.Nome);
                configs.Add("StringConexaoBanco", cliente.StringConexaoBanco);
                configs.Add("Icone", cliente.Icone);

                configClientes.Add(cliente.Dominio, configs);
            }

            Application["ClienteConfigs"] = configClientes;
        }

        private string GetSubdomain(string url, string domain = null)
        {
            var subdomain = url;
            if (subdomain != null)
            {
                if (domain == null)
                {
                    // Since we were not provided with a known domain, assume that second-to-last period divides the subdomain from the domain.
                    var nodes = url.Split('.');
                    var lastNodeIndex = nodes.Length - 1;
                    if (lastNodeIndex > 0)
                        domain = nodes[lastNodeIndex - 1] + "." + nodes[lastNodeIndex];
                }

                // Verify that what we think is the domain is truly the ending of the hostname... otherwise we're hooped.
                if (!subdomain.EndsWith(domain))
                    throw new ArgumentException("Site was not loaded from the expected domain");

                // Quash the domain portion, which should leave us with the subdomain and a trailing dot IF there is a subdomain.
                subdomain = subdomain.Replace(domain, "");
                // Check if we have anything left.  If we don't, there was no subdomain, the request was directly to the root domain:
                if (string.IsNullOrWhiteSpace(subdomain))
                    return null;

                // Quash any trailing periods
                subdomain = subdomain.TrimEnd(new[] { '.' });
            }

            return subdomain;
        }

    }
}

