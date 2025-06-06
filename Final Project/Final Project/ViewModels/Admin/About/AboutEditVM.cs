using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.About
{
    public class AboutEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Desc { get; set; }

        public IFormFile Img { get; set; }
    }
}
