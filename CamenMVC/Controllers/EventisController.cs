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
    public class EventisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Eventis
        public ActionResult Index()
        {
            return View(db.Eventis.ToList());
        }


        public ActionResult IndexUt()
        {
            return View(db.Eventis.ToList());
        }

        // GET: Eventis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            var eventoRuoli = db.EventiRuolis.Where(e=>e.Evento_Id == id);
            ViewBag.EventoRuoli = eventoRuoli;
            return View(eventi);
        }

        // GET: Eventis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Evento_Id,Evento")] Eventi eventi)
        {
            if (ModelState.IsValid)
            {
                db.Eventis.Add(eventi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventi);
        }

        // GET: Eventis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            var eventoRuoli = db.EventiRuolis.Where(e=>e.Evento_Id == id).Select(e=>e.RuoloId).ToList();
            return View(new EditEventiViewModel()
            {
                Evento_Id = eventi.Evento_Id,
                Evento = eventi.Evento,
                RolesList = db.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = eventoRuoli.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        // POST: Eventis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "EventoId,RuoloId")] EventiRuoli eventiRuoli, [Bind(Include = "Evento_Id,Evento")] EditEventiViewModel editEventi, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                Eventi eventi = db.Eventis.Find(editEventi.Evento_Id);
                if (eventi == null)
                {
                    return HttpNotFound();
                }
                //Assegno i campi del modelview al db e li salvo
                eventi.Evento_Id = editEventi.Evento_Id;
                eventi.Evento = editEventi.Evento;
                db.Entry(eventi).State = EntityState.Modified;
                db.SaveChanges();
                //Filtro eventoRuoli e cancello quelli relativi all'evento
                var eventoRuoli = db.EventiRuolis.Where(e => e.Evento_Id == editEventi.Evento_Id).ToList();
                foreach (var itemDel in eventoRuoli)
                {
                    EventiRuoli eventiRuoliDel = db.EventiRuolis.Find(itemDel.EventoRuoliId);
                    db.EventiRuolis.Remove(eventiRuoliDel);
                    db.SaveChanges();
                }
                //riscrivo i ruoli assegnati
                selectedRole = selectedRole ?? new string[] { };
                foreach (var item in selectedRole)
                {
                    eventiRuoli.Evento_Id = eventi.Evento_Id;
                    eventiRuoli.RuoloId = item.ToString();
                    db.EventiRuolis.Add(eventiRuoli);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        // GET: Eventis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            return View(eventi);
        }

        // POST: Eventis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Eventi eventi = db.Eventis.Find(id);
            db.Eventis.Remove(eventi);
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
