using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.BestSellingBook
{
    public class BestSellingBookEditVM
    {
        public int Id { get; set; }

        public IFormFile Img { get; set; }
        public string Image { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
