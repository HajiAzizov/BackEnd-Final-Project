using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.FuturedBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class FeaturedBookController : Controller
    {

        private readonly IFeaturedBookService _service;

        public FeaturedBookController(IFeaturedBookService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _service.GetAllAsync();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeaturedBookCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _service.CreateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _service.GetByIdAsync(id);
            if (book == null) return NotFound();

            var vm = new FeaturedBookEditVM
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FeaturedBookEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _service.UpdateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            var book = await _service.GetByIdAsync(id);
            if (book == null) return NotFound();

            return View(book);
        }
    }
}
