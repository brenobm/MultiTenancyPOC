using MultiTenancy.Integration.Master;
using MultiTenancy.Integration.Master.Models;
using System.Net;
using System.Web.Mvc;

namespace MultiTenancyPOC.Controllers
{
    public class ClientesController : Controller
    {
        private MasterUnitOfWork muow = new MasterUnitOfWork();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(muow.ClienteRepository.Get());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = muow.ClienteRepository.GetByID(id);

            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nome,Dominio,StringConexaoBanco,Icone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                muow.ClienteRepository.Insert(cliente);
                muow.Save();

                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = muow.ClienteRepository.GetByID(id);

            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome,Dominio,StringConexaoBanco,Icone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                muow.ClienteRepository.Update(cliente);
                muow.Save();

                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = muow.ClienteRepository.GetByID(id);

            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = muow.ClienteRepository.GetByID(id);

            muow.ClienteRepository.Delete(cliente);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                muow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
