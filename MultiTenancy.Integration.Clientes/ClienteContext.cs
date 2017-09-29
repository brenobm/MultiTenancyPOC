using MultiTenancy.Integration.Clientes.Models;
using System;
using System.Data.Entity;

namespace MultiTenancy.Integration.Clientes
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<ClienteContext>(null);
        }

        public DbSet<Produto> Produtos { get; set; }

        public override int SaveChanges()
        {
            AtualizarContextoUsuario();

            return base.SaveChanges();
        }

        private void AtualizarContextoUsuario()
        {
            var userName = "teste";
            if (String.IsNullOrWhiteSpace(userName))
                return;

            this.Database.Connection.Open();

            using (var cmd = this.Database.Connection.CreateCommand())
            {
                var parm = cmd.CreateParameter();
                parm.ParameterName = "@user";
                parm.Value = userName;

                cmd.CommandText = "AtualizaUsuarioContext";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(parm);

                cmd.ExecuteNonQuery();
            };
        }
    }
}