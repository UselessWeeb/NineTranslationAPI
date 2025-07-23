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
    public class PatchService : IPatchService
    {
        private readonly IRepository<PatchUpdate> _patchRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public PatchService(IRepository<PatchUpdate> patchRepository, IMapper mapper, IImageService imageService)
        {
            _patchRepository = patchRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        public Task AddPatchAsync(CreatePatchUpdateDto patchUpdateDto)
        {
            if (patchUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(patchUpdateDto), "Patch update data cannot be null.");
            }
            var patchUpdate = _mapper.Map<PatchUpdate>(patchUpdateDto);
            patchUpdate.ProjectDetailId = patchUpdateDto.ProjectDetailId;
            return _patchRepository.AddAsync(patchUpdate);
        }
    }
}
