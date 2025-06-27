using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.Home
{
    public class HomeBlogViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;
        public HomeBlogViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _blogService.GetAllAsync();
            return View(blogs.Take(3));
        }

    }
}
