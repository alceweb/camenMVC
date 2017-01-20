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
    [Authorize(Roles = "Admin")]

    public class SplashesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Splashes
        public ActionResult Index()
        {
            var splash = db.Splashs.ToList();
            ViewBag.SplashCount = splash.Count();
            return View(splash);
        }

        // GET: Splashes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Splash splash = db.Splashs.Find(id);
            if (splash == null)
            {
                return HttpNotFound();
            }
            return View(splash);
        }

        // GET: Splashes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Splashes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Splash_Id,Titolo,SottoTitolo,DataI,DataF", Exclude ="Testo")] Splash splash)
        {
           if (ModelState.IsValid)
            {
                  FormCollection collection = new FormCollection(Request.Unvalidated().Form);
                splash.Testo = collection["Testo"];
               db.Splashs.Add(splash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(splash);
        }

        // GET: Splashes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Splash splash = db.Splashs.Find(id);
            if (splash == null)
            {
                return HttpNotFound();
            }
            return View(splash);
        }

        // POST: Splashes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Splash_Id,Titolo,SottoTitolo,DataI,DataF", Exclude ="Testo")] Splash splash)
        {
            if (ModelState.IsValid)
            {
                FormCollection collection = new FormCollection(Request.Unvalidated().Form);
                splash.Testo = collection["Testo"];
                db.Entry(splash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(splash);
        }

        // GET: Splashes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Splash splash = db.Splashs.Find(id);
            if (splash == null)
            {
                return HttpNotFound();
            }
            return View(splash);
        }

        // POST: Splashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Splash splash = db.Splashs.Find(id);
            db.Splashs.Remove(splash);
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
