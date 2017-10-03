using MultiTenancy.Infraestrutura.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace MultiTenancy.Apresentacao.Filters
{
    public class MultiTenancyControllerAttribute : ActionFilterAttribute
    {
        public bool MultiTenancyNecessario { get; set; }
        
        public MultiTenancyControllerAttribute(bool multiTenancyNecessario)
        {
            this.MultiTenancyNecessario = multiTenancyNecessario;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool possuiCliente = MultiTenancyHelper.PossuiCliente();

            if ((possuiCliente && !this.MultiTenancyNecessario)
                || (!possuiCliente && this.MultiTenancyNecessario))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                            { "area", "" },
                            { "controller", "Acesso" },
                            { "action", "AcessoNegado" },
                            { "mensagem", "Você não tem permissão de acessar esta página!" }
                    });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}