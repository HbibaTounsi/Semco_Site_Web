using SiteWeb.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.Interfaces
{
    public interface IContactService
    {
        Task<bool> SubmitContactFormAsync(ContactFormDto form);
        Task<IReadOnlyList<ContactMessageDto>> GetAllMessagesAsync();
        Task<IReadOnlyList<ContactMessageDto>> GetRecentMessagesAsync(int count = 10);
        Task<ContactMessageDto?> GetMessageByIdAsync(int id);
        Task<bool> MarkAsReadAsync(int id);
        Task<bool> DeleteMessageAsync(int id);
        Task<int> GetUnreadCountAsync();
    }

}
