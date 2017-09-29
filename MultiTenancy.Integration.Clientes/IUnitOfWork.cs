using MultiTenancy.Integration.Clientes.Models;
using MultiTenancy.Integration.Repositories;

namespace MultiTenancy.Integration.Clientes
{
    public interface IUnitOfWork : IGenericUnitOfWork
    {
        GenericRepository<Produto, ClienteContext> ProdutoRepository { get; }
    }
}
