using Newtonsoft.Json.Linq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PasswordTokenService : IPasswordTokenService
    {
        private readonly Dictionary<string, Models.PasswordToken> _token = new();
        public async Task<string> CreatePasswordTokenAsync(string userId, string token)
        {
            var passwordToken = new Models.PasswordToken
            {
                UserId = userId,
                Token = token,
                ExpirationDate = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };
            _token[token] = passwordToken;
            return passwordToken.Id;
        }
        public async Task<bool> MarkTokenAsUsedAsync(string token)
        {
            if (_token.TryGetValue(token, out var passwordToken))
            {
                if (!passwordToken.IsUsed)
                {
                    passwordToken.IsUsed = true;
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> IsTokenValidAsync(string tokenId)
        {
            if (_token.TryGetValue(tokenId, out var passwordToken))
            {
                if (!passwordToken.IsUsed && passwordToken.ExpirationDate > DateTime.UtcNow)
                {
                    return true;
                }
            }
            return false;
        }

        public Task<string?> GetUserIdFromTokenAsync(string token)
        {
            if (_token.TryGetValue(token, out var passwordToken))
            {
                return Task.FromResult<string?>(passwordToken.UserId);
            }
            return Task.FromResult<string?>(null);
        }
    }
}
