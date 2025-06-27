using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService) 
        {
            _blogService = blogService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _blogService.GetByIdAsync(id); // await vacibdir!
            if (blog == null) return NotFound();

            return View(blog); // Views/Blog/Detail.cshtml
        }
    }
}

