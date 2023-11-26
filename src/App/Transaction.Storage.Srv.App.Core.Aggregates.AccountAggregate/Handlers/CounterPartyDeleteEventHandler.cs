using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Specifications;
using Transaction.Storage.Srv.Shared.Events.Handlers;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;

public class CounterPartyDeleteEventHandler : EntityDeleteEventHandler<CounterPartyDeleteEvent, CounterParty, CounterPartyDto>
{
  private readonly IReadRepositoryBase<Account> _accountRep;

  public CounterPartyDeleteEventHandler(IReadRepositoryBase<Account> account, IRepositoryBase<CounterParty> counterPartyRep) : base(counterPartyRep)
  {
    _accountRep = account;
  }
  protected override async Task<Result> CheckDependency(CounterPartyDeleteEvent request,
                                                       CancellationToken cancellationToken)
  {
    var assets_exist = await _accountRep.AnyAsync(new AccountsOfCounterPartySpec(request.Id), cancellationToken);
    if (assets_exist)
      return Result.Conflict("Account for this CounterParty exist");
    return await base.CheckDependency(request, cancellationToken);
  }
}