using MultiTenancy.Infraestrutura.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MultiTenancy.Apresentacao.Filters
{
    public class MultiTenancyWebAPIAttribute : ActionFilterAttribute
    {
        public bool MultiTenancyNecessario { get; set; }

        public MultiTenancyWebAPIAttribute(bool multiTenancyNecessario)
        {
            this.MultiTenancyNecessario = multiTenancyNecessario;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool possuiCliente = MultiTenancyHelper.PossuiCliente();

            if ((possuiCliente && !this.MultiTenancyNecessario)
                || (!possuiCliente && this.MultiTenancyNecessario))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                actionContext.Response.Content = new StringContent("{\"Erro\": \"Você não tem permissão de acessar este serviço!\"}");
                actionContext.Response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            }

            base.OnActionExecuting(actionContext);
        }
    }
}