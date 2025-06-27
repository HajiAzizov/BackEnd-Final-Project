using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Blog
{
    public class BlogCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
