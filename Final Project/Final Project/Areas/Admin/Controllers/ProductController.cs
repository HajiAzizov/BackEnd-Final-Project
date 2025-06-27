using Final_Project.Services;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Author;
using Final_Project.ViewModels.Admin.Genre;
using Final_Project.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
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
        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 10;
            var totalCount = await _productService.GetCountAsync();
            var products = await _productService.GetPagedAsync(page, pageSize);

            var model = new ProductListVM
            {
                Products = products,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            var authors = await _authorService.GetAllAsync() ?? new List<AuthorVM>();
            var genres = await _genreService.GetAllAsync() ?? new List<GenreVM>();

            ViewBag.Authors = authors;
            ViewBag.Genres = genres;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                return View(model);
            }

            await _productService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            var authors = await _authorService.GetAllAsync();
            var genres = await _genreService.GetAllAsync();

            var editVM = new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                AuthorIds = authors.Where(a => product.Authors.Contains(a.FullName)).Select(a => a.Id).ToList(),
                GenreIds = genres.Where(g => product.Genres.Contains(g.Name)).Select(g => g.Id).ToList()  // janr Id-lərini seçirik
            };

            ViewBag.Authors = authors;
            ViewBag.Genres = genres;

            return View(editVM);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductEditVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                return View(model);
            }

            await _productService.EditAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }
    }
}
