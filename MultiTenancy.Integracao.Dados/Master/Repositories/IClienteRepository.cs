using MultiTenancy.Dominio.Entidades.Master;
using MultiTenancy.Integracao.Dados.Repositories;

namespace MultiTenancy.Integracao.Dados.Master.Repositories
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
    }
}
