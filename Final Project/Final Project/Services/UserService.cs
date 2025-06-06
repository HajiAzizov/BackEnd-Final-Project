using MailKit.Net.Smtp;
using MimeKit;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Final_Project.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> AddUserRoleAsync(AppUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> RemoveUserRoleAsync(AppUser user, string role)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<AppUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IList<string>> GetUserRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IList<AppUser>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }

        // Burada email göndərmə metodu
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("YourAppName", "yourappemail@example.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.server.com", 587, false); // SMTP serverini özün yazacaqsan
            await client.AuthenticateAsync("yourappemail@example.com", "emailpassword"); // Öz mail və şifrən
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
