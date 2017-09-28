using MultiTenancy.Integration.Master.Models;
using System.Data.Entity;

namespace MultiTenancy.Integration.Master
{
    public class MasterContext : DbContext
    {
        public MasterContext()
            : base("MasterConnection")
        { }

        public virtual DbSet<Cliente> Clientes { get; set; }
    }
}