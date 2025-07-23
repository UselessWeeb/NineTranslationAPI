using AutoMapper;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProjectStaffService : IProjectStaffService
    {
        private readonly IRepository<ProjectStaff> _projectStaffRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ProjectStaffService(IRepository<ProjectStaff> projectStaffRepository, IMapper mapper, IImageService imageService)
        {
            _projectStaffRepository = projectStaffRepository;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task AddAsync(ProjectStaff newStaff)
        {
            await _projectStaffRepository.AddAsync(newStaff);
        }

        public async Task RemoveAsync(ProjectStaff staff)
        {
            var existingStaff = _projectStaffRepository.FindAsync(s => s.Id == staff.Id).Result.FirstOrDefault();
            if (existingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with ID '{staff.Id}' not found.");
            }
            await _projectStaffRepository.RemoveAsync(existingStaff);
        }
    }
}
