using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.BestSellingBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BestSellingBookController : Controller
    {
        private readonly IBestSellingBookService _service;

        public BestSellingBookController(IBestSellingBookService service)
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
        public async Task<IActionResult> Create(BestSellingBookCreateVM vm)
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

            var vm = new BestSellingBookEditVM
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Price = book.Price
            };

            ViewData["CurrentImg"] = book.Img;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BestSellingBookEditVM vm)
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

