using MultiTenancy.Apresentacao.Filters;
using MultiTenancy.Dominio.Master.Business;
using System.Web.Mvc;

namespace MultiTenancy.Apresentacao.Areas.Admin.Controllers
{
    [MultiTenancyAttribute(false)]
    public class ClientesController : Controller
    {
        private ClienteBusiness clienteBusiness;

        public ClientesController(ClienteBusiness clienteBusiness)
        {
            this.clienteBusiness = clienteBusiness;
        }

        // GET: Admin/Cliente
        public ActionResult Index()
        {
            return View(clienteBusiness.Listar());
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
