using AutoMapper;
using Dto;
using Models;
using Models.Enums;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(IRepository<Transaction> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateTransactionAsync(CreateTransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.TransactionDate = DateTime.UtcNow;
            transaction.Status = TransactionStatus.Pending;
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Select(t => _mapper.Map<TransactionDto>(t)).ToList();
        }

        public async Task<Dictionary<string, decimal>> GetDonationsPerMonthAsync(int months)
        {
            var transactions = await _transactionRepository.GetAllAsync();

            var donations = transactions
                .Where(t => t.Status == TransactionStatus.Completed && t.TransactionDate >= DateTime.UtcNow.AddMonths(-months))
                .GroupBy(t => new { t.TransactionDate.Year, t.TransactionDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                    TotalAmount = g.Sum(t => t.Amount)
                });
            return donations.ToDictionary(
                d => d.Month.ToString("MMMM yyyy"),
                d => d.TotalAmount
            );
        }

        public Task<TransactionDto> GetTransactionByIdAsync(int id)
        {
            var transaction = _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transaction with ID {id} not found.");
            }
            return Task.FromResult(_mapper.Map<TransactionDto>(transaction.Result));
        }

        public Task UpdateTransactionState(int transactionId, TransactionStatus newState)
        {
            var transaction = _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transaction with ID {transactionId} not found.");
            }
            transaction.Result.Status = newState;
            return _transactionRepository.UpdateAsync(transaction.Result);
        }
    }
}
