using AutoMapper;
using SiteWeb.Application.DTOs.Contact;
using SiteWeb.Application.Interfaces;
using SiteWeb.Core.Entities;
using SiteWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> SubmitContactFormAsync(ContactFormDto form)
        {
            var message = _mapper.Map<ContactMessage>(form);
            await _unitOfWork.ContactMessages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<ContactMessageDto>> GetAllMessagesAsync()
        {
            var messages = await _unitOfWork.ContactMessages.GetAllAsync();
            return _mapper.Map<IReadOnlyList<ContactMessageDto>>(messages);
        }

        public async Task<IReadOnlyList<ContactMessageDto>> GetRecentMessagesAsync(int count = 10)
        {
            var messages = await _unitOfWork.ContactMessages.GetRecentMessagesAsync(count);
            return _mapper.Map<IReadOnlyList<ContactMessageDto>>(messages);
        }

        public async Task<ContactMessageDto?> GetMessageByIdAsync(int id)
        {
            var message = await _unitOfWork.ContactMessages.GetByIdAsync(id);
            return message == null ? null : _mapper.Map<ContactMessageDto>(message);
        }

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var message = await _unitOfWork.ContactMessages.GetByIdAsync(id);
            if (message == null) return false;

            message.IsRead = true;
            await _unitOfWork.ContactMessages.UpdateAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            await _unitOfWork.ContactMessages.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetUnreadCountAsync()
        {
            return await _unitOfWork.ContactMessages.GetUnreadCountAsync();
        }
    }
}
