using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamenMVC.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
    public class EditUsViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }
    }

    public class EditEventiViewModel
    {
        [Key]
        public int Evento_Id { get; set; }
        [Display(Name = "Evento")]
        public string Evento { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
    public class EditDocViewModel
    {
        [Key]
        public int Documento_Id { get; set; }
        [Display(Name = "Categoria")]
        public int Categoria_Id { get; set; }
        public virtual Categorie Categoria { get; set; }
        [Display(Name = "Evento")]
        public int Evento_Id { get; set; }
        public virtual Eventi Evento { get; set; }
        [Display(Name = "Area")]
        public int Linea_Id { get; set; }
        public virtual Linee Linea { get; set; }
        [Display(Name = "Sessione")]
        public int Sessione_Id { get; set; }
        public virtual Sessioni Sessione { get; set; }
        [Display(Name = "Oratore")]
        public string Oratore { get; set; }
        [Display(Name = "Titolo")]
        public string Titolo { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
        [Display(Name = "Data di pubblicazione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        [Display(Name = "Riferimento")]
        public string Riferimento { get; set; }
        [Display(Name = "Lingua")]
        public string Lingua { get; set; }
        [Display(Name = "Nome file")]
        [Required]
        public string NomeFile { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }

}