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

        public static Dictionary<string, Cliente> RecuperarConfiguracoesClientes()
        {
            return (Dictionary<string, Cliente>)HttpContext.Current.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES];
        }

        public static void AtualizarConfiguracoesClientes(Dictionary<string, Cliente> configuracoes)
        {
            HttpContext.Current.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES] = configuracoes;
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
            return (Cliente) HttpContext.Current.Session[Constantes.Ambiente.SESSION_CONFIGURACAO_CLIENTE];
        }

        public static void AtualizarConfiguracaoCliente(Cliente cliente)
        {
            HttpContext.Current.Session[Constantes.Ambiente.SESSION_CONFIGURACAO_CLIENTE] = cliente;
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
