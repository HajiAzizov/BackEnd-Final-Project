﻿namespace Final_Project.Models
{
    public class BestSellingBook : BaseEntity
    {
        public string Img { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
