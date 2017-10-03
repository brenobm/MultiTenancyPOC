using MultiTenancy.Dominio.Business;
using MultiTenancy.Dominio.Entidades.Master;
using MultiTenancy.Integracao.Dados.Master.Repositories;
using MultiTenancy.Integracao.Dados.Master.UnitOfWork;

namespace MultiTenancy.Dominio.Master.Business
{
    public class ClienteBusiness : BusinessBase<Cliente, IClienteRepository>
    {
        public ClienteBusiness(IUnitOfWork uow)
            : base(uow)
        {
            this.uow = uow;
        }
    }
}
