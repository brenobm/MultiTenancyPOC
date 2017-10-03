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

        public static bool PossuiCliente()
        {
            return MultiTenancyHelper.RecuperarConfiguracaoCliente() != null;
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

        public static string RecuperarDominioCliente()
        {
            object dominio = HttpContext.Current.Session[Constantes.Ambiente.SESSION_DOMINIO];

            return dominio != null ? dominio.ToString() : null;
        }

        public static Cliente RecuperarConfiguracaoCliente()
        {
            Dictionary<string, Cliente> configuracoes = (Dictionary<string, Cliente>)HttpContext.Current.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES];
            string dominio = RecuperarDominioCliente();

            if (configuracoes == null || dominio == null)
                return null;

            Cliente cliente = (Cliente) configuracoes[dominio];

            if (cliente == null)
                return null;

            return cliente;
        }

        public static string RecuperarStringConexao()
        {
            object objDominio = HttpContext.Current.Session[Constantes.Ambiente.SESSION_DOMINIO];

            if (objDominio == null)
                return null;

            string dominio = objDominio.ToString();

            string stringConexao = null;

            Dictionary<string, Cliente> configsClientes = HttpContext.Current.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES]
                as Dictionary<string, Cliente>;

            if (configsClientes.ContainsKey(dominio))
            {
                stringConexao = configsClientes[dominio].StringConexaoBanco;
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
