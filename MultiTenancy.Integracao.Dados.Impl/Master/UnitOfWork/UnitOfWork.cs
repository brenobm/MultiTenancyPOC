using MultiTenancy.Integracao.Dados.Impl.Master.Context;
using MultiTenancy.Integracao.Dados.Impl.Master.Repositories;
using MultiTenancy.Integracao.Dados.Master.UnitOfWork;
using MultiTenancy.Integracao.Dados.Repositories;
using System;
using System.Collections.Generic;

namespace MultiTenancy.Integracao.Dados.Impl.Master.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected MasterContext context;

        private Dictionary<Type, IRepository> repositories;

        public UnitOfWork(MasterContext context)
        {
            this.context = context;

            this.repositories = new Dictionary<Type, IRepository>();
        }

        public IRepository Repository(Type tipo)
        {
            if (!this.repositories.ContainsKey(tipo))
            {
                IRepository repository;

                switch (tipo.Name)
                {
                    case "IClienteRepository":
                        repository = new ClienteRepository(this.context);
                        break;
                    default:
                        return null;
                }

                this.repositories.Add(tipo, repository);
            }

            return this.repositories[tipo];
        }

        public void Salvar()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
