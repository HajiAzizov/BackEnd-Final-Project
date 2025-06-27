using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Author
{
    public class AuthorCreateVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public IFormFile ImgFile { get; set; }
    }
}
