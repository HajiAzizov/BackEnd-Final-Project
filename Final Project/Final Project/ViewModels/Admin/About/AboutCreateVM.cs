using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.About
{
    public class AboutCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public IFormFile Img { get; set; }
    }
}
