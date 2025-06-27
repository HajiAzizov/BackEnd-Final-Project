namespace Final_Project.ViewModels.User.Basket
{
    public class BasketItemVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<string> Genres { get; set; } = new();

        public decimal Total => Price * Quantity;
    }
}
