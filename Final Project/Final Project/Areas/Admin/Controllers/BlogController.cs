using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();
            return View(blogs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _blogService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var blog = await _blogService.GetEditByIdAsync(id);
                return View(blog);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _blogService.EditAsync(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _blogService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var blog = await _blogService.GetByIdAsync(id);
                return View(blog);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
