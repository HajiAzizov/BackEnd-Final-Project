using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.About
{
    public class AboutPartnerViewComponent : ViewComponent
    {
        private readonly IPartnerService _partnerService;
        public AboutPartnerViewComponent(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var partners = await _partnerService.GetAllAsync();
            return View(partners);
        }
    }
}
