using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.FuturedBook
{
    public class FeaturedBookCreateVM
    {
        [Required]
        public IFormFile Img { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
