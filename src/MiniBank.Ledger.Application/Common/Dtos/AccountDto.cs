namespace MiniBank.Ledger.Application.Common.Dtos;

public record AccountDto(
    Guid Id,
    string Name,
    string Currency,
    decimal Balance
);