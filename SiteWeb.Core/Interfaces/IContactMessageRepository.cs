using SiteWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Core.Interfaces
{
    public interface IContactMessageRepository
    {
        Task<ContactMessage?> GetByIdAsync(int id);
        Task<IReadOnlyList<ContactMessage>> GetAllAsync();
        Task<ContactMessage> AddAsync(ContactMessage message);
        Task UpdateAsync(ContactMessage message);
        Task DeleteAsync(int id);
        Task<int> GetUnreadCountAsync();
        Task<IReadOnlyList<ContactMessage>> GetRecentMessagesAsync(int count = 10);
    }
}
