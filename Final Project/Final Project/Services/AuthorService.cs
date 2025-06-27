using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Author;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AuthorService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<AuthorVM>> GetAllAsync()
        {
            var authors = await _context.Authors.ToListAsync();

            return authors.Select(a => new AuthorVM
            {
                Id = a.Id,
                FullName = a.FullName,
                Img = a.Img
            }).ToList();
        }

        public async Task<AuthorVM> GetByIdAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null) throw new Exception("Author not found");

            return new AuthorVM
            {
                Id = author.Id,
                FullName = author.FullName,
                Img = author.Img
            };
        }

        public async Task CreateAsync(AuthorCreateVM model)
        {
            string fileName = null;
            if (model.ImgFile != null)
            {
                fileName = SaveImage(model.ImgFile);
            }

            var author = new Author
            {
                FullName = model.FullName,
                Img = fileName
            };

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, AuthorEditVM model)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null) throw new Exception("Author not found");

            if (model.ImgFile != null)
            {
                string newFileName = SaveImage(model.ImgFile);

                // köhnə şəkli sil
                if (!string.IsNullOrEmpty(author.Img))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "uploads", "authors", author.Img);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                author.Img = newFileName;
            }

            author.FullName = model.FullName;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null) throw new Exception("Author not found");

            if (!string.IsNullOrEmpty(author.Img))
            {
                string path = Path.Combine(_env.WebRootPath, "uploads", "authors", author.Img);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        private string SaveImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "authors");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName);
            using FileStream stream = new(fullPath, FileMode.Create);
            file.CopyTo(stream);

            return fileName;
        }
    }
}
