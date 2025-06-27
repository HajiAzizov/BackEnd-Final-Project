using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.User.Contact_Us
{
    public class ContactMessageCreateVM
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
