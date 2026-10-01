using Microsoft.EntityFrameworkCore;
using SiteWeb.Core.Entities;
using SiteWeb.Core.Interfaces;
using SiteWeb.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Infrastructure.Repositories
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactMessage?> GetByIdAsync(int id)
        {
            return await _context.ContactMessages.FindAsync(id);
        }

        public async Task<IReadOnlyList<ContactMessage>> GetAllAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(m => m.SentAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ContactMessage> AddAsync(ContactMessage message)
        {
            await _context.ContactMessages.AddAsync(message);
            return message;
        }

        public Task UpdateAsync(ContactMessage message)
        {
            _context.ContactMessages.Update(message);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                _context.ContactMessages.Remove(message);
            }
        }

        public async Task<int> GetUnreadCountAsync()
        {
            return await _context.ContactMessages.CountAsync(m => !m.IsRead);
        }

        public async Task<IReadOnlyList<ContactMessage>> GetRecentMessagesAsync(int count = 10)
        {
            return await _context.ContactMessages
                .OrderByDescending(m => m.SentAt)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }
    }

}
