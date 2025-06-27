using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.User.Contact_Us;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly AppDbContext _context;

        public ContactMessageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ContactMessageCreateVM model)
        {
            var entity = new ContactMessage
            {
                FullName = model.FullName,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
                CreatedAt = DateTime.Now
            };

            _context.ContactMessages.Add(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<List<ContactMessage>> GetAllAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
        public async Task<ContactMessage> GetByIdAsync(int id)
        {
            return await _context.ContactMessages.FindAsync(id);
        }
        public async Task DeleteAsync(ContactMessage message)
        {
            _context.ContactMessages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}
