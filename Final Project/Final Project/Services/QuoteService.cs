using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Quote;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly AppDbContext _context;

        public QuoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuoteVM>> GetAllAsync()
        {
            var quotes = await _context.Quotes.ToListAsync();

            // Manual mapping Model -> VM
            return quotes.Select(q => new QuoteVM
            {
                Id = q.Id,
                Content = q.Content,
                Author = q.Author
            }).ToList();
        }

        public async Task<QuoteVM> GetByIdAsync(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
                throw new Exception("Quote not found");

            return new QuoteVM
            {
                Id = quote.Id,
                Content = quote.Content,
                Author = quote.Author
            };
        }

        public async Task CreateAsync(QuoteCreateVM model)
        {
            var quote = new Quote
            {
                Content = model.Content,
                Author = model.Author
            };

            await _context.Quotes.AddAsync(quote);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, QuoteEditVM model)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
                throw new Exception("Quote not found");

            quote.Content = model.Content;
            quote.Author = model.Author;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
                throw new Exception("Quote not found");

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();
        }
    }
}
