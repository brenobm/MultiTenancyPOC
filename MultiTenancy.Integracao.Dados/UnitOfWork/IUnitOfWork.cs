using MultiTenancy.Integracao.Dados.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenancy.Integracao.Dados.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository Repository(Type tipo);
        void Salvar();
        void Dispose();
    }
}
