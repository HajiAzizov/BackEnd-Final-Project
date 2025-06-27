using System.ComponentModel.DataAnnotations;

namespace Final_Project.ViewModels.Admin.Quote
{
    public class QuoteEditVM
    {

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

    }
}
