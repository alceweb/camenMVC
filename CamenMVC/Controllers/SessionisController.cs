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
    public class SessionisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sessionis
        public ActionResult Index()
        {
            var sessioni = db.Sessionis.OrderBy(s=>s.Sessione).ToList();
            ViewBag.SessioniCount = sessioni.Count();
            return View(sessioni);
        }

        // GET: Sessionis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessioni sessioni = db.Sessionis.Find(id);
            if (sessioni == null)
            {
                return HttpNotFound();
            }
            return View(sessioni);
        }

        // GET: Sessionis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sessionis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sessione_Id,Sessione")] Sessioni sessioni)
        {
            if (ModelState.IsValid)
            {
                db.Sessionis.Add(sessioni);
                db.SaveChanges();
                return RedirectToAction("Create", "Documentis");
            }

            return View(sessioni);
        }

        // GET: Sessionis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessioni sessioni = db.Sessionis.Find(id);
            if (sessioni == null)
            {
                return HttpNotFound();
            }
            return View(sessioni);
        }

        // POST: Sessionis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sessione_Id,Sessione")] Sessioni sessioni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessioni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sessioni);
        }

        // GET: Sessionis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessioni sessioni = db.Sessionis.Find(id);
            if (sessioni == null)
            {
                return HttpNotFound();
            }
            return View(sessioni);
        }

        // POST: Sessionis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sessioni sessioni = db.Sessionis.Find(id);
            db.Sessionis.Remove(sessioni);
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
