using Final_Project.Models;
using Final_Project.ViewModels.Admin.Author;
using Final_Project.ViewModels.Admin.Genre;
using Final_Project.ViewModels.Admin.Product;

namespace Final_Project.ViewModels.User.UIProduct
{
    public class UIProductVM
    {
        public List<GenreVM>Genres { get; set; }
        public List<ProductVM> Products { get; set; }
        public List<AuthorVM> Authors { get; set; }
        public string Search { get; set; }

    }
}
