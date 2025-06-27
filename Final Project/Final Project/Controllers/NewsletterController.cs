using Final_Project.Data;
using Final_Project.Models;
using Final_Project.ViewModels.User.Subscribe;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;

namespace Final_Project.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly AppDbContext _context;

        public NewsletterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe( SubscribeVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Email");

            var exists = await _context.Subscribers
                .FirstOrDefaultAsync(s => s.Email == model.Email);

            if (exists != null)
            {
                if (exists.IsConfirmed)
                    return Ok(new { message = "You are already subscribed." });
                else
                    return Ok(new { message = "Please confirm your subscription via email." });
            }

            var token = Guid.NewGuid().ToString();

            var subscriber = new Subscriber
            {
                Email = model.Email,
                IsConfirmed = false,
                ConfirmationToken = token
            };

            _context.Subscribers.Add(subscriber);
            await _context.SaveChangesAsync();

            // Email təsdiq linki yaradılır
            var confirmationUrl = Url.Action("ConfirmSubscription", "Newsletter",
                new { email = model.Email, token },
                Request.Scheme);

            // Email mesajı tərtib olunur
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("hadjiedu@gmail.com")); // Sənin emailin
            email.To.Add(MailboxAddress.Parse(model.Email));
            email.Subject = "Confirm your subscription";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"Click <a href='{confirmationUrl}'>here</a> to confirm your subscription."
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, true);
            smtp.Authenticate("hadjiedu@gmail.com", "tvhl encr swgn wmyt"); // Sənin şifrə/app password
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok(new { message = "Confirmation email sent. Please check your inbox." });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmSubscription(string email, string token)
        {
            var subscriber = await _context.Subscribers
                .FirstOrDefaultAsync(s => s.Email == email && s.ConfirmationToken == token);

            if (subscriber == null)
                return NotFound("Invalid confirmation link.");

            subscriber.IsConfirmed = true;
            subscriber.ConfirmationToken = null;
            await _context.SaveChangesAsync();

            TempData["Message"] = "Subscription confirmed! Thank you.";
            return RedirectToAction("Index", "Home");
        }
    }
}
