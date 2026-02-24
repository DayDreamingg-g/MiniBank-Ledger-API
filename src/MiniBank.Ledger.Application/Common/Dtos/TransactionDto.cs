using MiniBank.Ledger.Domain.Enums;

namespace MiniBank.Ledger.Application.Common.Dtos;

public sealed record TransactionDto(
    Guid Id,
    decimal Amount,
    string Currency,
    DateTime Date,
    string Category,
    string Description,
    TransactionType Type
);