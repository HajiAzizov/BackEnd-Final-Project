using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Final_Project.ViewComponents
{
    public class SubscribeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(); 
        }
    }
}
