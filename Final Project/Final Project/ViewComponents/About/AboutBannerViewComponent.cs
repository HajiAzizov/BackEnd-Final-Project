using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.About
{
    public class AboutBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }   
    }
}
