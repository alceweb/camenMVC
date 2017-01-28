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
    public class EventiRuolisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EventiRuolis
        public ActionResult Index()
        {
            var eventiRuolis = db.EventiRuolis.Include(e => e.Evento);
            return View(eventiRuolis.ToList());
        }

        // GET: EventiRuolis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventiRuoli eventiRuoli = db.EventiRuolis.Find(id);
            if (eventiRuoli == null)
            {
                return HttpNotFound();
            }
            return View(eventiRuoli);
        }

        // GET: EventiRuolis/Create
        public ActionResult Create()
        {
            ViewBag.Evento_Id = new SelectList(db.Eventis, "Evento_Id", "Evento");
            return View();
        }

        // POST: EventiRuolis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventoRuoliId,Evento_Id,RuoloId")] EventiRuoli eventiRuoli)
        {
            if (ModelState.IsValid)
            {
                db.EventiRuolis.Add(eventiRuoli);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Evento_Id = new SelectList(db.Eventis, "Evento_Id", "Evento", eventiRuoli.Evento_Id);
            return View(eventiRuoli);
        }

        // GET: EventiRuolis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventiRuoli eventiRuoli = db.EventiRuolis.Find(id);
            if (eventiRuoli == null)
            {
                return HttpNotFound();
            }
            ViewBag.Evento_Id = new SelectList(db.Eventis, "Evento_Id", "Evento", eventiRuoli.Evento_Id);
            return View(eventiRuoli);
        }

        // POST: EventiRuolis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventoRuoliId,Evento_Id,RuoloId")] EventiRuoli eventiRuoli)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventiRuoli).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Evento_Id = new SelectList(db.Eventis, "Evento_Id", "Evento", eventiRuoli.Evento_Id);
            return View(eventiRuoli);
        }

        // GET: EventiRuolis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventiRuoli eventiRuoli = db.EventiRuolis.Find(id);
            if (eventiRuoli == null)
            {
                return HttpNotFound();
            }
            return View(eventiRuoli);
        }

        // POST: EventiRuolis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventiRuoli eventiRuoli = db.EventiRuolis.Find(id);
            db.EventiRuolis.Remove(eventiRuoli);
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
