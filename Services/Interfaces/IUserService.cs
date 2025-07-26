using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllStaffAsync();
        Task<UserDto> GetStaffByIdAsync(string id);
        Task<IdentityResult> CreateUserAsync(CreateUserDto model);
    }
}
