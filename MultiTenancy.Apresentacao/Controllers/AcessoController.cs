using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiTenancy.Apresentacao.Controllers
{
    public class AcessoController : Controller
    {
        // GET: Acesso
        public ActionResult Index()
        {
            return RedirectToAction("AcessoNegado");
        }

        public ActionResult AcessoNegado(string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem))
                mensagem = "Acesso Negado!";

            ViewBag.Mensagem = mensagem;

            return View();
        }
    }
}