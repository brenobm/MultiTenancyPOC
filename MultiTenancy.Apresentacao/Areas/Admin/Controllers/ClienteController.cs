using MultiTenancy.Apresentacao.Controllers.Base;
using System.Web.Mvc;

namespace MultiTenancy.Apresentacao.Areas.Admin.Controllers
{
    public class ClienteController : MultiTenancyController
    {
        // GET: Admin/Cliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cliente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
