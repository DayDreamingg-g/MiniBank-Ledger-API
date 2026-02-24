namespace MiniBank.Ledger.Application.Common.Dtos;

public sealed record SummaryReportDto(
    DateTime? From,
    DateTime? To,
    decimal TotalIncome,
    decimal TotalExpense,
    decimal Net
);