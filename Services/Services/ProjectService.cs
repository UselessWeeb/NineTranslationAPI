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
        private readonly IImageService _imageService;   

        public ProjectService(IRepository<Project> projectRepository, IMapper mapper, IImageService imageService)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _imageService = imageService;
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
                .Take(9)
                .ToList());
        }

        public async Task<IEnumerable<ProjectDto>> GetRandomThreeProjectsAsync()
        {
            var projects = (await _projectRepository.GetAllAsync())
                .Where(p => p.Type == "project" && p.isActive)
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
                .Where(p => p.By != "Nine Translation" && !string.IsNullOrEmpty(p.Status) && p.isActive)
                .OrderByDescending(p => p.Id)
                .Take(9)
                .ToList());
        }

        private IEnumerable<ProjectDto> GetRandomProjects(List<Project> projects, int count)
        {
            var random = new Random();
            var projectList = projects
                .Where(p => p.isActive)
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
            var projects = await _projectRepository.FindAsync(p => p.Heading.Contains(name) && p.Type.Equals("project") && !p.By.ToLower().Equals("nine translation") && p.isActive);
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

        public async Task CreateProjectAsync(CreateProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);

            project.Date = DateTime.UtcNow;

            var imageUrl = await _imageService.UploadImageAsync(projectDto.ThumbnailFile);

            project.Thumbnail = imageUrl != null ? imageUrl.Url.ToString() : "https://example.com/default-thumbnail.png";

            await _projectRepository.AddAsync(project);
        }

        public async Task SetCarouselAsync(int a, int b, int c)
        {
            var allProjects = await _projectRepository.GetAllAsync();
            var carouselProjects = allProjects.Where(p => p.Type == "project" && p.isCarousel).ToList();

            foreach (var project in carouselProjects)
            {
                project.isCarousel = false;
                await _projectRepository.UpdateAsync(project);
            }

            var newCarouselProjects = allProjects.Where(p => p.Type == "project" && (p.Id == a || p.Id == b || p.Id == c)).ToList();
            foreach (var project in newCarouselProjects)
            {
                project.isCarousel = true;
                await _projectRepository.UpdateAsync(project);
            }
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

        public Task UpdateProjectAsync(CreateProjectDto project)
        {
            var existingProject = _projectRepository.FindAsync(p => p.Id == project.Id).Result.FirstOrDefault();
            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Project with ID '{project.Id}' not found.");
            }
            existingProject = _mapper.Map(project, existingProject);
            if (project.ThumbnailFile != null)
            {
                var imageUrl = _imageService.UploadImageAsync(project.ThumbnailFile).Result;
                existingProject.Thumbnail = imageUrl != null ? imageUrl.Url.ToString() : "https://example.com/default-thumbnail.png";
            }
            return _projectRepository.UpdateAsync(existingProject);
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
    }
}