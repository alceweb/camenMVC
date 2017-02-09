using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamenMVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name ="Cognome")]
        public string Cognome { get; set; }
        [Display(Name ="Informativa privacy")]
        public bool Privacy { get; set; }

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
        public virtual ICollection<MenuRuoli> MenuRuoli { get; set; }
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
        [Display(Name = "Posizione")]
        public int Posizione { get; set; }
        public virtual ICollection<Pagina> Paginas { get; set; }

    }
    public class Pagina
    {
        [Key]
        public int Pagina_Id { get; set; }
        public int Smenu_Id { get; set; }
        public virtual SottoMenu TestoSmenu { get; set; }
        [Display(Name ="Contenuto pagina")]
        public string Contenuo { get; set; }
        [Display(Name = "Posizione")]
        public int Posizione { get; set; }

    }

    public class MenuRuoli
    {
        [Key]
        public int MenuRuoli_Id { get; set; }
        public string Ruolo { get; set; }
        public int Menu_Id { get; set; }
        public virtual Menu TestoMenu { get; set; }

    }

    public class Splash
    {
        [Key]
        public int Splash_Id { get; set; }
        [Display(Name ="Titolo")]
        public string Titolo { get; set; }
        [Display(Name = "Sotto titolo")]
        public string SottoTitolo { get; set; }
        [Display(Name = "Testo")]
        public string Testo { get; set; }
        [Display(Name = "Data inizio pubblicazione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataI { get; set; }
        [Display(Name = "Data fine pubblicazione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataF { get; set; }
    }

    public class Categorie
    {
        [Key]
        public int Categoria_Id { get; set; }
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }
        public virtual ICollection<Documenti> Documentis { get; set; }
    }
    public class Eventi
    {
        [Key]
        public int Evento_Id { get; set; }
        [Display(Name = "Evento")]
        public string Evento { get; set; }
        public virtual ICollection<Documenti> Documentis { get; set; }
    }
    public class Linee
    {
        [Key]
        public int Linea_Id { get; set; }
        [Display(Name = "Area")]
        public string Linea { get; set; }
        public virtual ICollection<Documenti> Documentis { get; set; }

    }
    public class Sessioni
    {
        [Key]
        public int Sessione_Id { get; set; }
        [Display(Name = "Sessione")]
        public string Sessione { get; set; }
        public virtual ICollection<Documenti> Documentis { get; set; }

    }

    public class Documenti
    {
        [Key]
        public int Documento_Id { get; set; }
        [Display(Name = "Categoria")]
        public int Categoria_Id { get; set; }
        public virtual Categorie Categoria { get; set; }
        [Display(Name = "Evento")]
        public int Evento_Id { get; set; }
        public virtual Eventi Evento { get; set; }
        [Display(Name = "Linea")]
        public int Linea_Id { get; set; }
        public virtual Linee Linea { get; set; }
        [Display(Name = "Sessione")]
        public int Sessione_Id { get; set; }
        public virtual Sessioni Sessione { get; set; }
        [Display(Name ="Oratore")]
        public string Oratore { get; set; }
        [Display(Name="Titolo")]
        public string Titolo { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
        [Display(Name ="Data di pubblicazione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        [Display(Name ="Riferimento")]
        public string Riferimento { get; set; }
        [Display(Name ="Lingua")]
        public string Lingua { get; set; }
        [Display(Name = "Nome file")]
        [Required]
        public string NomeFile { get; set; }
        public virtual ICollection<DocRuoli> DocRuolis { get; set; }
    }

    public class DocRuoli
    {
        [Key]
        public int DocRuoli_Id { get; set; }
        public int Documento_Id { get; set; }
        public virtual Documenti Documento { get; set; }
        public string RuoloId { get; set; }

    }
    public class EventiRuoli
    {
        [Key]
        public int EventoRuoliId { get; set; }
        public int Evento_Id { get; set; }
        public virtual Eventi Evento { get; set; }
        public string RuoloId { get; set; }
        
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
        public DbSet<Splash> Splashs { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Eventi> Eventis { get; set; }
        public DbSet<Linee> Linees { get; set; }
        public DbSet<Sessioni> Sessionis { get; set; }
        public DbSet<Documenti> Documentis { get; set; }
        public DbSet<EventiRuoli> EventiRuolis { get; set; }
        public DbSet<DocRuoli> DocRuolis { get; set; }
    }
}