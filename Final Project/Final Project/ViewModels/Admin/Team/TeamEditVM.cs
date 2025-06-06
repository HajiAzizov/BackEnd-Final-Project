using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Team
{
    public class TeamEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        public IFormFile Img { get; set; }
    }
}
