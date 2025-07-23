using Dto;
using Models.Enums;

namespace Services.Interfaces
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(CreateTransactionDto transactionDto);
        Task UpdateTransactionState(int transactionId, TransactionStatus newState);
        Task<TransactionDto> GetTransactionByIdAsync(int id);
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
        Task<Dictionary<string, decimal>> GetDonationsPerMonthAsync(int months);
    }
}
