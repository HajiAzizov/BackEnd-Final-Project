using Final_Project.ViewModels.User.Contact_Us;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.Contact
{
    public class ContactViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new ContactMessageCreateVM());
        }
    }

}
