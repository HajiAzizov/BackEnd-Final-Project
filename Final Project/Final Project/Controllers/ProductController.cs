using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Product;
using Final_Project.ViewModels.User.UIProduct;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;

        public ProductController(IProductService productService, IAuthorService authorService, IGenreService genreService)
        {
            _productService = productService;
            _authorService = authorService;
            _genreService = genreService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var productsSearchVM = await _productService.LiveSearchAsync(search);

            var productsVM = productsSearchVM.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Img = p.Img,
                Price = p.Price,
                Authors = p.Authors
            }).ToList();

            var model = new UIProductVM
            {
                Products = productsVM,
                Authors = await _authorService.GetAllAsync(),
                Genres = await _genreService.GetAllAsync(),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string term)
        {
            var results = await _productService.LiveSearchAsync(term);
            return Ok(results);
        }
        [HttpGet]
        public async Task<IActionResult> LoadMore(int skip)
        {
            int take = 6;

            var products = await _productService.GetPagedAsync(skip / take + 1, take);

            var result = products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Img,
                Authors = string.Join(", ", p.Authors)
            });

            return Json(result);
        }

    }
}
