using Dto;
using Models;
using ViewModels;

namespace Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetCarouselProjectsAsync();
        Task<IEnumerable<ProjectDto>> GetSortedNineListAsync();
        Task<IEnumerable<ProjectDto>> GetRandomThreeProjectsAsync();
        Task<IEnumerable<ProjectDto>> GetSortedNineListPostAsync();
        Task<IEnumerable<ProjectDto>> GetSortedNineListCompletedAsync();
        Task<IEnumerable<ProjectDto>> GetSortedNineListOnGoingAsync();
        Task<IEnumerable<ProjectDto>> GetSortedNineListPartnerAsync();
        Task<ProjectDto> GetProjectByFinderAsync(string finder);
        Task<IEnumerable<ProjectDto>> GetProjectByNameAsync(string name);
        Task<int> CreateProjectAsync(CreateProjectDto project);
        Task SetCarouselAsync(int newCarouselProjectId);
        Task<int> UpdateProjectAsync(UpdateProjectDto project);
        Task DeleteProjectById(int id);
        Task DeleteProjectByFinderAsync(string finder);
        Task DisableProjectAsync(string finder);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    }
}