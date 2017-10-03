using MultiTenancy.Dominio.Entidades.Master;
using MultiTenancy.Dominio.Master.Business;
using System;
using System.Collections.Generic;
using System.Web;

namespace MultiTenancy.Infraestrutura.Helpers
{
    public class MultiTenancyHelper
    {
        private ClienteBusiness clienteBusiness;

        public MultiTenancyHelper(ClienteBusiness clienteBusiness)
        {
            this.clienteBusiness = clienteBusiness;
        }

        public void CarregarClientesCadastrados()
        {
            Dictionary<string, Cliente> configClientes = new Dictionary<string, Cliente>();

            foreach (Cliente cliente in clienteBusiness.Listar())
            {
                configClientes.Add(cliente.Dominio, cliente);
            }

            HttpContext.Current.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES] = configClientes;
        }

        public static void CarregarConfiguracaoCliente()
        {
            Uri url = HttpContext.Current.Request.Url;

            string host = RecuperarSubdominio(url.DnsSafeHost.ToLower(), Constantes.Ambiente.MasterDomain);

            if (!string.IsNullOrEmpty(host))
            {
                HttpContext.Current.Session[Constantes.Ambiente.SESSION_DOMINIO] = host;
            }
            else
            {
                HttpContext.Current.Session[Constantes.Ambiente.SESSION_DOMINIO] = null;
            }
        }

        public static string RecuperarStringConexao()
        {
            string dominio = HttpContext.Current.Session[Constantes.Ambiente.SESSION_DOMINIO].ToString();

            string stringConexao = null;

            var configsClientes = HttpContext.Current.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES]
                as Dictionary<string, Cliente>;

            if (configsClientes.ContainsKey(dominio))
            {
                stringConexao = configsClientes[dominio].StringConexaoBanco;
            }

            return stringConexao;
        }

        public static void AtualizarDominio(string dominio)
        {
            HttpContext.Current.Session[Constantes.Ambiente.SESSION_DOMINIO] = dominio;
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
