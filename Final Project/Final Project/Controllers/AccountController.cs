using Final_Project.Models;
using Final_Project.ViewModels.User.Account;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Final_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            AppUser newUser = new AppUser
            {
                Fullname = registerVM.Fullname,
                Email = registerVM.Email,
                UserName = registerVM.Email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(registerVM);
            }

            if (!await _userManager.IsInRoleAsync(newUser, "Member"))
            {
                await _userManager.AddToRoleAsync(newUser, "Member");
            }

            //await _signInManager.SignInAsync(newUser, isPersistent: false);

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var url = Url.Action("ConfirmEmail", "Account", new { userID = newUser.Id, token },Request.Scheme,Request.Host.ToString());

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("hadjiedu@gmail.com"));
            email.To.Add(MailboxAddress.Parse(newUser.Email));
            email.Subject = "Test Email Subject";
            string html = $"<a href='{url}'>Click Here For Confirmation</a>";
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("hadjiedu@gmail.com", "tvhl encr swgn wmyt");
            smtp.Send(email);
            smtp.Disconnect(true);

            return RedirectToAction(nameof(VerifyEmail));
        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, token);
            return RedirectToAction(nameof(Login));
        }




        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            AppUser user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return RedirectToAction(nameof(VerifyEmail));
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string url = Url.Action("ResetPassword", "Account", new { token, userId = user.Id }, Request.Scheme);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("hadjiedu@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Reset your password";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<a href='{url}'>Click here to reset your password</a>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("hadjiedu@gmail.com", "tvhl encr swgn wmyt");
            smtp.Send(email);
            smtp.Disconnect(true);

            return RedirectToAction(nameof(VerifyEmail));
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string userId)
        {
            var model = new ResetPasswordVM
            {
                Token = token,
                UserId = userId
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return RedirectToAction("Index", "Home");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }
        public IActionResult ForgotPasswordConfirmation() => View();

    }
}

