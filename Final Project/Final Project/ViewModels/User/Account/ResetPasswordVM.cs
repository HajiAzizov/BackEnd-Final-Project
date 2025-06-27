using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.User.Account
{
    public class ResetPasswordVM
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Compare("NewPassword"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
