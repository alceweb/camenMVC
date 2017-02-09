using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CamenMVC.Models;
using System.IO;

namespace CamenMVC.Controllers
{
    public class DocumentisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documentis
        public ActionResult Index()
        {
            var documentis = db.Documentis.Include(d => d.Categoria).Include(d => d.Evento).Include(d => d.Linea).Include(d => d.Sessione).ToList();
            ViewBag.DocumentisCount = documentis.Count();
            return View(documentis);
        }
        // Pagina documenti contenente files a disposizione anche degli utenti non registrati
        public ActionResult IndexAn()
        {
            var documentis = db.Documentis.Where(d=>d.Evento_Id == 1).OrderByDescending(d => d.Data).Include(d => d.Categoria).ToList();
            return View(documentis);
        }
        public ActionResult IndexUt()
        {
            var documentis = db.Documentis.Where(d => d.Evento_Id != 1).OrderByDescending(d => d.Data).Include(d => d.Categoria).Include(d => d.Evento).Include(d => d.Linea).Include(d => d.Sessione).ToList();
            return View(documentis);
        }

        public ActionResult Eventi(int? id)
        {
            var documentis = db.Documentis.Where(e=>e.Categoria_Id == id).Include(d => d.Categoria).Include(d => d.Evento).Include(d => d.Linea).Include(d => d.Sessione);
            return View(documentis.ToList());
        }
        [Authorize]
        public ActionResult Linee(int? id)
        {
            int evento = Convert.ToInt32(Request.QueryString["evento"]);
            int categoria = Convert.ToInt32(Request.QueryString["categoria"]);
            var documentis = db.Documentis.Where(
                e => e.Categoria_Id == categoria &&
                e.Evento_Id == evento).Include(d => d.Categoria).Include(d => d.Evento).Include(d => d.Linea).Include(d => d.Sessione);
            return View(documentis.ToList());
        }

        public ActionResult Sessioni(int? id)
        {
            int linea = Convert.ToInt32(Request.QueryString["linea"]);
            int evento = Convert.ToInt32(Request.QueryString["evento"]);
            int categoria = Convert.ToInt32(Request.QueryString["categoria"]);
            var documentis = db.Documentis.Where(
                e => e.Categoria_Id == categoria && 
                e.Evento_Id == evento &&
                e.Linea_Id == linea).Include(d => d.Categoria).Include(d => d.Evento).Include(d => d.Linea).Include(d => d.Sessione);
            return View(documentis.ToList());
        }

        public ActionResult Docpdf(int? id)
        {
            int sessione = Convert.ToInt32(Request.QueryString["sessione"]);
            int linea = Convert.ToInt32(Request.QueryString["linea"]);
            int evento = Convert.ToInt32(Request.QueryString["evento"]);
            int categoria = Convert.ToInt32(Request.QueryString["categoria"]);
            var documentis = db.Documentis.OrderByDescending(d => d.Data).Where(
                e => e.Categoria_Id == categoria &&
                e.Evento_Id == evento &&
                e.Linea_Id == linea &&
                e.Sessione_Id == sessione).Include(d => d.Categoria).Include(d => d.Evento).Include(d => d.Linea).Include(d => d.Sessione);
            return View(documentis.ToList());
        }


