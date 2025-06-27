using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.Product
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public ProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetAllAsync();
        return View(products);}

    }
}
