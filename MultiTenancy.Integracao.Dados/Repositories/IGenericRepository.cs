using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MultiTenancy.Integracao.Dados.Repositories
{
    public interface IGenericRepository<T> : IRepository
    {
        IEnumerable<T> Listar(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> ordenadoPor = null,
            string propriedadesIncluidas = "");

        T Obter(int codigo);

        void Inserir(T entidade);

        void Excluir(int codigo);

        void Excluir(T entidade);

        void Atualizar(T entidade);
    }
}
