using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Partner
{
    public class PartnerCreateVM
    {
        [Required]
        public IFormFile ImgFile { get; set; }
    }
}
