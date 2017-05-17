using CamenMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Web.Helpers;

namespace CamenMVC.Controllers
{
    public class UsersAdminController : Controller
    {

        public UsersAdminController()
        {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

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

        //
        // GET: /Users/
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            ViewBag.UtentiCount = UserManager.Users.Count();
            return View(await UserManager.Users.ToListAsync());
        }

        [Authorize]
        public ActionResult IndexUs()
        {
            var utente = User.Identity.GetUserId();
            ViewBag.uid = utente;
            return View();
        }

        //
        // GET: /Users/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ruoli = RoleManager.Roles.Where(r => r.Name.Contains("Squadra"));
            ViewBag.Ruoli = ruoli;
            var user = await UserManager.FindByIdAsync(id);
            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);
            foreach (var rol in ViewBag.RoleNames)
            {
                @ViewBag.Rol = rol;
            }
            return View(user);
        }

        //
        // GET: /Users/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = userViewModel.Email, Email = userViewModel.Email };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            ViewBag.Uid = id;
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Nome = user.Nome,
                Cognome = user.Cognome,
                Indirizzo = user.Indirizzo,
                Città = user.Città,
                CAP = user.CAP,
                Telefono = user.Telefono,
                Professione = user.Professione,
                Organizzazione = user.Organizzazione,
                Bloccato = user.Bloccato,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,UserName,Nome,Cognome,Indirizzo,Città,CAP,Professione,Organizzazione,Bloccato")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.Email;
                user.UserName = editUser.UserName;
                user.Email = editUser.Email;
                user.Nome = editUser.Nome;
                user.Cognome = editUser.Cognome;
                user.Indirizzo = editUser.Indirizzo;
                user.Città = editUser.Città;
                user.CAP = editUser.CAP;
                user.Telefono = editUser.Telefono;
                user.Professione = editUser.Professione;
                user.Organizzazione = editUser.Organizzazione;
                user.Bloccato = editUser.Bloccato;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        public async Task<ActionResult> EditUs(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUsViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Nome = user.Nome,
                Cognome = user.Cognome,
                Indirizzo = user.Indirizzo,
                Città = user.Città,
                CAP = user.CAP,
                Telefono = user.Telefono,
                Professione = user.Professione,
                Organizzazione = user.Organizzazione,
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUs([Bind(Include = "Id,Email,Nome,Cognome,Indirizzo,Città,CAP,Telefono,Professione,Organizzazione")] EditUsViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                user.UserName = editUser.Email;
                user.Email = editUser.Email;
                user.Nome = editUser.Nome;
                user.Cognome = editUser.Cognome;
                user.Indirizzo = editUser.Indirizzo;
                user.CAP = editUser.CAP;
                user.Città = editUser.Città;
                user.Professione = editUser.Professione;
                user.Organizzazione = editUser.Organizzazione;

                await UserManager.UpdateAsync(user);
                return RedirectToAction("IndexUs");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }
        //
        // GET: /Users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult FotoProfilo(string id)
        {
            var utente = id;
            ViewBag.uid = utente;
            return View();
        }

        [HttpPost]
        public ActionResult FotoProfilo([Bind(Include = "Id")] EditFotoViewModel editUser, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var utente = editUser.Id;
                if (file != null && file.ContentLength > 0)
                    try
                    {
                        string estensione = Path.GetExtension(file.FileName).ToLower();
                        var path = Path.Combine(Server.MapPath("/Content/Immagini/FotoIscritti/"), utente + estensione);
                        WebImage img = new WebImage(file.InputStream);
                        var larghezza = img.Width;
                        var altezza = img.Height;
                        var rapportoO = larghezza / altezza;
                        var rapportoV = altezza / larghezza;
                        if (altezza > 300 | larghezza > 300)
                        {
                            if (rapportoO >= 1)
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Resize(300, 300 / rapportoO);
                                img.Save(path);
                                ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                            }
                            else
                            {
                                img.Resize(300 / rapportoV, 300);
                                img.Save(path);
                                ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                            }
                        }
                        else
                        {
                            if (rapportoO >= 1)
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Save(path);
                                ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                            }
                            else
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Save(path);
                                ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                            }
                        }
                        ViewBag.uid = utente;
                        ViewBag.Message = "Foto caricata correttamente";
                        return View();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "Non hai selezionato nessun file. Puoi usare solo file JPG";
                }
            }

            return View();
        }
    }
}