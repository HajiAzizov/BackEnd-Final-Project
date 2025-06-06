using Final_Project.ViewModels.Admin.Team;

namespace Final_Project.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();
        Task<TeamVM> GetByIdAsync(int id);
        Task CreateAsync(TeamCreateVM model);
        Task EditAsync(TeamEditVM model);
        Task DeleteAsync(int id);
    }
}
