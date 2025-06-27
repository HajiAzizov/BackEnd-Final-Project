using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Blog;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        private string SaveImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "blog");

            // Əgər qovluq yoxdursa, yarat
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName);
            using FileStream stream = new(fullPath, FileMode.Create);
            file.CopyTo(stream);

            return fileName;
        }


        public async Task<List<BlogVM>> GetAllAsync()
        {
            return await _context.Blogs
                .Select(b => new BlogVM
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Img = b.Img
                }).ToListAsync();
        }

        public async Task<BlogVM> GetByIdAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) throw new Exception("Blog not found");

            return new BlogVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                Img = blog.Img
            };
        }

        public async Task CreateAsync(BlogCreateVM model)
        {
            Blog blog = new()
            {
                Title = model.Title,
                Description = model.Description,
                Img = SaveImage(model.Photo)
            };

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<BlogEditVM> GetEditByIdAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) throw new Exception("Blog not found");

            return new BlogEditVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description
                // Img əlavə olunmur! (sən istəməmişdin)
            };
        }

        public async Task EditAsync(BlogEditVM model)
        {
            var blog = await _context.Blogs.FindAsync(model.Id);
            if (blog == null) throw new Exception("Blog not found");

            blog.Title = model.Title;
            blog.Description = model.Description;

            if (model.Photo != null)
            {
                // Köhnə şəkli sil
                string oldPath = Path.Combine(_env.WebRootPath, "uploads", "blog", blog.Img);
                if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);

                // Yeni şəkli yüklə
                blog.Img = SaveImage(model.Photo);
            }

            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) throw new Exception("Blog not found");

            // Şəkli sil
            string path = Path.Combine(_env.WebRootPath, "uploads", "blog", blog.Img);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }
    }
}
