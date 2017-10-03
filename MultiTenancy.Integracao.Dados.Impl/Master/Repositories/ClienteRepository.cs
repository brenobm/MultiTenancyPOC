using MultiTenancy.Dominio.Entidades.Master;
using MultiTenancy.Integracao.Dados.Impl.Master.Context;
using MultiTenancy.Integracao.Dados.Impl.Master.Entities;
using MultiTenancy.Integracao.Dados.Impl.Repositories;
using MultiTenancy.Integracao.Dados.Master.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenancy.Integracao.Dados.Impl.Master.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente, ClienteEntity>, IClienteRepository
    {
        public ClienteRepository(MasterContext context)
            : base(context)
        {

        }
    }
}
