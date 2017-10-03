using MultiTenancy.Integracao.Dados.Impl.Entities;
using System;
using System.Data.Entity;

namespace MultiTenancy.Integracao.Dados.Impl.Context
{
    public class MultiTenancyContext : DbContext
    {
        private string nomeUsuarioAutenticado;

        public MultiTenancyContext(string connectionString, string nomeUsuarioAutenticado)
            : base(connectionString)
        {
            this.nomeUsuarioAutenticado = nomeUsuarioAutenticado;

            Database.SetInitializer<MultiTenancyContext>(null);
        }

        public DbSet<ProdutoEntity> Produtos { get; set; }

        public override int SaveChanges()
        {
            AtualizarContextoUsuario();

            return base.SaveChanges();
        }

        private void AtualizarContextoUsuario()
        {
            if (String.IsNullOrWhiteSpace(nomeUsuarioAutenticado))
                return;

            this.Database.Connection.Open();

            using (var cmd = this.Database.Connection.CreateCommand())
            {
                var parm = cmd.CreateParameter();
                parm.ParameterName = "@user";
                parm.Value = nomeUsuarioAutenticado;

                cmd.CommandText = "AtualizaUsuarioContext";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(parm);

                cmd.ExecuteNonQuery();
            };
        }
    }
}
