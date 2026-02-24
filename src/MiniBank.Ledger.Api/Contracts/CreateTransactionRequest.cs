using MiniBank.Ledger.Domain.Enums;

namespace MiniBank.Ledger.Api.Contracts;

public sealed record CreateTransactionRequest(
    decimal Amount,
    string Currency,
    DateTime Date,
    string Category,
    string? Description,
    TransactionType Type
);