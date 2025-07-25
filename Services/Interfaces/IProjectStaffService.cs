using Dto;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IProjectStaffService
    {
        Task AddListAsync(IEnumerable<CreateProjectStaffDto> newStaff);
        Task SmartUpdateAsync(int projectId, IEnumerable<UpdateProjectStaffDto> staffUpdates);
    }
}
