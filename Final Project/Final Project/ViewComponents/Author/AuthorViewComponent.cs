using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.Author
{
    public class AuthorViewComponent : ViewComponent
    {
        private readonly IAuthorService _authorService;
        public AuthorViewComponent(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }

    }
}
