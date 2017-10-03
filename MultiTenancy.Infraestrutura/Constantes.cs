using System.Configuration;

namespace MultiTenancy.Infraestrutura
{
    public static class Constantes
    {
        public static class Ambiente
        {
            public static readonly string APPLICATION_CONFIGURACOES = "ConfiguracoesClientes";
            public static readonly string SESSION_CONFIGURACAO_CLIENTE = "ConfiguracaoCliente";
            public static readonly string SESSION_DOMINIO = "DominioCliente";
            public static readonly string USUARIO_AUTENTICADO = "NomeUsuarioAutenticado";

            public static string MasterDomain
            {
                get
                {
                    return ConfigurationManager.AppSettings["MasterDomain"] ?? "";
                }
            }
        }
    }
}
