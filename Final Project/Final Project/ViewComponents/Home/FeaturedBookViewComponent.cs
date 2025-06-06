using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.Home
{
    public class FeaturedBookViewComponent : ViewComponent
    {
        private readonly IFeaturedBookService _featuredBookService;
        public FeaturedBookViewComponent(IFeaturedBookService featuredBookService)
        {
            _featuredBookService = featuredBookService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _featuredBookService.GetAllAsync();
            return View(books);
        }

    }
}
