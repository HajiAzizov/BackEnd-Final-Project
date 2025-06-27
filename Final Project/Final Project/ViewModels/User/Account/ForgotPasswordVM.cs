using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.User.Account
{
    public class ForgotPasswordVM
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
