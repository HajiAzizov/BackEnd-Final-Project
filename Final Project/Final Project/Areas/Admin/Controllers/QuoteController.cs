using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Quote;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuoteController : Controller
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        // GET: Admin/Quote
        public async Task<IActionResult> Index()
        {
            var quotes = await _quoteService.GetAllAsync();
            return View(quotes);
        }

        // GET: Admin/Quote/Detail/5
        public async Task<IActionResult> Detail(int id)
        {
            var quote = await _quoteService.GetByIdAsync(id);
            if (quote == null) return NotFound();
            return View(quote);
        }

        // GET: Admin/Quote/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Quote/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuoteCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _quoteService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Quote/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var quote = await _quoteService.GetByIdAsync(id);
            if (quote == null) return NotFound();

            var editVM = new QuoteEditVM
            {
                Id = quote.Id,
                Content = quote.Content,
                Author = quote.Author
            };

            return View(editVM);
        }

        // POST: Admin/Quote/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuoteEditVM model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid) return View(model);

            try
            {
                await _quoteService.EditAsync(id, model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Admin/Quote/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _quoteService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
