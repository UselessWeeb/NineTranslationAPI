using Models;
using ViewModels;

namespace Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetCarouselProjectsAsync(int x, int y, int z);
        Task<IEnumerable<ProjectDto>> GetSortedNineListAsync();
        Task<IEnumerable<ProjectDto>> GetRandomThreeProjectsAsync();
        Task<IEnumerable<ProjectDto>> GetSortedNineListPostAsync();
        Task<IEnumerable<ProjectDto>> GetSortedNineListCompletedAsync();
        Task<IEnumerable<ProjectDto>> GetSortedNineListOnGoingAsync();
        Task<IEnumerable<ProjectDto>> GetSortedNineListPartnerAsync();
        Task<ProjectDto> GetProjectByFinderAsync(string finder);
        Task<IEnumerable<ProjectDto>> GetProjectByNameAsync(string name);
        Task CreateProject(CreateProjectDto project);
    }
}