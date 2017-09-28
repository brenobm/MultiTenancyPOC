using MultiTenancy.Infrastructure.Helpers;
using MultiTenancy.Integration.Clientes;
using MultiTenancy.Integration.Clientes.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MultiTenancyPOC.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork uow;
        private bool possuiCliente;
        Dictionary<string, Dictionary<string, object>> clientesConfig;
        Dictionary<string, object> clienteConfig;

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

                    this.uow = new UnitOfWork(clientesConfig[dominio][Constantes.Cliente.STRING_CONEXAO].ToString());

                    possuiCliente = true;
                }
                else
                {
                    possuiCliente = false;
                }
            }
        }

        public ActionResult Index()
        {
            IEnumerable<Produto> produtos = null;

            ViewBag.PossuiCliente = this.possuiCliente;

            if (this.possuiCliente)
            {
                ViewBag.Nome = clienteConfig[Constantes.Cliente.NOME].ToString();

                produtos = uow.ProdutoRepository.Get();
            }

            return View(produtos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}