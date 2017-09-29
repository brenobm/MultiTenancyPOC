using MultiTenancy.Integration.Clientes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;

namespace MultiTenancy.Integration.Clientes.Audit
{
    public class AuditoriaHelper
    {
        public static IEnumerable<Auditoria> AuditoriaFactory(ObjectStateEntry registro, string usuario)
        {
            IList<Auditoria> auditorias = new List<Auditoria>();

            if (registro.Entity is EntityObject)
            {
                IEnumerable<string> modifiedProperties = registro.GetModifiedProperties();

                foreach (string nomePropriedade in registro.GetModifiedProperties())
                {
                    Auditoria auditoria = new Auditoria();
                    auditoria.DataAcao = DateTime.Now;
                    auditoria.Usuario = usuario;

                    if (registro.State == EntityState.Added)
                    {
                        auditoria.TipoAcao = TipoAcaoBanco.Insert;
                    }
                    else if (registro.State == EntityState.Deleted)
                    {
                        auditoria.TipoAcao = TipoAcaoBanco.Delete;
                    }
                    else
                    {
                        auditoria.TipoAcao = TipoAcaoBanco.Update;
                    }

                    string valorAntigo = null;
                    string valorNovo = null;

                    valorAntigo = TratarValorBanco(registro.OriginalValues[nomePropriedade]);
                    valorNovo = TratarValorBanco(registro.CurrentValues[nomePropriedade]);

                    if (valorAntigo != valorNovo)
                    {
                        auditoria.ValorAntigo = valorAntigo;
                        auditoria.ValorNovo = valorNovo;
                        auditoria.NomeCampo = nomePropriedade;
                    }

                    auditorias.Add(auditoria);
                }
            }

            return auditorias;
        }

        private static string TratarValorBanco(object valor)
        {
            if (valor == DBNull.Value)
                return null;

            return valor.ToString();
        }
    }
}
