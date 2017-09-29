using MultiTenancy.Integration.Master.Models;
using MultiTenancy.Integration.Repositories;

namespace MultiTenancy.Integration.Master
{
    public interface IMasterUnitOfWork : IGenericUnitOfWork
    {
        GenericRepository<Cliente, MasterContext> ClienteRepository { get; }
    }
}
