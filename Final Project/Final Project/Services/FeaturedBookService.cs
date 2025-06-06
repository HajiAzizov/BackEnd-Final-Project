using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.FuturedBook;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class FeaturedBookService : IFeaturedBookService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FeaturedBookService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<FeaturedBookVM>> GetAllAsync()
        {
            return await _context.FeaturedBooks
                .Select(b => new FeaturedBookVM
                {
                    Id = b.Id,
                    Img = b.Img,
                    Title = b.Title,
                    Author = b.Author,
                    Price = b.Price
                }).ToListAsync();
        }

        public async Task<FeaturedBookVM> GetByIdAsync(int id)
        {
            var book = await _context.FeaturedBooks.FindAsync(id);
            if (book == null)
                throw new Exception("Featured book not found");

            return new FeaturedBookVM
            {
                Id = book.Id,
                Img = book.Img,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price
            };
        }

        public async Task CreateAsync(FeaturedBookCreateVM model)
        {
            if (model.Img == null)
                throw new Exception("Image is required");

            string fileName = await SaveFileAsync(model.Img);

            var entity = new FeaturedBook
            {
                Img = fileName,
                Title = model.Title,
                Author = model.Author,
                Price = model.Price
            };

            await _context.FeaturedBooks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FeaturedBookEditVM model)
        {
            var entity = await _context.FeaturedBooks.FindAsync(model.Id);
            if (entity == null)
                throw new Exception("Featured book not found");

            if (model.Img != null)
            {
                DeleteFile(entity.Img);
                entity.Img = await SaveFileAsync(model.Img);
            }

            entity.Title = model.Title;
            entity.Author = model.Author;
            entity.Price = model.Price;

            _context.FeaturedBooks.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FeaturedBooks.FindAsync(id);
            if (entity == null)
                throw new Exception("Featured book not found");

            DeleteFile(entity.Img);
            _context.FeaturedBooks.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "books");
            Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Path.Combine("uploads/books", fileName).Replace("\\", "/");
        }

        private void DeleteFile(string fileName)
        {
            string path = Path.Combine(_env.WebRootPath, fileName);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
    }
}
