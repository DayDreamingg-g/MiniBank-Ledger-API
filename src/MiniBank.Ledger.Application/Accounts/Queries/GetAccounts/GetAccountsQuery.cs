using MediatR;
using MiniBank.Ledger.Application.Common.Dtos;

namespace MiniBank.Ledger.Application.Accounts.Queries.GetAccounts;

public record GetAccountsQuery() : IRequest<List<AccountDto>>;