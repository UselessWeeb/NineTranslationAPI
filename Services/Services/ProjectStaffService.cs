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
    public class ProjectStaffService : IProjectStaffService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectStaff> _projectStaffRepository;
        private readonly IRepository<ProjectDetail> _projectDetailRepository;
        private readonly IMapper _mapper;

        public ProjectStaffService(IRepository<Project> projectRepository, IRepository<ProjectStaff> projectStaffRepository, IRepository<ProjectDetail> projectDetailRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _projectStaffRepository = projectStaffRepository;
            _projectDetailRepository = projectDetailRepository;
            _mapper = mapper;
        }

        public async Task AddListAsync(IEnumerable<CreateProjectStaffDto> newStaff)
        {
            if (newStaff == null || !newStaff.Any())
            {
                throw new ArgumentException("New staff cannot be null or empty.", nameof(newStaff));
            }
            var projectStaffList = _mapper.Map<IEnumerable<ProjectStaff>>(newStaff);
            await _projectStaffRepository.AddRangeAsync(projectStaffList);
        }

        public async Task SmartUpdateAsync(int projectId, IEnumerable<UpdateProjectStaffDto> staffUpdates)
        {
            //simply just remove all belong to the project then recreate it
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }
            var projectDetail = project.Detail;
            var existingStaff = projectDetail.StaffRoles?.ToList() ?? new List<ProjectStaff>();
            if (existingStaff.Any())
            {
                await _projectStaffRepository.RemoveRangeAsync(existingStaff);
            }
            if (staffUpdates == null || !staffUpdates.Any())
            {
                throw new ArgumentException("Staff updates cannot be null or empty.", nameof(staffUpdates));
            }
            var newStaff = _mapper.Map<IEnumerable<ProjectStaff>>(staffUpdates);
            foreach (var staff in newStaff)
            {
                staff.ProjectDetailId = projectDetail.Id;
            }
            await _projectStaffRepository.AddRangeAsync(newStaff);
        }
    }
}
