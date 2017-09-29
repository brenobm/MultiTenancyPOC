using MultiTenancy.Infrastructure.Helpers;
using MultiTenancy.Infrastructure.MVC;
using MultiTenancy.Integration.Clientes;
using MultiTenancy.Integration.Clientes.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MultiTenancyPOC.Controllers
{
    public class HomeController : MultitenantyController
    {
        private UnitOfWork uow;

        public HomeController(UnitOfWork uow)
        {
            this.uow = uow;
        }


        public ActionResult Index()
        {
            IEnumerable<Produto> produtos = null;

            ViewBag.Nome = clienteConfig[Constantes.Cliente.NOME].ToString();

            produtos = uow.ProdutoRepository.Get();

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