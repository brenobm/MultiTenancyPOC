using MultiTenancy.Integracao.Dados.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MultiTenancy.Integracao.Dados.Impl.Context;
using System.Data.Entity;
using AutoMapper;
using MultiTenancy.Dominio.Entidades;

namespace MultiTenancy.Integracao.Dados.Impl.Repositories
{
    public abstract class GenericRepository<T, TEntidade> : IGenericRepository<T>
        where T : EntidadeBase
        where TEntidade : class
    {
        protected DbContext context;
        protected DbSet<TEntidade> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntidade>();
        }

        public virtual void Atualizar(T entidade)
        {
            TEntidade dbEntitdade = dbSet.Find(entidade.Codigo);

            Mapper.Map(entidade, dbEntitdade);
            context.Entry(dbEntitdade).State = EntityState.Modified;
        }

        public virtual IEnumerable<T> Listar(
            Expression<Func<T, bool>> filtro = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> ordenadoPor = null, 
            string propriedadesIncluidas = "")
        {
            IQueryable<TEntidade> query = dbSet;

            if (filtro != null)
            {
                Expression<Func<TEntidade, bool>> dbFiltro = Mapper.Map<Expression<Func<TEntidade, bool>>>(filtro);

                query = query.Where(dbFiltro);
            }

            foreach (var includeProperty in propriedadesIncluidas.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            IEnumerable<TEntidade> resultado;

            if (ordenadoPor != null)
            {
                Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>> dbOrdenadoPor = Mapper.Map<Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>>(ordenadoPor);

                resultado = dbOrdenadoPor(query).ToList();
            }
            else
            {
                resultado = query.ToList();
            }

            return Mapper.Map<IEnumerable<T>>(resultado);
        }

        public virtual T Obter(int codigo)
        {
            return Mapper.Map<T>(dbSet.Find(codigo));
        }

        public virtual void Excluir(int codigo)
        {
            TEntidade entidade = dbSet.Find(codigo);
            Excluir(entidade);
        }

        public virtual void Excluir(T entidade)
        {
            Excluir(entidade.Codigo);
        }

        public virtual void Inserir(T entidade)
        {
            TEntidade dbEntitdade = Mapper.Map<TEntidade>(entidade);
            dbSet.Add(dbEntitdade);
        }

        private void Excluir(TEntidade dbEntidade)
        {
            if (context.Entry(dbEntidade).State == EntityState.Detached)
            {
                dbSet.Attach(dbEntidade);
            }
            dbSet.Remove(dbEntidade);
        }
    }
}
