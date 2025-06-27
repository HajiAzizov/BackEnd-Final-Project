using Final_Project.Models;
using Final_Project.ViewModels.User.Contact_Us;

namespace Final_Project.Services.Interfaces
{
    public interface IContactMessageService
    {
        Task CreateAsync(ContactMessageCreateVM model);
        Task<List<ContactMessage>> GetAllAsync();
        Task<ContactMessage> GetByIdAsync(int id);
        Task DeleteAsync(ContactMessage message);
    }
}
