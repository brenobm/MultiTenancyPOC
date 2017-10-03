using MultiTenancy.Dominio.Entidades;
using MultiTenancy.Integracao.Dados.Repositories;
using MultiTenancy.Integracao.Dados.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MultiTenancy.Dominio.Business
{
    public abstract class BusinessBase<T, TRepositorio>
        where T : EntidadeBase
        where TRepositorio : IGenericRepository<T>
    {
        protected IUnitOfWork uow;
        protected IGenericRepository<T> repository;

        public BusinessBase(IUnitOfWork uow)
        {
            this.uow = uow;
            this.repository = (TRepositorio)uow.Repository(typeof(TRepositorio));
        }

        public virtual void Salvar(T entidade)
        {
            T ent = repository.Obter(entidade.Codigo);

            if (ent == null)
            {
                repository.Inserir(entidade);
            }
            else
            {
                repository.Atualizar(entidade);
            }

            uow.Salvar();
        }

        public virtual T Obter(int codigo)
        {
            return repository.Obter(codigo);
        }

        public virtual IEnumerable<T> Listar(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> ordenadoPor = null,
            string propriedadesIncluidas = "")
        {
            return repository.Listar(filtro, ordenadoPor, propriedadesIncluidas);
        }

        public virtual void Excluir(T entidade)
        {
            repository.Excluir(entidade);

            uow.Salvar();
        }

        public virtual void Excluir(int codigo)
        {
            repository.Excluir(codigo);

            uow.Salvar();
        }
    }
}
