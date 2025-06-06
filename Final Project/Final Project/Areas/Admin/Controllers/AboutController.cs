using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.About;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();
            return View(abouts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _aboutService.CreateAsync(vm);
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
            try
            {
                var about = await _aboutService.GetByIdAsync(id);
                if (about == null) return NotFound("About not found");

                var vm = new AboutEditVM
                {
                    Id = about.Id,
                    Title = about.Title,
                    Desc = about.Desc,
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AboutEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _aboutService.EditAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _aboutService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var about = await _aboutService.GetByIdAsync(id);
                if (about == null) return NotFound("About not found!");

                return View(about);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
