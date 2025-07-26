using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class CreatePasswordTokenDto
    {
        public required string UserId { get; set; }
        public required string Token { get; set; }
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddMinutes(5);
        public bool IsUsed { get; set; } = false;
    }
}
