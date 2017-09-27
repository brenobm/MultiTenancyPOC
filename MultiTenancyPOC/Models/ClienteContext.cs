using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MultiTenancyPOC.Models
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(string connectionString)
            : base(connectionString)
        { }

        public DbSet<Produto> Produtos { get; set; }
    }
}