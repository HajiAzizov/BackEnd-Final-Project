using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.User.Contact_Us;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageService _service;

        public ContactController(IContactMessageService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactMessageCreateVM model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            await _service.CreateAsync(model);

            TempData["Success"] = "Message sent successfully";
            return RedirectToAction("Index");
        }

    }
}
