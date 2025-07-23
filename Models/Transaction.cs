using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public string Description { get; set; } = string.Empty;
    }
}
