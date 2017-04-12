using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CamenMVC.Models;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using Newtonsoft.Json.Converters;

namespace CamenMVC.Controllers
{
    public class StatistichesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, DateFormatString = "dd/MMM/yy" };

        // GET: Statistiches
        public ActionResult Index()
        {
            var statistiche = db.Statistiches.ToList();
            ViewBag.Data = db.Statistiches.OrderByDescending(d => d.Data.Month).ToList();
            ViewBag.DataPoints = JsonConvert.SerializeObject(db.Statistiches.Where(u => u.UName != "anonimous").OrderByDescending(d => d.Data).GroupBy(d => d.UName).Select(s => new { x = s.Key, y = s.Count() }).OrderByDescending(s => s.y).ToList(), _jsonSetting);
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(db.Statistiches
                .OrderByDescending(d => d.Data.Month)
                .GroupBy(d =>  DbFunctions.TruncateTime(d.Data))
                .Select(s => new { x = s.Key, y = s.Count()})
                .ToList(), _jsonSetting);
            ViewBag.StatisticheCount = statistiche.Count();
            return View(statistiche);
        }

        public ActionResult IndexStat()
        {
            try
            {
                ViewBag.DataPoints = JsonConvert.SerializeObject(db.Statistiches.OrderByDescending(d => d.Data).GroupBy(d => d.UName).Select(s => new { x = s.Key, y = s.Count() }).OrderByDescending(s => s.y).ToList(), _jsonSetting);
                ViewBag.DataPoints1 = JsonConvert.SerializeObject(db.Statistiches
                    .OrderByDescending(d => d.Data)
                    .GroupBy(d => d.Data.Month)
                    .Select(s => new { x = s.Key, y = s.Count() })
                    .ToList(), _jsonSetting);
                return View();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return View("Error");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return View("Error");
            }
        }


        public ActionResult Grafico()
        {
            try
            {
                var pag = db.Statistiches.GroupBy(p => p.Pagina).ToList();
                ViewBag.DataPoints = pag;
                return View(pag);
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return View("Error");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return View("Error");
            }

        }


        // GET: Statistiches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistiche statistiche = db.Statistiches.Find(id);
            if (statistiche == null)
            {
                return HttpNotFound();
            }
            return View(statistiche);
        }

        // GET: Statistiches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statistiches/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Statistiche_Id,Data,Ip,Pagina,UId,UName")] Statistiche statistiche)
        {
            if (ModelState.IsValid)
            {
                db.Statistiches.Add(statistiche);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statistiche);
        }

        // GET: Statistiches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistiche statistiche = db.Statistiches.Find(id);
            if (statistiche == null)
            {
                return HttpNotFound();
            }
            return View(statistiche);
        }

        // POST: Statistiches/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Statistiche_Id,Data,Ip,Pagina,UId,UName")] Statistiche statistiche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statistiche).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statistiche);
        }

        // GET: Statistiches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistiche statistiche = db.Statistiches.Find(id);
            if (statistiche == null)
            {
                return HttpNotFound();
            }
            return View(statistiche);
        }

        // POST: Statistiches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statistiche statistiche = db.Statistiches.Find(id);
            db.Statistiches.Remove(statistiche);
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
