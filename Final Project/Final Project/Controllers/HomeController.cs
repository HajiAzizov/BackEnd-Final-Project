using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int? genreId)
        {
            return View(genreId);
        }

    }
}
