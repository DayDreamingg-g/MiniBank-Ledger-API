using MiniBank.Ledger.Application.Common.Dtos;
using MiniBank.Ledger.Application.Common.Interfaces;

namespace MiniBank.Ledger.Infrastructure.Repositories;

public sealed class InMemoryAccountRepository : IAccountRepository
{
    public Task<List<AccountDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var data = new List<AccountDto>
        {
            new(Guid.NewGuid(), "Main", "UAH", 1000m),
            new(Guid.NewGuid(), "Savings", "USD", 250m),
        };

        return Task.FromResult(data);
    }
}