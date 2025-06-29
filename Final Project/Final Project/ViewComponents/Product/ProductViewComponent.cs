using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.User.UIProduct;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.Product
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        public ProductViewComponent(IProductService productService, IGenreService genreService, IAuthorService authorService)
        {
            _productService = productService;
            _genreService = genreService;
            _authorService = authorService; 
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetAllAsync();
            var genres = await _genreService.GetAllAsync();
            var authors = await _authorService.GetAllAsync();

            var vm = new UIProductVM
            {
                Products= products,
                Genres = genres,
                Authors = authors,
            };

            return View(vm);

        }

    }
}
