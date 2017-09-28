using MultiTenancy.Integration.Master;
using MultiTenancy.Integration.Master.Models;
using MultiTenancy.Integration.Repositories;
using System;

namespace MultiTenancy.Integration.Master
{
    public class MasterUnitOfWork : GenericUnitOfWork, IDisposable
    {
        private GenericRepository<Cliente, MasterContext> clienteRepository;

        public MasterUnitOfWork()
        {
            this.context = new MasterContext();
        }

        public GenericRepository<Cliente, MasterContext> ClienteRepository
        {
            get
            {

                if (this.clienteRepository == null)
                {
                    this.clienteRepository = new GenericRepository<Cliente, MasterContext>(this.context as MasterContext);
                }
                return clienteRepository;
            }
        }
    }

}
