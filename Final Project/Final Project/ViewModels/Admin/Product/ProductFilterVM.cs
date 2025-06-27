using Final_Project.ViewModels.Admin.Author;
using Final_Project.ViewModels.Admin.Genre;

namespace Final_Project.ViewModels.Admin.Product
{
    public class ProductFilterVM
    {
        public List<ProductVM> Products { get; set; } = new();
        public List<GenreVM> Genres { get; set; } = new();
        public List<AuthorVM> Authors { get; set; } = new();

        public List<int> SelectedGenreIds { get; set; } = new();
        public List<int> SelectedAuthorIds { get; set; } = new();
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
