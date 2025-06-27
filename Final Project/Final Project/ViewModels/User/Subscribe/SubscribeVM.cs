using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.User.Subscribe
{
    public class SubscribeVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
