namespace Final_Project.Models
{
    public class ProductAuthor
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
