using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Slider
{
    public class SliderEditVM
    {
        public int Id { get; set; }
        public string ExistingImg { get; set; }
        public IFormFile ImgFile { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
