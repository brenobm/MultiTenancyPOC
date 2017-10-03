using MultiTenancy.Dominio.Entidades.Master;
using MultiTenancy.Infraestrutura;
using MultiTenancy.Infraestrutura.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MultiTenancy.Apresentacao.Controllers.Base
{
    public class MultiTenancyController : Controller
    {
        protected bool possuiCliente;
        protected Cliente clienteConfig;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (Session[Constantes.Ambiente.SESSION_DOMINIO] == null)
            {
                possuiCliente = false;
            }
            else
            {
                string dominio = Session[Constantes.Ambiente.SESSION_DOMINIO].ToString();

                Dictionary<string, Cliente> clientesConfig =
                     HttpContext.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES] as Dictionary<string, Cliente>;

                if (clientesConfig.ContainsKey(dominio))
                {
                    clienteConfig = clientesConfig[dominio];

                    MultiTenancyHelper.AtualizarDominio(dominio);

                    possuiCliente = true;
                }
                else
                {
                    possuiCliente = false;
                }
            }

            if (!possuiCliente)
            {
                MultiTenancyHelper.AtualizarDominio(null);
                filterContext.Result = RedirectToAction("ClienteInvalido", "Clientes");
                return;
            }
        }
    }
}