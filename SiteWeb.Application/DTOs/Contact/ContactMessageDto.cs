using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.DTOs.Contact
{
    public class ContactMessageDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? CompanyName { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? ProductReference { get; set; }
        public string? ProductName { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
