using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.Home
{
    public class BestSellingBookViewComponent : ViewComponent
    {
        private readonly IBestSellingBookService _bestSellingBookService;
        public BestSellingBookViewComponent(IBestSellingBookService bestSellingBookService)
        {
            _bestSellingBookService = bestSellingBookService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _bestSellingBookService.GetAllAsync();
            var book = books.FirstOrDefault();

            return View(book); 
        }


    }
}
