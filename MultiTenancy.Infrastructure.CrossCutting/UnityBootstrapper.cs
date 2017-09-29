using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using MultiTenancy.Infrastructure.Helpers;
using MultiTenancy.Integration.Clientes;
using System.Web;
using System.Web.Mvc;

namespace MultiTenancy.Infrastructure.CrossCutting
{
    public class UnityBootstrapper
    {
        public static IUnityContainer Initialize()
        {
            IUnityContainer container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(UnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Transient);

            container.RegisterType<IUnitOfWork, UnitOfWork>(
                new InjectionFactory(c => ConstruirUoW()));

        }

        private static UnitOfWork ConstruirUoW()
        {
            string connectionString = MultiTenancyHelper.RecuperarStringConexao(HttpContext.Current);

            if (!string.IsNullOrEmpty(connectionString))
            {
                return new UnitOfWork(connectionString);
            }

            return null;
        }
    }
}
