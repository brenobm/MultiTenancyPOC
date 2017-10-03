using MultiTenancy.Dominio.Entidades;
using MultiTenancy.Integracao.Dados.Impl.Context;
using MultiTenancy.Integracao.Dados.Impl.Entities;
using MultiTenancy.Integracao.Dados.Repositories;

namespace MultiTenancy.Integracao.Dados.Impl.Repositories
{
    public class ProdutoRepository : GenericRepository<Produto, ProdutoEntity>, IProdutoRepository
    {
        public ProdutoRepository(MultiTenancyContext context)
            : base(context)
        {

        }
    }
}
