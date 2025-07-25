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
        private readonly IRepository<ProjectStaff> _projectStaffRepository;
        private readonly IMapper _mapper;

        public ProjectStaffService(IRepository<ProjectStaff> projectStaffRepository, IMapper mapper)
        {
            _projectStaffRepository = projectStaffRepository;
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
            var currentStaff = (await _projectStaffRepository
                .FindAsync(s => s.ProjectDetailId == projectId)).ToList();

            var incomingStaff = staffUpdates
                .Select(dto => _mapper.Map<ProjectStaff>(dto))
                .ToList();

            var toAdd = incomingStaff
                .Where(ns => !currentStaff.Any(cs => cs.Id == ns.Id))
                .ToList();

            var toUpdate = incomingStaff
                .Where(ns => currentStaff.Any(cs => cs.Id == ns.Id &&
                    (cs.UserId != ns.UserId || cs.Role != ns.Role || cs.ProjectDetailId != ns.ProjectDetailId)))
                .ToList();

            var toRemove = currentStaff
                .Where(cs => !incomingStaff.Any(ns => ns.Id == cs.Id))
                .ToList();

            if (toAdd.Any())
                await _projectStaffRepository.AddRangeAsync(toAdd);

            foreach (var updateStaff in toUpdate)
                await _projectStaffRepository.UpdateAsync(updateStaff);

            if (toRemove.Any())
                await _projectStaffRepository.RemoveRangeAsync(toRemove);
        }
    }
}
