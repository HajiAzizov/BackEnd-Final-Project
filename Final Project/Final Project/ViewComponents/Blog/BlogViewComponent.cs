using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.Blog
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public BlogViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<BlogVM> blog = await _blogService.GetAllAsync();
            return await Task.FromResult(View(blog));

        }
    }
}