        // GET: Documentis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (documenti == null)
            {
                return HttpNotFound();
            }
            return View(documenti);
        }

        // GET: Documentis/Create
        public ActionResult Create()
        {
            ViewBag.Categoria_Id = new SelectList(db.Categories.OrderBy(c=>c.Categoria), "Categoria_Id", "Categoria");
            ViewBag.Evento_Id = new SelectList(db.Eventis.OrderBy(e=>e.Evento), "Evento_Id", "Evento");
            ViewBag.Linea_Id = new SelectList(db.Linees.OrderBy(l=>l.Linea), "Linea_Id", "Linea");
            ViewBag.Sessione_Id = new SelectList(db.Sessionis.OrderBy(s=>s.Sessione), "Sessione_Id", "Sessione");
            return View();
        }

        // POST: Documentis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase NomeFile, [Bind(Include = "Documento_Id,Categoria_Id,Evento_Id,Linea_Id,Sessione_Id,Oratore,Titolo,Descrizione,Data,Riferimento,Lingua,NomeFile")] Documenti documenti)
        {
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(NomeFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Documenti/"), filename);
                NomeFile.SaveAs(path);
                documenti.NomeFile = filename;
                db.Documentis.Add(documenti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categoria_Id = new SelectList(db.Categories, "Categoria_Id", "Categoria", documenti.Categoria_Id);
            ViewBag.Evento_Id = new SelectList(db.Eventis, "Evento_Id", "Evento", documenti.Evento_Id);
            ViewBag.Linea_Id = new SelectList(db.Linees, "Linea_Id", "Linea", documenti.Linea_Id);
            ViewBag.Sessione_Id = new SelectList(db.Sessionis, "Sessione_Id", "Sessione", documenti.Sessione_Id);
            return View(documenti);
        }

        // GET: Documentis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (documenti == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria_Id = new SelectList(db.Categories, "Categoria_Id", "Categoria", documenti.Categoria_Id);
            ViewBag.Evento_Id = new SelectList(db.Eventis, "Evento_Id", "Evento", documenti.Evento_Id);
            ViewBag.Linea_Id = new SelectList(db.Linees, "Linea_Id", "Linea", documenti.Linea_Id);
            ViewBag.Sessione_Id = new SelectList(db.Sessionis, "Sessione_Id", "Sessione", documenti.Sessione_Id);
            var documentoRuoli = db.DocRuolis.Where(d => d.Documento_Id == id).Select(e=>e.RuoloId).ToList();
            return View(new EditDocViewModel()
            {
                Documento_Id = documenti.Documento_Id,
                Categoria_Id = documenti.Categoria_Id,
                Evento_Id = documenti.Evento_Id,
                Linea_Id = documenti.Linea_Id,
                Sessione_Id = documenti.Sessione_Id,
                Oratore = documenti.Oratore,
                Titolo = documenti.Titolo,
                Descrizione = documenti.Descrizione,
                Data = documenti.Data,
                Riferimento = documenti.Riferimento,
                Lingua = documenti.Lingua,
                NomeFile = documenti.NomeFile,
                RolesList = db.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = documentoRuoli.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })

            });
        }

        // POST: Documentis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "Documento_Id,RuoloId")] DocRuoli docRuoli, [Bind(Include = "Documento_Id,Categoria_Id,Evento_Id,Linea_Id,Sessione_Id,Oratore,Titolo,Descrizione,Data,Riferimento,Lingua,NomeFile")] EditDocViewModel editDoc, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                Documenti documenti = db.Documentis.Find(id);
                if (documenti == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Categoria_Id = new SelectList(db.Categories, "Categoria_Id", "Categoria", documenti.Categoria_Id);
                ViewBag.Evento_Id = new SelectList(db.Eventis, "Evento_Id", "Evento", documenti.Evento_Id);
                ViewBag.Linea_Id = new SelectList(db.Linees, "Linea_Id", "Linea", documenti.Linea_Id);
                ViewBag.Sessione_Id = new SelectList(db.Sessionis, "Sessione_Id", "Sessione", documenti.Sessione_Id);
                //Assegno i campi del modelview al db e li salvo
                documenti.Documento_Id = editDoc.Documento_Id;
                documenti.Categoria_Id = editDoc.Categoria_Id;
                documenti.Evento_Id = editDoc.Evento_Id;
                documenti.Linea_Id = editDoc.Linea_Id;
                documenti.Sessione_Id = editDoc.Sessione_Id;
                documenti.Oratore = editDoc.Oratore;
                documenti.Titolo = editDoc.Titolo;
                documenti.Descrizione = editDoc.Descrizione;
                documenti.Data = editDoc.Data;
                documenti.Riferimento = editDoc.Riferimento;
                documenti.Lingua = editDoc.Lingua;
                documenti.NomeFile = editDoc.NomeFile;
                db.Entry(documenti).State = EntityState.Modified;
                db.SaveChanges();
                //Filtro documentoRuoli e cancello quelli relativi al documento
                var documentoRuoli = db.DocRuolis.Where(d => d.Documento_Id == editDoc.Documento_Id).ToList();
                foreach (var itemDel in documentoRuoli)
                {
                    DocRuoli documentiRuoliDel = db.DocRuolis.Find(itemDel.DocRuoli_Id);
                    db.DocRuolis.Remove(documentiRuoliDel);
                    db.SaveChanges();
                }
                //riscrivo i ruoli assegnati
                selectedRole = selectedRole ?? new string[] { };
                foreach (var item in selectedRole)
                {
                    docRuoli.Documento_Id = documenti.Documento_Id;
                    docRuoli.RuoloId = item.ToString();
                    db.DocRuolis.Add(docRuoli);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Documentis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (documenti == null)
            {
                return HttpNotFound();
            }
            return View(documenti);
        }

        // POST: Documentis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var file = "~/Content/Documenti/" + Request.QueryString["nomefile"];
            System.IO.File.Delete(Server.MapPath(file));
            Documenti documenti = db.Documentis.Find(id);
            db.Documentis.Remove(documenti);
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
