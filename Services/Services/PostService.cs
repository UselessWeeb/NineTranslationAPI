﻿using AutoMapper;
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

        public Task DeletePostAsync(int id)
        {
            var post = _projectRepository.GetByIdAsync(id);
            if (post == null)
            {
                throw new KeyNotFoundException($"Post with ID {id} not found.");
            }
            return _projectRepository.RemoveAsync(post.Result);
        }

        public async Task DeletePostASync(string finder)
        {
            var post = _projectRepository.FindAsync(p => p.Finder.Equals(finder)).Result.FirstOrDefault();
            if (post == null)
            {
                throw new KeyNotFoundException($"Post with finder {finder} not found.");
            }
            await _projectRepository.RemoveAsync(post);
        }

        public async Task DisablePost(string finder)
        {
            var post = _projectRepository.FindAsync(p => p.Finder.Equals(finder)).Result.FirstOrDefault();
            if (post == null)
            {
                throw new KeyNotFoundException($"Post with finder {finder} not found.");
            }
            post.isActive = false;
            await _projectRepository.UpdateAsync(post);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllPostsAsync()
        {
            var posts = await _projectRepository.FindAsync(p => p.Type.Equals("post"));
            return MapToViewModel(posts ?? new List<Project>() { new Project() });
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
