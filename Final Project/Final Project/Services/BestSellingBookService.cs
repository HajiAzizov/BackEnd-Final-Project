using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.BestSellingBook;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace Final_Project.Services
{
    public class BestSellingBookService : IBestSellingBookService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BestSellingBookService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<BestSellingBookVM>> GetAllAsync()
        {
            return await _context.BestSellingBooks
                .Select(b => new BestSellingBookVM
                {
                    Id = b.Id,
                    Img = b.Img,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    Price = b.Price
                }).ToListAsync();
        }

        public async Task<BestSellingBookVM> GetByIdAsync(int id)
        {
            var book = await _context.BestSellingBooks.FindAsync(id);
            if (book == null)
                throw new Exception("BestSellingBook not found");

            return new BestSellingBookVM
            {
                Id = book.Id,
                Img = book.Img,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Price = book.Price
            };
        }

        public async Task CreateAsync(BestSellingBookCreateVM model)
        {
            var allBooks = await _context.BestSellingBooks.ToListAsync();
            if (allBooks.Any())
            {
                _context.BestSellingBooks.RemoveRange(allBooks);
                await _context.SaveChangesAsync();
            }
            if (model.Img == null)
                throw new Exception("Image is required");

            string fileName = await SaveFileAsync(model.Img);

            var entity = new BestSellingBook
            {
                Img = fileName,
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Price = model.Price
            };

            await _context.BestSellingBooks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BestSellingBookEditVM model)
        {
            var entity = await _context.BestSellingBooks.FindAsync(model.Id);
            if (entity == null)
                throw new Exception("BestSellingBook not found");

            if (model.Img != null)
            {
                DeleteFile(entity.Img);
                entity.Img = await SaveFileAsync(model.Img);
            }

            entity.Title = model.Title;
            entity.Author = model.Author;
            entity.Description = model.Description;
            entity.Price = model.Price;

            _context.BestSellingBooks.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.BestSellingBooks.FindAsync(id);
            if (entity == null)
                throw new Exception("BestSellingBook not found");

            DeleteFile(entity.Img);
            _context.BestSellingBooks.Remove(entity);
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
