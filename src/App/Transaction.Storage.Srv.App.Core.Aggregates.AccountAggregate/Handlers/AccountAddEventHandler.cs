using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;

public class AccountAddEventHandler : EntityAddEventHandler<AccountAddEvent, Account, AccountDto>
{
  private readonly IReadRepositoryBase<CounterParty> _counterPartyRep;

  public AccountAddEventHandler(IReadRepositoryBase<CounterParty> AccountTypeRep, IRepositoryBase<Account> AccountRep, IEntityFactory<AccountAddEvent, Account> entityFactory) : base(AccountRep, entityFactory)
  {
    this._counterPartyRep = AccountTypeRep;
  }
  protected override async Task<Result> CheckDependency(AccountAddEvent request, CancellationToken cancellationToken)
  {
    var cpt = await _counterPartyRep.GetByIdAsync(request.CounterPartyId, cancellationToken);
    if (cpt is null)
      return Result.NotFound("CounterPartyId doesn't exist");
    return await base.CheckDependency(request, cancellationToken);
  }
}