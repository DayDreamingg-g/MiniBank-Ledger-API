using MediatR;
using MiniBank.Ledger.Application.Common.Dtos;

namespace MiniBank.Ledger.Application.Transactions.Queries.GetTransactions;

public sealed record GetTransactionsQuery(DateTime? From, DateTime? To)
    : IRequest<List<TransactionDto>>;