namespace Final_Project.Models
{
    public class Subscriber : BaseEntity
    {
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmationToken { get; set; }
    }
}
