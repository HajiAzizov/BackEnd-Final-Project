using Final_Project.ViewModels.Admin.Partner;

namespace Final_Project.Services.Interfaces
{
    public interface IPartnerService
    {
        Task<List<PartnerVM>> GetAllAsync();
        Task<PartnerVM> GetByIdAsync(int id);
        Task CreateAsync(PartnerCreateVM vm);
        Task EditAsync(PartnerEditVM vm);
        Task DeleteAsync(int id);
    }
}
