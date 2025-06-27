using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ContactController : Controller
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _contactMessageService.GetAllAsync();
            return View(messages);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var message = await _contactMessageService.GetByIdAsync(id);
            if (message == null) return NotFound();

            return View(message);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _contactMessageService.GetByIdAsync(id);
            if (message == null) return NotFound();

            await _contactMessageService.DeleteAsync(message);
            return RedirectToAction(nameof(Index));
        }


    }
}
