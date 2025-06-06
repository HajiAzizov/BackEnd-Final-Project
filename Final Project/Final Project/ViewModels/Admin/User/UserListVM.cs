using Final_Project.Models;

namespace Final_Project.ViewModels.Admin.User
{
    public class UserListVM
    {
        public IList<AppUser> Users { get; set; }
        public Dictionary<string, IList<string>> UserRoles { get; set; }
    }
}
