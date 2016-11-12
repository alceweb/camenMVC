using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CamenMVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name ="Cognome")]
        public string Cognome { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Menu
    {
        [Key]
        [Display(Name ="Menu")]
        public int Menu_Id { get; set; }
        [Display(Name ="Posizione")]
        public int Posizione { get; set; }
        [Display(Name ="Testo menu")]
        public string TestoMenu { get; set; }
        [Display(Name ="Pubblica")]
        public bool Pubblica { get; set; }
        public string Ruolo { get; set; }
        public virtual ICollection<SottoMenu> SottoMenus { get; set; }
}

    public class SottoMenu
    {
        [Key]
        public int Smenu_Id { get; set; }
        [Display(Name = "Menu")]

        public int Menu_Id { get; set; }
        public virtual Menu TestoMenu { get; set; }
        [Display(Name ="Testo sottomenu")]
        public string TestoSmenu { get; set; }
        [Display(Name ="Pubblica")]
        public bool Pubblica { get; set; }


    }
    public class Pagina
    {
        [Key]
        public int Pagina_Id { get; set; }
        public int Smenu_Id { get; set; }
        public virtual SottoMenu TestoSmenu { get; set; }
        [Display(Name ="Contenuto pagina")]
        public string Contenuo { get; set; }

    }

    public class MenuRuoli
    {
        [Key]
        public int MenuRuoli_Id { get; set; }
        public string Ruolo { get; set; }
        public int Menu_Id { get; set; }
        public virtual Menu TestoMenu { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SottoMenu> SottoMenus { get; set; }
        public DbSet<Pagina> Paginas { get; set; }
        public DbSet<MenuRuoli> MenuRuolis { get; set; }
    }
}