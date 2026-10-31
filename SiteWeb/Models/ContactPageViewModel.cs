using SiteWeb.Application.DTOs.Contact;

namespace SiteWeb.Models
{
    public class ContactPageViewModel
    {
        public ContactFormDto Form { get; set; } = new ContactFormDto();
        public bool IsSuccess { get; set; } = false;
        public string? SuccessMessage { get; set; }
        public string? PreFilledProductName { get; set; }
        public string? PreFilledProductReference { get; set; }
    }
}
