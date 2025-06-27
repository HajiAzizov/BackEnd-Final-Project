namespace Final_Project.ViewModels.Admin.Blog
{
    public class BlogEditVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile Photo { get; set; }
    }
}
