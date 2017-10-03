using Microsoft.Practices.Unity;
using MultiTenancy.Infraestrutura.Helpers;
using MultiTenancy.Integracao.Dados.Impl.Context;
using System;

namespace MultiTenancy.Infraestrutura.IoC
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Transient);

            container.RegisterType<MultiTenancyContext, MultiTenancyContext>(
                new InjectionFactory(c => ConstruirContext()));

            container.RegisterType<Integracao.Dados.UnitOfWork.IUnitOfWork, Integracao.Dados.Impl.UnitOfWork.UnitOfWork>();

            container.RegisterType<Integracao.Dados.Master.UnitOfWork.IUnitOfWork, Integracao.Dados.Impl.Master.UnitOfWork.UnitOfWork>();
        }

        private static MultiTenancyContext ConstruirContext()
        {
            string connectionString = MultiTenancyHelper.RecuperarStringConexao();
            string nomeUsuarioAutenticado = AutenticacaoHelper.RecuperarUsuarioAutenticado();

            if (!string.IsNullOrEmpty(connectionString))
            {
                return new MultiTenancyContext(connectionString, nomeUsuarioAutenticado);
            }

            return null;
        }
    }
}