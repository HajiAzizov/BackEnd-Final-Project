namespace Final_Project.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
        public ICollection<ProductAuthor> ProductAuthors { get; set; } = new List<ProductAuthor>();
        public ICollection<ProductGenre> ProductGenres { get; set; }
    }
}
