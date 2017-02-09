using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CamenMVC.Models;
using System.Net.Mail;

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
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Contact(ContactViewModels contatti)
        {
            if (ModelState.IsValid)
            {
                MailMessage message = new MailMessage(
                    "webservice@camen.org",
                    "cesare@cr-consult.eu,giuseppe.fortini@gmail.com",
                    "RIchiesta informazioni dal sito camen.org",
                    "Il giorno " + DateTime.Now + "<br/><strong>" +
                    contatti.Nome + " " +
                    contatti.Cognome + "</strong> [" +
                    contatti.Email + "] " +
                    "<br/> ha inviato una richiesta di informazioni dal sito www.camen.org<hr/><ul><li> Indirizzo: <strong>" +
                    contatti.Indirizzo +
                    "</strong></li><li> Telefono: <strong>" +
                    contatti.Telefono +
                     "</strong></li><li> Professione: <strong>" +
                    contatti.Professione +
                     "</strong></li><li> Organizzazione: <strong>" +
                    contatti.Organizzazione +
                    "</strong></li><li> Richiesta: <strong>" +
                    contatti.Richiesta +
                    "</strong></li>"
                    );
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
                return RedirectToAction("FormOk", "Home");
            }
            return View(contatti);
        }

        public ActionResult FormOk()
        {
            return View();
        }
    }
}