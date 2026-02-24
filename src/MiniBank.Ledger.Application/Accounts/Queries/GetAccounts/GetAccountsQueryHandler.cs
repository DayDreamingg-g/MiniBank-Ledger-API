using MediatR;
using MiniBank.Ledger.Application.Common.Dtos;
using MiniBank.Ledger.Application.Common.Interfaces;

namespace MiniBank.Ledger.Application.Accounts.Queries.GetAccounts;

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountDto>>
{
    private readonly IAccountRepository _repository;

    public GetAccountsQueryHandler(IAccountRepository repository)
        => _repository = repository;

    public Task<List<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);
}