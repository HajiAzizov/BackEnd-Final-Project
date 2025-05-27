using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile ImgFile { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
