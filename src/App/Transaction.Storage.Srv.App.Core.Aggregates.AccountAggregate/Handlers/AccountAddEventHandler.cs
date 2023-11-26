using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;

public class AccountAddEventHandler : IRequestHandler<AccountAddEvent, Result<AccountDto>>
{
  private readonly IReadRepositoryBase<CounterParty> _counterPartyRep;
  private readonly IRepositoryBase<Account> _accountRep;

  public AccountAddEventHandler(IReadRepositoryBase<CounterParty> AccountTypeRep, IRepositoryBase<Account> AccountRep)
  {
    this._counterPartyRep = AccountTypeRep;
    this._accountRep = AccountRep;
  }
  public async Task<Result<AccountDto>> Handle(AccountAddEvent request, CancellationToken cancellationToken)
  {
    var cpt = await _counterPartyRep.GetByIdAsync(request.CounterPartyId, cancellationToken);
    if (cpt is null)
      return Result.NotFound("CounterPartyId doesn't exist");

    var build_result = Account.BuildNew(request);
    if (!build_result.IsSuccess)
      return build_result.Map<Account, AccountDto>((at) => throw new ApplicationException("Unexpected result mapping"));

    var new_Asset = await _accountRep.AddAsync(build_result.Value, cancellationToken);
    return Result.Success(new_Asset.Adapt<AccountDto>());
  }
}