using Models;
using ViewModels;

namespace Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectViewModel>> GetCarouselProjectsAsync(int x, int y, int z);
        Task<IEnumerable<ProjectViewModel>> GetSortedNineListAsync();
        Task<IEnumerable<ProjectViewModel>> GetRandomThreeProjectsAsync();
        Task<IEnumerable<ProjectViewModel>> GetSortedNineListPostAsync();
        Task<IEnumerable<ProjectViewModel>> GetSortedNineListCompletedAsync();
        Task<IEnumerable<ProjectViewModel>> GetSortedNineListOnGoingAsync();
        Task<IEnumerable<ProjectViewModel>> GetSortedNineListPartnerAsync();
        Task<ProjectViewModel> GetProjectByFinderAsync(string finder);
        Task<IEnumerable<ProjectViewModel>> GetProjectByNameAsync(string name);
    }
}