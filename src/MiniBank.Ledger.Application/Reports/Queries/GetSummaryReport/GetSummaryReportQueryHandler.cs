using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniBank.Ledger.Application.Common.Dtos;
using MiniBank.Ledger.Application.Common.Interfaces;
using MiniBank.Ledger.Domain.Enums;

namespace MiniBank.Ledger.Application.Reports.Queries.GetSummaryReport;

public sealed class GetSummaryReportQueryHandler
    : IRequestHandler<GetSummaryReportQuery, SummaryReportDto>
{
    private readonly ILedgerDbContext _db;

    public GetSummaryReportQueryHandler(ILedgerDbContext db)
    {
        _db = db;
    }

    public async Task<SummaryReportDto> Handle(GetSummaryReportQuery request, CancellationToken cancellationToken)
    {
        var q = _db.Transactions.AsNoTracking();

        if (request.From is not null)
            q = q.Where(x => x.Date >= request.From.Value);

        if (request.To is not null)
            q = q.Where(x => x.Date <= request.To.Value);

        var totalIncome = await q
            .Where(x => x.Type == TransactionType.Income)
            .SumAsync(x => (decimal?)x.Amount, cancellationToken) ?? 0m;

        var totalExpense = await q
            .Where(x => x.Type == TransactionType.Expense)
            .SumAsync(x => (decimal?)x.Amount, cancellationToken) ?? 0m;

        return new SummaryReportDto(
            From: request.From,
            To: request.To,
            TotalIncome: totalIncome,
            TotalExpense: totalExpense,
            Net: totalIncome - totalExpense
        );
    }
}