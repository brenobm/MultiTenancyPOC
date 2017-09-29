using MultiTenancy.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MultiTenancy.Infrastructure.MVC
{
    public class MultitenantyController : Controller
    {
        protected bool possuiCliente;
        protected Dictionary<string, Dictionary<string, object>> clientesConfig;
        protected Dictionary<string, object> clienteConfig;

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

                clientesConfig =
                     HttpContext.Application[Constantes.Ambiente.APPLICATION_CONFIGURACOES] as Dictionary<string, Dictionary<string, object>>;

                if (clientesConfig.ContainsKey(dominio))
                {
                    clienteConfig = clientesConfig[dominio];

                    possuiCliente = true;
                }
                else
                {
                    possuiCliente = false;
                }
            }

            if (!possuiCliente)
            {
                filterContext.Result = RedirectToAction("ClienteInvalido", "Clientes");
                return;
            }
        }
    }
}
