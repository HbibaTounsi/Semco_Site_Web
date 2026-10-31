using Microsoft.AspNetCore.Mvc;
using SiteWeb.Application.DTOs.Contact;
using SiteWeb.Application.Interfaces;
using SiteWeb.Models;

namespace SiteWeb.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index(string? productRef, string? productName)
        {
            var viewModel = new ContactPageViewModel
            {
                PreFilledProductReference = productRef,
                PreFilledProductName = productName,
                Form = new ContactFormDto
                {
                    ProductReference = productRef,
                    ProductName = productName,
                    Subject = !string.IsNullOrEmpty(productName)
                        ? $"Demande d'information / devis pour : {productName} (Réf: {productRef})"
                        : string.Empty
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _contactService.SubmitContactFormAsync(model.Form);

                var successModel = new ContactPageViewModel
                {
                    IsSuccess = true,
                    SuccessMessage = "Votre message a été transmis avec succès à l'équipe commerciale SEMCO. Nous vous répondrons dans les plus brefs délais.",
                    Form = new ContactFormDto() // Clear form
                };

                return View(successModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Une erreur s'est produite lors de l'envoi de votre message. Veuillez réessayer ou nous contacter directement par téléphone ou WhatsApp.");
                return View(model);
            }
        }
    }
}
