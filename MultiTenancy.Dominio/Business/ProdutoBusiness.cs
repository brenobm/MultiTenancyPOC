using MultiTenancy.Dominio.Entidades;
using MultiTenancy.Integracao.Dados.Repositories;
using MultiTenancy.Integracao.Dados.UnitOfWork;

namespace MultiTenancy.Dominio.Business
{
    public class ProdutoBusiness : BusinessBase<Produto, IProdutoRepository>
    {
        public ProdutoBusiness(IUnitOfWork uow)
            : base(uow)
        {
            this.uow = uow;
        }
    }
}
