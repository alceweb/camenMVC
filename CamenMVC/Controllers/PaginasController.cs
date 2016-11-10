using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CamenMVC.Models;
using System.Web.Helpers;

namespace CamenMVC.Controllers
{
    public class PaginasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paginas
        public ActionResult Index()
        {
            var paginas = db.Paginas.Include(p => p.TestoSmenu).OrderBy(p=>p.TestoSmenu.TestoSmenu);
            return View(paginas.ToList());
        }

        public ActionResult IndexUt (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pagina = db.Paginas.Where(p => p.Smenu_Id == id).ToList();
            //Pagina pagina = db.Paginas.Find(id);
            if (pagina == null)
            {
                return HttpNotFound();
            }
            return View(pagina);
        }

        // GET: Paginas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagina pagina = db.Paginas.Find(id);
            if (pagina == null)
            {
                return HttpNotFound();
            }
            return View(pagina);
        }

        // GET: Paginas/Create
        public ActionResult Create()
        {
            ViewBag.Smenu_Id = new SelectList(db.SottoMenus, "Smenu_Id", "TestoSmenu");
            return View();
        }

        // POST: Paginas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pagina_Id,Smenu_Id", Exclude ="Contenuo")] Pagina pagina)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            pagina.Contenuo = collection["Contenuo"];
            if (ModelState.IsValid)
            {
                db.Paginas.Add(pagina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Smenu_Id = new SelectList(db.SottoMenus, "Smenu_Id", "TestoSmenu", pagina.Smenu_Id);
            return View(pagina);
        }

        // GET: Paginas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagina pagina = db.Paginas.Find(id);
            if (pagina == null)
            {
                return HttpNotFound();
            }
            ViewBag.Smenu_Id = new SelectList(db.SottoMenus, "Smenu_Id", "TestoSmenu", pagina.Smenu_Id);
            return View(pagina);
        }

        // POST: Paginas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pagina_Id,Smenu_Id", Exclude ="Contenuo")] Pagina pagina)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            pagina.Contenuo = collection["Contenuo"];
            if (ModelState.IsValid)
            {
                db.Entry(pagina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Smenu_Id = new SelectList(db.SottoMenus, "Smenu_Id", "TestoSmenu", pagina.Smenu_Id);
            return View(pagina);
        }

        // GET: Paginas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagina pagina = db.Paginas.Find(id);
            if (pagina == null)
            {
                return HttpNotFound();
            }
            return View(pagina);
        }

        // POST: Paginas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagina pagina = db.Paginas.Find(id);
            db.Paginas.Remove(pagina);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
