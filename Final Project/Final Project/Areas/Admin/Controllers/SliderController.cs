using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderController(ISliderService sliderService, IWebHostEnvironment webHostEnvironment)
        {
            _sliderService = sliderService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            return View(sliders);
        }
        // GET: /Slider/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Slider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _sliderService.CreateAsync(vm);
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
                var slider = await _sliderService.GetByIdAsync(id);
                var vm = new SliderEditVM
                {
                    Id = slider.Id,
                    Title = slider.Title,
                    Description = slider.Description,
                    ExistingImg = slider.Img
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: /Slider/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _sliderService.EditAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var slider = await _sliderService.GetByIdAsync(id);
                if (slider == null) return NotFound();

                return View(slider);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sliderService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
