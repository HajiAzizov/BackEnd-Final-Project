using Final_Project.ViewModels.Admin.Slider;

namespace Final_Project.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<SliderVM> GetByIdAsync(int id);
        Task CreateAsync(SliderCreateVM vm);
        Task EditAsync(SliderEditVM vm);
        Task DeleteAsync(int id);
    }
}
