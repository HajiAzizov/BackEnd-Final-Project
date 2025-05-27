using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Slider;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<SliderVM>> GetAllAsync()
        {
            return await _context.Sliders
                .Select(s => new SliderVM
                {
                    Id = s.Id,
                    Img = s.Img,
                    Title = s.Title,
                    Description = s.Description
                }).ToListAsync();
        }

        public async Task<SliderVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("ID düzgün deyil");

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) throw new KeyNotFoundException("Slider tapılmadı");

            return new SliderVM
            {
                Id = slider.Id,
                Img = slider.Img,
                Title = slider.Title,
                Description = slider.Description
            };
        }

        public async Task CreateAsync(SliderCreateVM vm)
        {
            if (vm.ImgFile == null || vm.ImgFile.Length == 0)
                throw new ArgumentException("Şəkil faylı boş ola bilməz");

            if (string.IsNullOrWhiteSpace(vm.Title))
                throw new ArgumentException("Başlıq boş ola bilməz");

            string fileName = $"{Guid.NewGuid()}_{vm.ImgFile.FileName}";
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "sliders");
            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await vm.ImgFile.CopyToAsync(stream);
            }

            var slider = new Slider
            {
                Img = Path.Combine("uploads", "sliders", fileName),
                Title = vm.Title.Trim(),
                Description = vm.Description?.Trim()
            };

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(SliderEditVM vm)
        {
            var slider = await _context.Sliders.FindAsync(vm.Id);
            if (slider == null) throw new KeyNotFoundException("Slider tapılmadı");

            if (string.IsNullOrWhiteSpace(vm.Title))
                throw new ArgumentException("Başlıq boş ola bilməz");

            if (vm.ImgFile != null)
            {
                // Köhnə şəkli sil
                string oldImagePath = Path.Combine(_env.WebRootPath, slider.Img);
                if (File.Exists(oldImagePath))
                    File.Delete(oldImagePath);

                // Yeni şəkli yüklə
                string fileName = $"{Guid.NewGuid()}_{vm.ImgFile.FileName}";
                string folderPath = Path.Combine(_env.WebRootPath, "uploads", "sliders");
                Directory.CreateDirectory(folderPath);
                string newFilePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await vm.ImgFile.CopyToAsync(stream);
                }

                slider.Img = Path.Combine("uploads", "sliders", fileName);
            }

            slider.Title = vm.Title.Trim();
            slider.Description = vm.Description?.Trim();

            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) throw new KeyNotFoundException("Slider tapılmadı");

            string imagePath = Path.Combine(_env.WebRootPath, slider.Img);
            if (File.Exists(imagePath))
                File.Delete(imagePath);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }
    }
}
