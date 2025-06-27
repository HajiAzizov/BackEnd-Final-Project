using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();

            var userRoles = new Dictionary<string, IList<string>>();
            foreach (var user in users)
            {
                var roles = await _userService.GetUserRolesAsync(user);
                userRoles[user.Id] = roles;
            }

            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.AllRoles = allRoles;

            var model = new UserListVM
            {
                Users = users,
                UserRoles = userRoles
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddRole(string userId, string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                TempData["RoleError_" + userId] = "Please select a role.";
                return RedirectToAction("Index");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            var userRoles = await _userService.GetUserRolesAsync(user);

            if (userRoles.Contains(role))
            {
                TempData["RoleError_" + userId] = $"This user already has the '{role}' role";
                return RedirectToAction("Index");
            }

            var success = await _userService.AddUserRoleAsync(user, role);
            if (success)
                return RedirectToAction("Index");

            TempData["RoleError_" + userId] = "Failed to add role.";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound();

            var success = await _userService.RemoveUserRoleAsync(user, role);
            if (success)
                return RedirectToAction("Index");
            else
                return BadRequest("Role silinə bilmədi");
        }
    }
}
