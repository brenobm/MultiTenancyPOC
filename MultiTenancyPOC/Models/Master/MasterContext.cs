using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MultiTenancyPOC.Models.Master
{
    public class MasterContext : DbContext
    {
        public MasterContext()
            : base("MasterConnection")
        { }

        public virtual DbSet<Cliente> Clientes { get; set; }
    }
}