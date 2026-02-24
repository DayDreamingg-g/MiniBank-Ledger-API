using Microsoft.EntityFrameworkCore;
using MiniBank.Ledger.Domain.Entities;

namespace MiniBank.Ledger.Application.Common.Interfaces;

public interface ILedgerDbContext
{
    DbSet<Transaction> Transactions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}