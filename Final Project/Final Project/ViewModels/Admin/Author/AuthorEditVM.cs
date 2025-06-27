using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Author
{
    public class AuthorEditVM
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public IFormFile ImgFile { get; set; }  

    }
}
