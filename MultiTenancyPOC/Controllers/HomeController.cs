using MultiTenancyPOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiTenancyPOC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IList<Produto> produtos = null;

            if (Session["domain"] == null)
            {
                ViewBag.PossuiCliente = false;
            }
            else
            {
                string dominio = Session["domain"].ToString();

                Dictionary<string, Dictionary<string, object>> clienteConfig =
                    HttpContext.Application["ClienteConfigs"] as Dictionary<string, Dictionary<string, object>>;

                if (clienteConfig.ContainsKey(dominio))
                {
                    ViewBag.PossuiCliente = true;
                    ViewBag.Nome = clienteConfig[dominio]["Nome"].ToString();

                    ClienteContext clienteContext = new ClienteContext(clienteConfig[dominio]["StringConexaoBanco"].ToString());

                    produtos = clienteContext.Produtos.ToList();
                }
                else
                {
                    ViewBag.PossuiCliente = false;
                }              
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