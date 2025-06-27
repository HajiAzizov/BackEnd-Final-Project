using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Genre
{
    public class GenreEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
