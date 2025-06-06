using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Partner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class PartnerController : Controller
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        public async Task<IActionResult> Index()
        {
            var partners = await _partnerService.GetAllAsync();
            return View(partners);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartnerCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _partnerService.CreateAsync(vm);
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
                var partner = await _partnerService.GetByIdAsync(id);
                var vm = new PartnerEditVM
                {
                    Id = partner.Id,
                    ExistingImg = partner.Img
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
        public async Task<IActionResult> Edit(PartnerEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _partnerService.EditAsync(vm);
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
                await _partnerService.DeleteAsync(id);
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
                var partner = await _partnerService.GetByIdAsync(id);
                if (partner == null) return NotFound("Partner not found!");

                return View(partner);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
