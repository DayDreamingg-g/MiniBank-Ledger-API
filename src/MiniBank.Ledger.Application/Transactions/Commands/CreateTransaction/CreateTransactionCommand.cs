using MediatR;
using MiniBank.Ledger.Domain.Enums;

namespace MiniBank.Ledger.Application.Transactions.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(
    decimal Amount,
    string Currency,
    DateTime Date,
    string Category,
    string? Description,
    TransactionType Type
) : IRequest<Guid>;