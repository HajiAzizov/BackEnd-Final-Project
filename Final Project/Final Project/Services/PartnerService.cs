using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Partner;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PartnerService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<PartnerVM>> GetAllAsync()
        {
            return await _context.Partners
                .Select(p => new PartnerVM
                {
                    Id = p.Id,
                    Img = p.Img
                })
                .ToListAsync();
        }

        public async Task<PartnerVM> GetByIdAsync(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null) throw new Exception("Partner not found");

            return new PartnerVM
            {
                Id = partner.Id,
                Img = partner.Img
            };
        }

        public async Task CreateAsync(PartnerCreateVM vm)
        {
            if (vm.ImgFile == null) throw new Exception("Image file is required");

            string fileName = await SaveFileAsync(vm.ImgFile);
            var partner = new Partner { Img = fileName };

            await _context.Partners.AddAsync(partner);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(PartnerEditVM vm)
        {
            var partner = await _context.Partners.FindAsync(vm.Id);
            if (partner == null) throw new Exception("Partner not found");

            if (vm.ImgFile != null)
            {
                DeleteFile(partner.Img);
                partner.Img = await SaveFileAsync(vm.ImgFile);
            }

            _context.Partners.Update(partner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null) throw new Exception("Partner not found");

            DeleteFile(partner.Img);
            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();
        }
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "partners");
            Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Path.Combine("uploads/partners", fileName).Replace("\\", "/");
        }

        private void DeleteFile(string fileName)
        {
            string filePath = Path.Combine(_env.WebRootPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
