using MultiTenancy.Dominio.Entidades.Master;
using MultiTenancy.Infraestrutura;
using MultiTenancy.Infraestrutura.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MultiTenancy.Apresentacao.Controllers.Base
{
    public class MultiTenancyController : Controller
    {
        public bool PossuiCliente { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string dominio = MultiTenancyHelper.RecuperarDominioCliente();

            PossuiCliente = false;

            if (!string.IsNullOrEmpty(dominio))
            {
                Dictionary<string, Cliente> clientesConfig = MultiTenancyHelper.RecuperarConfiguracoesClientes();
                     ;

                if (clientesConfig.ContainsKey(dominio))
                {
                    MultiTenancyHelper.AtualizarConfiguracaoCliente(clientesConfig[dominio]);

                    PossuiCliente = true;
                }
            }

            if (!PossuiCliente)
            {
                MultiTenancyHelper.AtualizarDominio(null);
                MultiTenancyHelper.AtualizarConfiguracaoCliente(null);
            }
        }
    }
}