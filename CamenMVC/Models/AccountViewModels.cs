﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CamenMVC.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Ricordami")]
        public bool RememberMe { get; set; }

        [Display(Name = "Bloccato")]
        public bool Bloccato { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Nome")]
        public string Nome { get; set; }
        [Required]
        [Display(Name ="Cognome")]
        public string Cognome { get; set; }
        [Required]
        [Display(Name ="Indirizzo")]
        public string Indirizzo { get; set; }
        [Display(Name = "Città")]
        public string Città { get; set; }
        [Display(Name = "CAP")]
        public string CAP { get; set; }
        [Display(Name ="Telefono")]
        public string Telefono { get; set; }
        [Display(Name ="Professione")]
        public string Professione { get; set; }
        [Display(Name ="Organizzazione di appartenenza")]
        public string Organizzazione { get; set; }
        [Display(Name = "Bloccato")]
        public bool Bloccato { get; set; }
        [Display(Name="Note alla registrazione")]
        public string Note { get; set; }
        [Required]
        [Display(Name = "Io sono")]
        public string Ruolo { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "Devi accettare il documento della privacy.")]
        [Display(Name = "Informativa privacy")]
        public bool Privacy { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email/UserName")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma password")]
        [Compare("Password", ErrorMessage = "La password e conferma password non corrispondono.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
