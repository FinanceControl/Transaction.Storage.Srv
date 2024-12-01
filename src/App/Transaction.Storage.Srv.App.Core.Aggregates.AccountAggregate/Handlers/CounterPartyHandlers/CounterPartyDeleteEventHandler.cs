using Ardalis.Result;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Specifications;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.CounterPartyEvents;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers.CounterPartyHandlers;

public class DeleteEventHandler : EntityDeleteEventHandler<CounterPartyDeleteEvent, CounterParty, CounterPartyDto>
{
  private readonly IReadRepositoryBase<Account> _accountRep;

  public DeleteEventHandler(IReadRepositoryBase<Account> account, IRepositoryBase<CounterParty> counterPartyRep) : base(counterPartyRep)
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