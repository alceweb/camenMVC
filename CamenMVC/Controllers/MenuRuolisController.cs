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
    public class MenuRuolisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MenuRuolis
        public ActionResult Index()
        {
            var menuRuolis = db.MenuRuolis.Include(m => m.TestoMenu);
            return View(menuRuolis.ToList());
        }

        // GET: MenuRuolis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuRuoli menuRuoli = db.MenuRuolis.Find(id);
            if (menuRuoli == null)
            {
                return HttpNotFound();
            }
            return View(menuRuoli);
        }

        // GET: MenuRuolis/Create
        public ActionResult Create()
        {
            ViewBag.Menu_Id = new SelectList(db.Menus, "Menu_Id", "TestoMenu");
            return View();
        }

        // POST: MenuRuolis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuRuoli_Id,Ruolo,Menu_Id")] MenuRuoli menuRuoli)
        {
            if (ModelState.IsValid)
            {
                db.MenuRuolis.Add(menuRuoli);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Menu_Id = new SelectList(db.Menus, "Menu_Id", "TestoMenu", menuRuoli.Menu_Id);
            return View(menuRuoli);
        }

        // GET: MenuRuolis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuRuoli menuRuoli = db.MenuRuolis.Find(id);
            if (menuRuoli == null)
            {
                return HttpNotFound();
            }
            ViewBag.Menu_Id = new SelectList(db.Menus, "Menu_Id", "TestoMenu", menuRuoli.Menu_Id);
            return View(menuRuoli);
        }

        // POST: MenuRuolis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuRuoli_Id,Ruolo,Menu_Id")] MenuRuoli menuRuoli)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuRuoli).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Menu_Id = new SelectList(db.Menus, "Menu_Id", "TestoMenu", menuRuoli.Menu_Id);
            return View(menuRuoli);
        }

        // GET: MenuRuolis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuRuoli menuRuoli = db.MenuRuolis.Find(id);
            if (menuRuoli == null)
            {
                return HttpNotFound();
            }
            return View(menuRuoli);
        }

        // POST: MenuRuolis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuRuoli menuRuoli = db.MenuRuolis.Find(id);
            db.MenuRuolis.Remove(menuRuoli);
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
