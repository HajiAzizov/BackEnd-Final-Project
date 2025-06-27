namespace Final_Project.Models
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
