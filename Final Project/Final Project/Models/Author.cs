namespace Final_Project.Models
{
    public class Author : BaseEntity
    {
        public string FullName { get; set; }
        public string Img { get; set; }

        public ICollection<ProductAuthor> ProductAuthors { get; set; } = new List<ProductAuthor>();
    }
}
