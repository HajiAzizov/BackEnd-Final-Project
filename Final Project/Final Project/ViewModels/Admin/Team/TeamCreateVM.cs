using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Team
{
    public class TeamCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public IFormFile Img { get; set; }
    }
}
