using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class CreateTransactionDto
    {
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        public string UserEmail { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
