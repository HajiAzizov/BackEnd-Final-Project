using Microsoft.AspNetCore.Identity;

namespace Final_Project.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
