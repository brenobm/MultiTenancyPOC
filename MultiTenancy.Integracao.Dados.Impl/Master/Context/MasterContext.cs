using MultiTenancy.Integracao.Dados.Impl.Master.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenancy.Integracao.Dados.Impl.Master.Context
{
    public class MasterContext : DbContext
    {
        public MasterContext()
            : base("MasterConnection")
        { }

        public virtual DbSet<ClienteEntity> Clientes { get; set; }
    }
}
