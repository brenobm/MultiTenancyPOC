using MultiTenancy.Integration.Clientes.Audit;
using MultiTenancy.Integration.Clientes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace MultiTenancy.Integration.Clientes
{
    public class ClienteContext : DbContext
    {
        private const string CONTEXT_SQL = @"declare @Length tinyint
                                    declare @Ctx varbinary(128)
                                    select @Length = len(@user)
                                    select @Ctx = convert(binary(1), @Length) + convert(varbinary(127), @user)
                                    set context_info @Ctx";

        public ClienteContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<ClienteContext>(null);
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

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

            //Open a connection to the database so the session is set up
            this.Database.Connection.Open();

            //Set the user context
            //Cannot use ExecuteSqlCommand here as it will close the connection
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

        //public override int SaveChanges()
        //{
        //    IEnumerable<ObjectStateEntry> changes =
        //        (this as IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntries(
        //            EntityState.Added | EntityState.Deleted | EntityState.Modified);

        //    foreach (ObjectStateEntry stateEntryEntity in changes)
        //    {
        //        if (!stateEntryEntity.IsRelationship &&
        //            stateEntryEntity.Entity != null)
        //        {
        //            var auditorias = AuditoriaHelper.AuditoriaFactory(stateEntryEntity, "teste");

        //            this.Auditorias.AddRange(auditorias);
        //        }
        //    }

        //    return base.SaveChanges();
        //}


    }
}