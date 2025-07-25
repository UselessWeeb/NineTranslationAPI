using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IPatchService
    {
        Task AddPatchListAsync(IEnumerable<CreatePatchUpdateDto> patchUpdateDto);
        Task SmartUpdatePatchAsync(int projectDetailId, IEnumerable<UpdatePatchUpdateDto> patchUpdates);
    }
}
