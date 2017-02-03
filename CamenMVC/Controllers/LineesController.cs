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
    public class LineesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Linees
        public ActionResult Index()
        {
            var doc = db.Documentis.ToList();
            ViewBag.Doc = doc;
            return View(db.Linees.ToList());
        }

        // GET: Linees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linee linee = db.Linees.Find(id);
            if (linee == null)
            {
                return HttpNotFound();
            }
            return View(linee);
        }

        // GET: Linees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Linees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Linea_Id,Linea")] Linee linee)
        {
            if (ModelState.IsValid)
            {
                db.Linees.Add(linee);
                db.SaveChanges();
                return RedirectToAction("Create", "Documentis");
            }

            return View(linee);
        }

        // GET: Linees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linee linee = db.Linees.Find(id);
            if (linee == null)
            {
                return HttpNotFound();
            }
            return View(linee);
        }

        // POST: Linees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Linea_Id,Linea")] Linee linee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(linee);
        }

        // GET: Linees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linee linee = db.Linees.Find(id);
            if (linee == null)
            {
                return HttpNotFound();
            }
            return View(linee);
        }

        // POST: Linees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Linee linee = db.Linees.Find(id);
            db.Linees.Remove(linee);
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
