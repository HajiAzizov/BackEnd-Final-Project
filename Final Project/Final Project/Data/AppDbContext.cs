using Final_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<FeaturedBook> FeaturedBooks { get; set; }
        public DbSet<BestSellingBook> BestSellingBooks { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ProductAuthor> ProductAuthors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductAuthor>()
                .HasKey(pa => new { pa.ProductId, pa.AuthorId });

            modelBuilder.Entity<ProductAuthor>()
                .HasOne(pa => pa.Product)
                .WithMany(p => p.ProductAuthors)
                .HasForeignKey(pa => pa.ProductId);

            modelBuilder.Entity<ProductAuthor>()
                .HasOne(pa => pa.Author)
                .WithMany(a => a.ProductAuthors)
                .HasForeignKey(pa => pa.AuthorId);
        }
    }
}
