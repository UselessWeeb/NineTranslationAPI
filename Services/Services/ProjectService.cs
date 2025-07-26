using AutoMapper;
using Dto;
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
        private readonly IImageService _imageService;
        private readonly IProjectStaffService _projectStaffService;

        public ProjectService(IRepository<Project> projectRepository, IMapper mapper, IImageService imageService, IProjectStaffService projectStaffService)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _imageService = imageService;
            _projectStaffService = projectStaffService;
        }

        public async Task<IEnumerable<ProjectDto>> GetCarouselProjectsAsync()
        {
            var projects = await _projectRepository.FindAsync(p =>
                p.Type == "project" && p.isCarousel && p.isActive);

            return MapToViewModel(projects.OrderByDescending(p => p.Id).ToList());
        }

        public async Task<IEnumerable<ProjectDto>> GetSortedNineListAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.Type == "project" && p.isActive)
                .OrderByDescending(p => p.Id)
                .Take(6)
                .ToList());
        }

        public async Task<IEnumerable<ProjectDto>> GetRandomThreeProjectsAsync()
        {
            var projects = (await _projectRepository.GetAllAsync())
                .Where(p => (p.Type == "project" || p.Type == "post") && p.isActive)
                .ToList();

            return GetRandomProjects(projects, 3);
        }

        public async Task<IEnumerable<ProjectDto>> GetSortedNineListPostAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.Type == "post" && p.isActive)
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        public async Task<IEnumerable<ProjectDto>> GetSortedNineListCompletedAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.Status == "Completed" && p.isActive)
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        public async Task<IEnumerable<ProjectDto>> GetSortedNineListOnGoingAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.Status == "On-Going" && p.isActive)
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        public async Task<IEnumerable<ProjectDto>> GetSortedNineListPartnerAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return MapToViewModel(projects
                .Where(p => p.Type == "project" && p.By != "Nine Translation" && !string.IsNullOrEmpty(p.Status) && p.isActive)
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        private IEnumerable<ProjectDto> GetRandomProjects(List<Project> projects, int count)
        {
            var random = new Random();
            var projectList = projects
                .Where(p => p.isActive && p.Type == "project")
                .OrderBy(x => random.Next())
                .Take(count);

            return MapToViewModel(projectList);
        }

        public async Task<ProjectDto> GetProjectByFinderAsync(string finder)
        {
            var projects = await _projectRepository.FindAsync(p => p.Finder == finder && p.isActive);
            return MapToViewModel(projects.FirstOrDefault() ?? new Project());
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectByNameAsync(string name)
        {
            var projects = await _projectRepository.FindAsync(p => p.Heading.Contains(name) && (p.Type.Equals("project") || p.Type.Equals("partner")) && !p.By.ToLower().Equals("nine translation") && p.isActive);
            return MapToViewModel(projects ?? new List<Project>() { new Project() });
        }

        private ProjectDto MapToViewModel(Project project)
        {
            return _mapper.Map<ProjectDto>(project);
        }

        private IEnumerable<ProjectDto> MapToViewModel(IEnumerable<Project> projectList)
        {
            return _mapper.Map<IEnumerable<ProjectDto>>(projectList);
        }

        public async Task<int> CreateProjectAsync(CreateProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);

            project.Date = DateTime.UtcNow;

            if (projectDto.ThumbnailFile != null)
            {
                var imageUrl = await _imageService.UploadImageAsync(projectDto.ThumbnailFile);

                project.Thumbnail = imageUrl != null ? imageUrl.Url.ToString() : "https://example.com/default-thumbnail.png";
            }

            await _projectRepository.AddAsync(project);
            return project.Detail.Id;
        }

        public async Task SetCarouselAsync(int newCarouselProjectId)
        {
            var allProjects = await _projectRepository.GetAllAsync();
            var carouselProjects = allProjects
                .Where(p => p.Type == "project" && p.isCarousel)
                .OrderBy(p => p.Date)
                .ToList();

            bool isAlreadyCarousel = carouselProjects.Any(p => p.Id == newCarouselProjectId);
            if (isAlreadyCarousel)
            {
                return;
            }

            if (carouselProjects.Count >= 3)
            {
                var oldestCarousel = carouselProjects.First();
                oldestCarousel.isCarousel = false;
                await _projectRepository.UpdateAsync(oldestCarousel);
            }

            var newCarouselProject = allProjects.FirstOrDefault(p => p.Id == newCarouselProjectId);
            if (newCarouselProject == null)
            {
                throw new KeyNotFoundException($"Project with ID {newCarouselProjectId} not found.");
            }

            newCarouselProject.isCarousel = true;
            await _projectRepository.UpdateAsync(newCarouselProject);
        }

        public Task DeleteProjectByFinderAsync(string finder)
        {
            var project = _projectRepository.FindAsync(p => p.Finder == finder).Result.FirstOrDefault();
            if (project != null)
            {
                return _projectRepository.RemoveAsync(project);
            }
            else
            {
                throw new KeyNotFoundException($"Project with finder '{finder}' not found.");
            }
        }

        public Task DeleteProjectById(int id)
        {
            var project = _projectRepository.FindAsync(p => p.Id == id).Result.FirstOrDefault();
            if (project != null)
            {
                return _projectRepository.RemoveAsync(project);
            }
            else
            {
                throw new KeyNotFoundException($"Project with ID '{id}' not found.");
            }
        }

        public async Task<int> UpdateProjectAsync(UpdateProjectDto project)
        {
            var existingProjects = await _projectRepository.FindAsync(p => p.Id == project.Id);
            var existingProject = existingProjects.FirstOrDefault();

            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Project with ID '{project.Id}' not found.");
            }

            _mapper.Map(project, existingProject);

            if (project.ThumbnailFile != null)
            {
                var imageUrl = await _imageService.UploadImageAsync(project.ThumbnailFile);
                existingProject.Thumbnail = imageUrl?.Url?.ToString() ?? "https://example.com/default-thumbnail.png";
            }

            await _projectRepository.UpdateAsync(existingProject);
            return existingProject.Detail.Id;
        }


        public async Task DisableProjectAsync(string finder)
        {
            var projectList = await _projectRepository.FindAsync(p => p.Finder == finder);
            var project = projectList.FirstOrDefault();

            if (project == null)
            {
                throw new KeyNotFoundException($"Project with finder '{finder}' not found.");
            }

            project.isActive = !project.isActive;
            await _projectRepository.UpdateAsync(project);
        }

        public Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = _projectRepository.GetAllAsync().Result
                .Where(p => p.Type == "project")
                .ToList();
            if (projects == null || !projects.Any())
            {
                throw new KeyNotFoundException("No projects found.");
            }
            return Task.FromResult(MapToViewModel(projects));
        }
    }
}