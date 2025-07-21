using AutoMapper;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IMapper _mapper;

        public PostService(IRepository<Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectViewModel>> getPostByNameAsync(string name)
        {
            var posts = await _projectRepository.FindAsync(p => p.Heading.Contains(name) && p.Type.Equals("post") && !p.By.ToLower().Equals("nine translation"));
            return MapToViewModel(posts ?? new List<Project>() { new Project() });
        }

        private IEnumerable<ProjectViewModel> MapToViewModel(IEnumerable<Project> projectList)
        {
            return _mapper.Map<IEnumerable<ProjectViewModel>>(projectList);
        }
    }
}
