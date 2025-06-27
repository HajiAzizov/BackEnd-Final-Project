namespace Final_Project.ViewModels.Admin.Blog
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}
