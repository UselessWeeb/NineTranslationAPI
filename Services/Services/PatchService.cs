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
        private readonly IMapper _mapper;

        public PatchService(IRepository<PatchUpdate> patchRepository, IMapper mapper)
        {
            _patchRepository = patchRepository;
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
            if (patchUpdates == null || !patchUpdates.Any())
            {
                throw new ArgumentException("Patch updates cannot be null or empty.", nameof(patchUpdates));
            }

            var existingPatches = (await _patchRepository.FindAsync(p => p.ProjectDetailId == projectDetailId)).ToList();

            var updatesDict = patchUpdates.ToDictionary(u => u.Id);
            var existingDict = existingPatches.ToDictionary(p => p.Id);

            foreach (var patchUpdate in patchUpdates)
            {
                if (existingDict.TryGetValue(patchUpdate.Id ?? 0, out var existingPatch))
                {
                    _mapper.Map(patchUpdate, existingPatch);
                    await _patchRepository.UpdateAsync(existingPatch);
                }
                else
                {
                    var newPatch = _mapper.Map<PatchUpdate>(patchUpdate);
                    newPatch.ProjectDetailId = projectDetailId;
                    await _patchRepository.AddAsync(newPatch);
                }
            }
            var patchesToRemove = existingPatches.Where(p => !updatesDict.ContainsKey(p.Id)).ToList();
            foreach (var patch in patchesToRemove)
            {
                await _patchRepository.RemoveAsync(patch);
            }
        }
    }
}
