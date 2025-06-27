using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Genre;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _context;
        public GenreService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<GenreVM>> GetAllAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            return genres.Select(g => new GenreVM
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }

        public async Task<GenreVM> GetByIdAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) throw new Exception("Genre not found");

            return new GenreVM
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public async Task CreateAsync(GenreCreateVM model)
        {
            var genre = new Genre
            {
                Name = model.Name
            };

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, GenreEditVM model)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) throw new Exception("Genre not found");

            genre.Name = model.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) throw new Exception("Genre not found");

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}
