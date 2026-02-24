using MiniBank.Ledger.Application.Common.Dtos;

namespace MiniBank.Ledger.Application.Common.Interfaces;

public interface IAccountRepository
{
    Task<List<AccountDto>> GetAllAsync(CancellationToken cancellationToken);
}