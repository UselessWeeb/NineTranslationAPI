using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPasswordTokenService
    {
        Task<string> CreatePasswordTokenAsync(string userId, string token);
        Task<string?> GetUserIdFromTokenAsync(string token);
        Task<bool> MarkTokenAsUsedAsync(string token);
        Task<bool> IsTokenValidAsync(string tokenId);
    }
}
