using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.DTOs.Contact
{
    public class ContactFormDto
    {
        [Required(ErrorMessage = "Le nom complet est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'adresse email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Veuillez fournir une adresse email valide.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Veuillez fournir un numéro de téléphone valide.")]
        public string? PhoneNumber { get; set; }

        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Le sujet est obligatoire.")]
        [StringLength(150, ErrorMessage = "Le sujet ne doit pas dépasser 150 caractères.")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le message est obligatoire.")]
        [MinLength(10, ErrorMessage = "Le message doit contenir au moins 10 caractères.")]
        public string Message { get; set; } = string.Empty;

        public string? ProductReference { get; set; }
        public string? ProductName { get; set; }
    }
}
