using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Team;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<TeamVM>> GetAllAsync()
        {
            return await _context.Teams
                .Select(t => new TeamVM
                {
                    Id = t.Id,
                    Name = t.Name,
                    Position = t.Position,
                    Img = t.Img
                })
                .ToListAsync();
        }

        public async Task<TeamVM> GetByIdAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) throw new Exception("Team member not found");

            return new TeamVM
            {
                Id = team.Id,
                Name = team.Name,
                Position = team.Position,
                Img = team.Img
            };
        }

        public async Task CreateAsync(TeamCreateVM model)
        {
            if (model.Img == null)
                throw new Exception("Image is required");

            string fileName = await SaveFileAsync(model.Img);

            var team = new Team
            {
                Name = model.Name,
                Position = model.Position,
                Img = fileName
            };

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TeamEditVM model)
        {
            var team = await _context.Teams.FindAsync(model.Id);
            if (team == null) throw new Exception("Team member not found");

            if (model.Img != null)
            {
                DeleteFile(team.Img);
                team.Img = await SaveFileAsync(model.Img);
            }

            team.Name = model.Name;
            team.Position = model.Position;

            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) throw new Exception("Team member not found");

            DeleteFile(team.Img);

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            string folder = Path.Combine(_env.WebRootPath, "uploads", "team");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("uploads/team", fileName).Replace("\\", "/");
        }


        private void DeleteFile(string filePath)
        {
            string fullPath = Path.Combine(_env.WebRootPath, filePath.Replace("/", "\\"));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
