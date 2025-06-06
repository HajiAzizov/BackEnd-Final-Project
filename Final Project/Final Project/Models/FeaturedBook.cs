namespace Final_Project.Models
{
    public class FeaturedBook : BaseEntity
    {
        public string Img { get; set; }    
        public string Title { get; set; }   
        public string Author { get; set; }  
        public decimal Price { get; set; }
    }
}
