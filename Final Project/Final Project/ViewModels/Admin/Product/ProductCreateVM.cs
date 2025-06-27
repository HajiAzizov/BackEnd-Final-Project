using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
        [Required]
        public IFormFile ImgFile { get; set; }

        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
