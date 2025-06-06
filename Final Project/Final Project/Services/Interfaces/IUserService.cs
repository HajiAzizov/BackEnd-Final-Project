using Final_Project.Models;

namespace Final_Project.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUserRoleAsync(AppUser user, string role);
        Task<bool> RemoveUserRoleAsync(AppUser user, string role);
        Task<AppUser?> GetUserByIdAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
        Task<IList<AppUser>> GetAllUsersAsync();
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
