using Ardalis.Result;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Specifications;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.CounterPartyEvents;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Handlers.CounterPartyHandlers;

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