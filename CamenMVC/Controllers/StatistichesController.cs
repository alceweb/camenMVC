using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CamenMVC.Models;

namespace CamenMVC.Controllers
{
    public class StatistichesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Statistiches
        public ActionResult Index()
        {
            return View(db.Statistiches.ToList());
        }

        // GET: Statistiches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistiche statistiche = db.Statistiches.Find(id);
            if (statistiche == null)
            {
                return HttpNotFound();
            }
            return View(statistiche);
        }

        // GET: Statistiches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statistiches/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Statistiche_Id,Data,Ip,Pagina,UId,UName")] Statistiche statistiche)
        {
            if (ModelState.IsValid)
            {
                db.Statistiches.Add(statistiche);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statistiche);
        }

        // GET: Statistiches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistiche statistiche = db.Statistiches.Find(id);
            if (statistiche == null)
            {
                return HttpNotFound();
            }
            return View(statistiche);
        }

        // POST: Statistiches/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Statistiche_Id,Data,Ip,Pagina,UId,UName")] Statistiche statistiche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statistiche).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statistiche);
        }

        // GET: Statistiches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistiche statistiche = db.Statistiches.Find(id);
            if (statistiche == null)
            {
                return HttpNotFound();
            }
            return View(statistiche);
        }

        // POST: Statistiches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statistiche statistiche = db.Statistiches.Find(id);
            db.Statistiches.Remove(statistiche);
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
