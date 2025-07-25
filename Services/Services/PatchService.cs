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
    public class PatchService : IPatchService
    {
        private readonly IRepository<PatchUpdate> _patchRepository;
        private readonly IRepository<ProjectDetail> _projectDetailRepository;
        private readonly IMapper _mapper;

        public PatchService(IRepository<PatchUpdate> patchRepository, IRepository<ProjectDetail> projectDetailRepository, IMapper mapper)
        {
            _patchRepository = patchRepository;
            _projectDetailRepository = projectDetailRepository;
            _mapper = mapper;
        }

        public async Task AddPatchListAsync(IEnumerable<CreatePatchUpdateDto> patchUpdateDto)
        {
            if (patchUpdateDto == null || !patchUpdateDto.Any())
            {
                throw new ArgumentException("Patch updates cannot be null or empty.", nameof(patchUpdateDto));
            }
            var patchUpdates = _mapper.Map<IEnumerable<PatchUpdate>>(patchUpdateDto);
            foreach (var patchUpdate in patchUpdates)
            {
                await _patchRepository.AddAsync(patchUpdate);
            }
        }

        public async Task SmartUpdatePatchAsync(int projectDetailId, IEnumerable<UpdatePatchUpdateDto> patchUpdates)
        {
            // Remove all existing patches for the project detail and recreate them
            var existingPatches = await _patchRepository.FindAsync(p => p.ProjectDetailId == projectDetailId);
            if (existingPatches.Any())
            {
                await _patchRepository.RemoveRangeAsync(existingPatches);
            }
            if (patchUpdates == null || !patchUpdates.Any())
            {
                throw new ArgumentException("Patch updates cannot be null or empty.", nameof(patchUpdates));
            }
            var newPatches = _mapper.Map<IEnumerable<PatchUpdate>>(patchUpdates);
            foreach (var patch in newPatches)
            {
                patch.ProjectDetailId = projectDetailId;
                await _patchRepository.AddAsync(patch);
            }
        }
    }
}
