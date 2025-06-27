namespace Final_Project.ViewModels.Admin.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Genres { get; set; } = new();


    }

}
