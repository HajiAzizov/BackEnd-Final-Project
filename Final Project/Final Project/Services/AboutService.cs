using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.About;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<AboutVM>> GetAllAsync()
        {
            return await _context.Abouts
                .Select(a => new AboutVM
                {
                    Id = a.Id,
                    Title = a.Title,
                    Desc = a.Desc,
                    Img = a.Img
                })
                .ToListAsync();
        }

        public async Task<AboutVM> GetByIdAsync(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about == null) throw new Exception("About not found");

            return new AboutVM
            {
                Id = about.Id,
                Title = about.Title,
                Desc = about.Desc,
                Img = about.Img
            };
        }

        public async Task CreateAsync(AboutCreateVM vm)
        {
            var allAbouts = await _context.Abouts.ToListAsync();
            if (allAbouts.Any())
            {
                _context.Abouts.RemoveRange(allAbouts);
                await _context.SaveChangesAsync();
            }

            if (vm.Img == null)
                throw new Exception("Image is required");

            string fileName = await SaveFileAsync(vm.Img);

            var about = new About
            {
                Title = vm.Title,
                Desc = vm.Desc,
                Img = fileName
            };

            await _context.Abouts.AddAsync(about);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(AboutEditVM vm)
        {
            var about = await _context.Abouts.FindAsync(vm.Id);
            if (about == null) throw new Exception("About not found");

            about.Title = vm.Title;
            about.Desc = vm.Desc;

            if (vm.Img != null)
            {
                DeleteFile(about.Img);
                about.Img = await SaveFileAsync(vm.Img);
            }

            _context.Abouts.Update(about);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about == null) throw new Exception("About not found");

            DeleteFile(about.Img);

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "about");
            Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Path.Combine("uploads/about", fileName).Replace("\\", "/");
        }

        private void DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            string filePath = Path.Combine(_env.WebRootPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
