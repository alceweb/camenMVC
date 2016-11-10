using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CamenMVC.Models
{
    public class MenuViewModel
    {
        [Key]
        public int Menu_Id { get; set; }
        [Display(Name = "Posizione")]
        public int Posizione { get; set; }
        [Display(Name = "Testo menu")]
        public string TestoMenu { get; set; }
        [Display(Name = "Pubblica")]
        public bool Pubblica { get; set; }

    }
    public class SottoMenuViewModel
    {
        [Key]
        public int Smenu_Id { get; set; }
        public int Menu_Id { get; set; }
        public virtual Menu TestoMenu { get; set; }
        [Display(Name = "Testo sottomenu")]
        public string TestoSmenu { get; set; }
        [Display(Name = "Pubblica")]
        public bool Pubblica { get; set; }

    }
}