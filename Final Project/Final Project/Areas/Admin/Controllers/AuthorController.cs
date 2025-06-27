using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _authorService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();

            var editVM = new AuthorEditVM
            {
                Id = author.Id,
                FullName = author.FullName
            };

            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AuthorEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _authorService.EditAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();

            return View(author);
        }
    }
}
