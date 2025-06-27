using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Genre
{
    public class GenreCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
