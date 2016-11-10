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
    public class SottoMenusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SottoMenus
        public ActionResult Index()
        {
            var sottoMenus = db.SottoMenus.Include(s => s.TestoMenu);
            return View(sottoMenus.ToList());
        }

        public ActionResult IndexAdm(int id)
        {
            var sottoMenus = db.SottoMenus.Include(s => s.TestoMenu).Where(m=>m.Menu_Id == id);
            return View(sottoMenus.ToList());
        }

        // GET: SottoMenus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SottoMenu sottoMenu = db.SottoMenus.Find(id);
            if (sottoMenu == null)
            {
                return HttpNotFound();
            }
            return View(sottoMenu);
        }

        // GET: SottoMenus/Create
        public ActionResult Create()
        {
            ViewBag.Menu_Id = new SelectList(db.Menus, "Menu_Id", "TestoMenu");
            return View();
        }

        // POST: SottoMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Smenu_Id,Menu_Id,TestoSmenu,Pubblica")] SottoMenu sottoMenu)
        {
            if (ModelState.IsValid)
            {
                db.SottoMenus.Add(sottoMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Menu_Id = new SelectList(db.Menus, "Menu_Id", "TestoMenu", sottoMenu.Menu_Id);
            return View(sottoMenu);
        }

        // GET: SottoMenus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SottoMenu sottoMenu = db.SottoMenus.Find(id);
            if (sottoMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.Menu_Id = new SelectList(db.Menus, "Menu_Id", "TestoMenu", sottoMenu.Menu_Id);
            return View(sottoMenu);
        }

        // POST: SottoMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Smenu_Id,Menu_Id,TestoSmenu,Pubblica")] SottoMenu sottoMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sottoMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Menu_Id = new SelectList(db.Menus, "Menu_Id", "TestoMenu", sottoMenu.Menu_Id);
            return View(sottoMenu);
        }

        // GET: SottoMenus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SottoMenu sottoMenu = db.SottoMenus.Find(id);
            if (sottoMenu == null)
            {
                return HttpNotFound();
            }
            return View(sottoMenu);
        }

        // POST: SottoMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SottoMenu sottoMenu = db.SottoMenus.Find(id);
            db.SottoMenus.Remove(sottoMenu);
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
