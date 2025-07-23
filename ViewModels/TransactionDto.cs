using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string? Status { get; set; }
        public string? Description { get; set; }
    }
}
