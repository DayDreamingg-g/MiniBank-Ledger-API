using MediatR;
using MiniBank.Ledger.Application.Common.Interfaces;
using MiniBank.Ledger.Domain.Entities;

namespace MiniBank.Ledger.Application.Transactions.Commands.CreateTransaction;

public sealed class CreateTransactionCommandHandler
    : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly ILedgerDbContext _db;

    public CreateTransactionCommandHandler(ILedgerDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var tx = new Transaction(
            amount: request.Amount,
            currency: request.Currency,
            date: request.Date,
            category: request.Category,
            description: request.Description ?? string.Empty,
            type: request.Type);

        _db.Transactions.Add(tx);
        await _db.SaveChangesAsync(cancellationToken);

        return tx.Id;
    }
}