using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AvansFestivals.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Voornaam")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "Leeftijd")]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Gebruikersnaam")]
        [System.Web.Mvc.Remote("CheckAvailabilty", "Account", HttpMethod = "POST", ErrorMessage = "Gebruikersnaam is al in gebruik. Voer een andere gebruikersnaam in a.u.b.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "De {0} moet minstens {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "Wachtwoord en bevistig wachtwoord komen niet met elkaar overeen.")]
        public string ConfirmPassword { get; set; }
    }
}