using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.PopularBooksVM;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Final_Project.ViewComponents.Home
{
    public class PopularBookViewComponent : ViewComponent
    {
         private readonly IProductService _productService;
        private readonly IGenreService _genreService;

        public PopularBookViewComponent(IProductService productService, IGenreService genreService)
        {
            _productService = productService;
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var popbooks = await _productService.GetAllAsync();
            var genres = await _genreService.GetAllAsync();

            var model = new PopularBooksVM
            {
                Genres = genres,
                Products = popbooks.Take(8).ToList()
            };

            return View(model);
        }
    }
}
