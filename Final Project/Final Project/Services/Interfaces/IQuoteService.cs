using Final_Project.ViewModels.Admin.Quote;

namespace Final_Project.Services.Interfaces
{
    public interface IQuoteService
    {
        Task<List<QuoteVM>> GetAllAsync();
        Task<QuoteVM> GetByIdAsync(int id);
        Task CreateAsync(QuoteCreateVM model);
        Task EditAsync(int id, QuoteEditVM model);
        Task DeleteAsync(int id);
    }
}
