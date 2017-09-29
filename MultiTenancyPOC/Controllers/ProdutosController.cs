using MultiTenancy.Infrastructure.MVC;
using MultiTenancy.Integration.Clientes;
using MultiTenancy.Integration.Clientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MultiTenancyPOC.Controllers
{
    public class ProdutosController : MultitenantyController
    {
        private UnitOfWork uow;

        public ProdutosController(UnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET: Produtos
        public ActionResult Index()
        {
            return View(uow.ProdutoRepository.Get());
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = uow.ProdutoRepository.GetByID(id);

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
                uow.ProdutoRepository.Insert(produto);
                uow.Save();

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

            Produto produto = uow.ProdutoRepository.GetByID(id);

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
                uow.ProdutoRepository.Update(produto);
                uow.Save();

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

            Produto produto = uow.ProdutoRepository.GetByID(id);

            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = uow.ProdutoRepository.GetByID(id);

            uow.ProdutoRepository.Delete(produto);

            uow.Save();

            return RedirectToAction("Index");
        }
    }
}
