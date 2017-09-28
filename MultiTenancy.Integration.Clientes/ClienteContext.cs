using MultiTenancy.Integration.Clientes.Models;
using System.Data.Entity;

namespace MultiTenancy.Integration.Clientes
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(string connectionString)
            : base(connectionString)
        { }

        public DbSet<Produto> Produtos { get; set; }
    }
}