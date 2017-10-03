using MultiTenancy.Apresentacao.Controllers.Base;
using System.Web.Mvc;
using System.Web.Routing;

namespace MultiTenancy.Apresentacao.Filters
{
    public class VerificarMultiTenancyClienteAttribute : ActionFilterAttribute
    {
        public bool MultiTenancyNecessario { get; set; }
        
        public VerificarMultiTenancyClienteAttribute(bool multiTenancyNecessario)
        {
            this.MultiTenancyNecessario = multiTenancyNecessario;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller is MultiTenancyController)
            {
                MultiTenancyController controller = filterContext.Controller as MultiTenancyController;

                //Está sendo acessado como multitenancy mas não deve; ou
                //Não está sendo acessado como multitenancy mas deve
                if ((controller.PossuiCliente && !this.MultiTenancyNecessario)
                    || (!controller.PossuiCliente && this.MultiTenancyNecessario))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "area", "" },
                            { "controller", "Acesso" },
                            { "action", "AcessoNegado" },
                            { "mensagem", "Você não tem permissão de acessa esta página!" }
                        });
                }
            }

            base.OnActionExecuting(filterContext);


        }
    }
}