using MultiTenancy.Integration.Master;
using MultiTenancy.Integration.Master.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace MultiTenancy.Infrastructure.Helpers
{
    public class MultiTenancyHelper
    {
        public static void CarregarClientesCadastrados(HttpApplication application)
        {
            MasterUnitOfWork muow = new MasterUnitOfWork();

            Dictionary<string, Dictionary<string, object>> configClientes = new Dictionary<string, Dictionary<string, object>>();

            foreach (Cliente cliente in muow.ClienteRepository.Get())
            {
                Dictionary<string, object> configs = new Dictionary<string, object>();

                configs.Add(Constantes.Cliente.CODIGO, cliente.Codigo);
                configs.Add(Constantes.Cliente.NOME, cliente.Nome);
                configs.Add(Constantes.Cliente.STRING_CONEXAO, cliente.StringConexaoBanco);
                configs.Add(Constantes.Cliente.ICONE, cliente.Icone);

                configClientes.Add(cliente.Dominio, configs);
            }

            application.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES] = configClientes;
        }

        public static void CarregarConfiguracaoCliente(HttpApplication application)
        {
            Uri url = application.Request.Url;

            string host = RecuperarSubdominio(url.DnsSafeHost.ToLower(), Constantes.Ambiente.MasterDomain);

            if (!string.IsNullOrEmpty(host))
            {
                application.Session[Constantes.Ambiente.SESSION_DOMINIO] = host;
            }
            else
            {
                application.Session[Constantes.Ambiente.SESSION_DOMINIO] = null;
            }
        }

        public static string RecuperarStringConexao(HttpContext context)
        {
            string dominio = context.Session[Constantes.Ambiente.SESSION_DOMINIO].ToString();

            string stringConexao = null;

            var configsClientes = HttpContext.Current.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES]
                as Dictionary<string, Dictionary<string, object>>;

            if (configsClientes.ContainsKey(dominio))
            {
                stringConexao = configsClientes[dominio][Constantes.Cliente.STRING_CONEXAO].ToString();
            }

            return stringConexao;
        }

        private static string RecuperarSubdominio(string url, string dominio)
        {
            var subdominio = url;

            if (subdominio != null)
            {

                if (!subdominio.EndsWith(dominio))
                    throw new ArgumentException("A aplicação não foi carregada através do domínio esperado.");

                subdominio = subdominio.Replace(dominio, "");

                if (string.IsNullOrWhiteSpace(subdominio))
                    return null;

                subdominio = subdominio.TrimEnd(new[] { '.' });
            }

            return subdominio;
        }
    }
}
