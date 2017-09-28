using MultiTenancy.Integration.Clientes.Models;
using MultiTenancy.Integration.Repositories;
using System;

namespace MultiTenancy.Integration.Clientes
{
    public class UnitOfWork : GenericUnitOfWork, IDisposable
    {
        private GenericRepository<Produto, ClienteContext> produtoRepository;

        public UnitOfWork(string connectionString)
        {
            this.context = new ClienteContext(connectionString);
        }

        public GenericRepository<Produto, ClienteContext> ProdutoRepository
        {
            get
            {

                if (this.produtoRepository == null)
                {
                    this.produtoRepository = new GenericRepository<Produto, ClienteContext>(context as ClienteContext);
                }
                return produtoRepository;
            }
        }
    }

}
