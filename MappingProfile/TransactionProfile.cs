using AutoMapper;
using Dto;
using Models;

namespace MappingProfile
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<CreateTransactionDto, Transaction>();
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
