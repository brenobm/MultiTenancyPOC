﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MultiTenancy.Infrastructure.Helpers
{
    public static class Constantes
    {
        public static class Cliente
        {
            public static readonly string NOME = "Nome";
            public static readonly string CODIGO = "Codigo";
            public static readonly string DOMINIO = "Dominio";
            public static readonly string STRING_CONEXAO = "StringConexao";
            public static readonly string ICONE = "Icone";

        }

        public static class Ambiente
        {
            public static readonly string APPLICATION_CONFIGURACOES = "ConfiguracoesClientes";
            public static readonly string SESSION_DOMINIO = "DominioCliente";

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