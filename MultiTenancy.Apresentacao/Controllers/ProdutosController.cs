using MultiTenancy.Apresentacao.Controllers.Base;
using MultiTenancy.Apresentacao.Filters;
using MultiTenancy.Dominio.Business;
using MultiTenancy.Dominio.Entidades;
using System.Net;
using System.Web.Mvc;

namespace MultiTenancy.Apresentacao.Controllers
{
    [VerificarMultiTenancyCliente(true)]
    public class ProdutosController : MultiTenancyController
    {
        private ProdutoBusiness produtoBusiness;

        public ProdutosController(ProdutoBusiness produtoBusiness)
        {
            this.produtoBusiness = produtoBusiness;
        }

        public ActionResult Index()
        {
            var produtos = produtoBusiness.Listar();

            return View(produtos);
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoBusiness.Obter(id.Value);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produtoBusiness.Salvar(produto);

                return RedirectToAction("Index");
            }

            return View(produto);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoBusiness.Obter(id.Value);

            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produtoBusiness.Salvar(produto);

                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoBusiness.Obter(id.Value);

            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = produtoBusiness.Obter(id);

            produtoBusiness.Excluir(produto);
            
            return RedirectToAction("Index");
        }
    }
}