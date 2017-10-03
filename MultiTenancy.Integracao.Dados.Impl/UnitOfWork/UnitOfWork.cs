using MultiTenancy.Integracao.Dados.Impl.Context;
using MultiTenancy.Integracao.Dados.Impl.Repositories;
using MultiTenancy.Integracao.Dados.Repositories;
using MultiTenancy.Integracao.Dados.UnitOfWork;
using System;
using System.Collections.Generic;

namespace MultiTenancy.Integracao.Dados.Impl.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private MultiTenancyContext context;

        private Dictionary<Type, IRepository> repositories;

        public UnitOfWork(MultiTenancyContext context)
        {
            this.context = context;

            this.repositories = new Dictionary<Type, IRepository>();
        }

        public IRepository Repository (Type tipo)
        {
            if (!this.repositories.ContainsKey(tipo))
            {
                IRepository repository;

                switch(tipo.Name)
                {
                    case "IProdutoRepository":
                        repository = new ProdutoRepository(this.context);
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
