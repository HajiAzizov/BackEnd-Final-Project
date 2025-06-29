using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
