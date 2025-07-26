using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PasswordToken
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public PasswordToken()
        {
            Id = Guid.NewGuid().ToString();
            ExpirationDate = DateTime.UtcNow.AddMinutes(5);
            IsUsed = false;
        }
    }
}
