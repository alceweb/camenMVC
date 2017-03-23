using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CamenMVC.Models
{
    public class ContactViewModels
    {
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Indirizzo")]
        public string Indirizzo { get; set; }
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        [Display(Name = "Professione")]
        public string Professione { get; set; }
        [Display(Name = "Organizzazione di appartenenza")]
        public string Organizzazione { get; set; }
        [Required]
        [Display(Name = "Richiesta")]
        public string Richiesta { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "Devi accettare il documento della privacy.")]
        [Display(Name = "Informativa privacy")]
        public bool Privacy { get; set; }

    }
}