using AutoMapper;
using Dto;
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
        private readonly IImageService _imageService;

        public PostService(IRepository<Project> projectRepository, IMapper mapper, IImageService imageService)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task CreatePostAsync(CreatePostDto postDto)
        {
            var post = _mapper.Map<Project>(postDto);

            post.Date = DateTime.UtcNow;

            post.Type = "post";

            var imageUrl = await _imageService.UploadImageAsync(postDto.ThumbnailFile);

            post.Thumbnail = imageUrl != null ? imageUrl.Url.ToString() : "https://example.com/default-thumbnail.png";

            await _projectRepository.AddAsync(post);
        }

        public Task DeletePostAsync(int id)
        {
            var post = _projectRepository.GetByIdAsync(id);
            if (post == null)
            {
                throw new KeyNotFoundException($"Post with ID {id} not found.");
            }
            return _projectRepository.RemoveAsync(post.Result);
        }

        public Task DeletePostASync(string finder)
        {
            var post = _projectRepository.FindAsync(p => p.Finder.Equals(finder)).Result.FirstOrDefault();
            if (post == null)
            {
                throw new KeyNotFoundException($"Post with finder {finder} not found.");
            }
            return _projectRepository.RemoveAsync(post);
        }

        public Task DisablePost(string finder)
        {
            var post = _projectRepository.FindAsync(p => p.Finder.Equals(finder)).Result.FirstOrDefault();
            if (post == null)
            {
                throw new KeyNotFoundException($"Post with finder {finder} not found.");
            }
            post.isActive = !post.isActive;
            return _projectRepository.UpdateAsync(post);
        }

        public async Task<IEnumerable<ProjectDto>> getPostByNameAsync(string name)
        {
            var posts = await _projectRepository.FindAsync(p => p.Heading.Contains(name) && p.Type.Equals("post") && !p.By.ToLower().Equals("nine translation"));
            return MapToViewModel(posts ?? new List<Project>() { new Project() });
        }

        private IEnumerable<ProjectDto> MapToViewModel(IEnumerable<Project> projectList)
        {
            return _mapper.Map<IEnumerable<ProjectDto>>(projectList);
        }
    }
}
