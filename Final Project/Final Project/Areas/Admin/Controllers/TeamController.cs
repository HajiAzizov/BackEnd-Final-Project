using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class TeamController : Controller
    {
        
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _teamService.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _teamService.GetByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Img == null)
            {
                ModelState.AddModelError("Img", "Image is required.");
                return View(model);
            }

            try
            {
                await _teamService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }   

        public async Task<IActionResult> Edit(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();

            var model = new TeamEditVM
            {
                Id = team.Id,
                Name = team.Name,
                Position = team.Position
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeamEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _teamService.EditAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _teamService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
