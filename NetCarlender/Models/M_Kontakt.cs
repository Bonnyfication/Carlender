using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetCarlender.Models
{
    public class M_Kontakt
    {
        [Required]
        [Display(Name = "Vorname:")]
        public string Vorname { get; set; }

        [Required]
        [Display(Name = "Nachname:")]
        public string Nachname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail:")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nachricht:")]
        public string Nachricht { get; set; }
    }
}