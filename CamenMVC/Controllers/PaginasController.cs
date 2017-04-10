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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var paginas = db.Paginas.Include(p => p.TestoSmenu).Include(m=>m.TestoSmenu.TestoMenu).OrderBy(p=> new { p.TestoSmenu.TestoMenu.TestoMenu, p.TestoSmenu.TestoSmenu }).ToList();
            ViewBag.PaginasCount = paginas.Count();
            return View(paginas);
        }

        public ActionResult IndexUt (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var intestazione = db.SottoMenus.Where(s => s.Smenu_Id == id).Select(s=>s.TestoSmenu);
            ViewBag.Intestazione = intestazione;
            var smenuId = db.SottoMenus.Where(s => s.Smenu_Id == id).Select(s => s.Smenu_Id);
            ViewBag.SmenuId = smenuId;
            //Se l'utente è admin visualizzo anche le pagine non pubblicate'
            if (User.IsInRole("Admin"))
            {
                var pagina = db.Paginas.OrderBy(p => p.Posizione).Where(p => p.Smenu_Id == id).ToList();
                if (pagina == null)
                {
                    return HttpNotFound();
                }
                return View(pagina);
            }
            else
            {
                var pagina = db.Paginas.OrderBy(p => p.Posizione).Where(p => p.Smenu_Id == id && p.Pubblica == true).ToList();
                if (pagina == null)
                {
                    return HttpNotFound();
                }
                return View(pagina);
            }
        }

        // GET: Paginas/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Smenu_Id = new SelectList(db.SottoMenus, "Smenu_Id", "TestoSmenu");
            return View();
        }

        // POST: Paginas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pagina_Id,Smenu_Id,Posizione,Pubblica", Exclude ="Contenuo")] Pagina pagina)
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

        [Authorize(Roles = "Admin")]
        public ActionResult CreateAd()
        {
            ViewBag.Smenu_Id = new SelectList(db.SottoMenus, "Smenu_Id", "TestoSmenu");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAd([Bind(Include = "Pagina_Id,Smenu_Id,Posizione,Pubblica", Exclude = "Contenuo")] Pagina pagina)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            pagina.Contenuo = collection["Contenuo"];
            if (ModelState.IsValid)
            {
                int smenuid = Convert.ToInt32(Request.QueryString["SmenuId"]);
                pagina.Smenu_Id = smenuid;
                db.Paginas.Add(pagina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Smenu_Id = new SelectList(db.SottoMenus, "Smenu_Id", "TestoSmenu", pagina.Smenu_Id);
            return View(pagina);
        }


        [Authorize(Roles = "Admin")]
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
            var smenu = db.SottoMenus.Include(m => m.TestoMenu.TestoMenu).Select(s => new { ID = s.Smenu_Id, Smenu = s.TestoMenu.TestoMenu + "=>" + s.TestoSmenu });
            ViewBag.Smenu_Id = new SelectList(smenu, "ID", "Smenu", pagina.Smenu_Id);
            return View(pagina);
        }

        // POST: Paginas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pagina_Id,Smenu_Id,Posizione,Pubblica", Exclude ="Contenuo")] Pagina pagina)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagina pagina = db.Paginas.Find(id);
            db.Paginas.Remove(pagina);
            db.SaveChanges();
            return RedirectToAction("Index", "Paginas");
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
