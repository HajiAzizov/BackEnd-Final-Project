using Final_Project.ViewModels.Admin.Genre;
using Final_Project.ViewModels.Admin.Product;

namespace Final_Project.ViewModels.Admin.PopularBooksVM
{
    public class PopularBooksVM
    {
        public List<GenreVM> Genres { get; set; }
        public List<ProductVM> Products { get; set; }
    }
}
