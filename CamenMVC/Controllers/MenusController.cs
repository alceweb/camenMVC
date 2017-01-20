using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CamenMVC.Models;
using Microsoft.AspNet.Identity.Owin;

namespace CamenMVC.Controllers
{
    [Authorize(Roles = "Admin")]

    public class MenusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }



        // GET: Menus
        public ActionResult Index()
        {
            var menu = db.Menus.Include(s=>s.SottoMenus).OrderBy(o=>o.Posizione).ToList();
            return View(menu);
        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Menu_Id,Posizione,TestoMenu,Pubblica,Ruolo")] Menu menu, [Bind(Include = "MenuRuoli_Id,Ruolo,Menu_Id")] MenuRuoli menuruoli, params String[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View(menu);
        }

        public async Task<ActionResult> EditR(int? id)
        {
            Menu menu = db.Menus.Find(id);
            var assRole = db.MenuRuolis.Where(m => m.Menu_Id == id);
            ViewBag.AssRole = assRole;
            var selRole = await RoleManager.Roles.ToListAsync();
            ViewBag.RoleId = new SelectList(selRole, "Name", "Name");
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditR(int? id, [Bind(Include = "MenuRuoli_Id,Ruolo,Menu_Id")] MenuRuoli menuruoli, params String[] selectedRoles)
        {

            if (ModelState.IsValid)
            {
                var menuR = db.MenuRuolis.Where(m => m.Menu_Id == id).Select(m=>m.MenuRuoli_Id).ToList();
                if (selectedRoles != null)
                {
                    foreach (var item in menuR)
                    {
                        MenuRuoli menur = db.MenuRuolis.Find(item);
                        db.MenuRuolis.Remove(menur);
                        db.SaveChanges();
                    }
                    for (int i = 0; i < selectedRoles.Length; i++)
                    {
                        int menu = Convert.ToInt32(id);
                        menuruoli.Menu_Id = menu;
                        menuruoli.Ruolo = selectedRoles[i];
                        db.MenuRuolis.Add(menuruoli);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Non hai selezionato nessun ruolo.";
                    Menu menu2 = db.Menus.Find(id);
                    ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                    return View(menu2);
                }
            }
            Menu menu1 = db.Menus.Find(id);
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View(menu1);

        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            var roleslist = db.Roles.OrderBy(r => r.Name);
            ViewBag.RolesList = roleslist;
            //sezione pronta per mmenu agganciati a ruoli multipli
            //var ruoli = db.MenuRuolis.Where(m => m.Menu_Id == id);
            //ViewBag.Ruoli = ruoli;
            //ViewBag.RuoliCount = ruoli.Count();
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Menu_Id,Posizione,TestoMenu,Pubblica,Ruolo")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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
