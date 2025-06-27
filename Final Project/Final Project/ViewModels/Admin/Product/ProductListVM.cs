namespace Final_Project.ViewModels.Admin.Product
{
    public class ProductListVM
    {
        public List<ProductVM> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
