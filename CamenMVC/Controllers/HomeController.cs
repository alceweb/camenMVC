using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CamenMVC.Models;

namespace CamenMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var splash = db.Splashs.Where(d=>d.DataI <= DateTime.Today && d.DataF >= DateTime.Today).OrderByDescending(d => d.DataI).ToList();
            ViewBag.SplashCount = splash.Count();
            return View(splash);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}