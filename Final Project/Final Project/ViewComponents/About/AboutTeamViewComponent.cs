using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Team;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents.About
{
    public class AboutTeamViewComponent : ViewComponent
    {
        private readonly ITeamService _teamService;
        public AboutTeamViewComponent(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<TeamVM> teams = await _teamService.GetAllAsync();
            return await Task.FromResult(View(teams));
        }
    }
}
