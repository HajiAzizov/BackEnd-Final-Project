using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
