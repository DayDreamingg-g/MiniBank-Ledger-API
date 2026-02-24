using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniBank.Ledger.Application.Common.Dtos;
using MiniBank.Ledger.Application.Common.Interfaces;

namespace MiniBank.Ledger.Application.Transactions.Queries.GetTransactions;

public sealed class GetTransactionsQueryHandler
    : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
{
    private readonly ILedgerDbContext _db;

    public GetTransactionsQueryHandler(ILedgerDbContext db)
    {
        _db = db;
    }

    public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var q = _db.Transactions.AsNoTracking();

        if (request.From is not null)
            q = q.Where(x => x.Date >= request.From.Value);

        if (request.To is not null)
            q = q.Where(x => x.Date <= request.To.Value);

        return await q
            .OrderByDescending(x => x.Date)
            .Select(x => new TransactionDto(
                x.Id,
                x.Amount,
                x.Currency,
                x.Date,
                x.Category,
                x.Description,
                x.Type))
            .ToListAsync(cancellationToken);
    }
}