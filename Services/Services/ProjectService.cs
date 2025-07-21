using AutoMapper;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels;

namespace Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IRepository<Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetCarouselProjectsAsync(int x, int y, int z)
        {
            var specificIds = new List<int> { x, y, z };
            var projects = await _projectRepository.FindAsync(p =>
                p.Type == "project" && specificIds.Contains(p.Id));

            return MapToViewModel(projects.OrderByDescending(p => p.Id).ToList());
        }

        public async Task<IEnumerable<ProjectViewModel>> GetSortedNineListAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.Type == "project")
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        public async Task<IEnumerable<ProjectViewModel>> GetRandomThreeProjectsAsync()
        {
            var projects = (await _projectRepository.GetAllAsync())
                .Where(p => p.Type == "project")
                .ToList();

            return GetRandomProjects(projects, 3);
        }

        public async Task<IEnumerable<ProjectViewModel>> GetSortedNineListPostAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.Type == "post")
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        public async Task<IEnumerable<ProjectViewModel>> GetSortedNineListCompletedAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.Status == "Completed")
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        public async Task<IEnumerable<ProjectViewModel>> GetSortedNineListOnGoingAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.Status == "On-Going")
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        public async Task<IEnumerable<ProjectViewModel>> GetSortedNineListPartnerAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.By != "Nine Translation" && !string.IsNullOrEmpty(p.Status))
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        private IEnumerable<ProjectViewModel> GetRandomProjects(List<Project> projects, int count)
        {
            var random = new Random();
            var projectList = projects
                .OrderBy(x => random.Next())
                .Take(count);

            return MapToViewModel(projectList);
        }

        public async Task<ProjectViewModel> GetProjectByFinderAsync(string finder)
        {
            var projects = await _projectRepository.FindAsync(p => p.Finder == finder);
            return MapToViewModel(projects.FirstOrDefault() ?? new Project());
        }

        public async Task<IEnumerable<ProjectViewModel>> GetProjectByNameAsync(string name)
        {
            var projects = await _projectRepository.FindAsync(p => p.Heading.Contains(name) && p.Type.Equals("project") && !p.By.ToLower().Equals("nine translation"));
            return MapToViewModel(projects ?? new List<Project>() { new Project() });
        }

        private ProjectViewModel MapToViewModel(Project project)
        {
            return _mapper.Map<ProjectViewModel>(project);
        }

        private IEnumerable<ProjectViewModel> MapToViewModel(IEnumerable<Project> projectList)
        {
            return _mapper.Map<IEnumerable<ProjectViewModel>>(projectList);
        }
    }
}