using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.Home
{
    public class QuoteViewComponent : ViewComponent
    {
        private readonly IQuoteService _quoteService;
        public QuoteViewComponent(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var quotes = await _quoteService.GetAllAsync();
            return View(quotes);    
        }
    }
}
