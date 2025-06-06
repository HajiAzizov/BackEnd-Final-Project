using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.About;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.About
{
    public class AboutViewComponent : ViewComponent
    {
      private readonly IAboutService _aboutService;
        public AboutViewComponent(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<AboutVM> about = await _aboutService.GetAllAsync();
            return await Task.FromResult(View(about));
        }
    }
}
