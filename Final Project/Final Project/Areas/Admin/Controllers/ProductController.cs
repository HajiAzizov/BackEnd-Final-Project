using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAuthorService _authorService;
        public ProductController(IProductService productService, IAuthorService authorService)
        {
            _productService = productService;
            _authorService = authorService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
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

            var productDb = await _productService.GetByIdAsync(id);

            var editVM = new ProductEditVM
            {
                Id = productDb.Id,
                Name = productDb.Name,
                Price = productDb.Price,
                AuthorIds = productDb.Authors != null
                    ? (await _authorService.GetAllAsync())
                        .Where(a => productDb.Authors.Contains(a.FullName))
                        .Select(a => a.Id).ToList()
                    : new List<int>()
            };

            ViewBag.Authors = await _authorService.GetAllAsync();
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
