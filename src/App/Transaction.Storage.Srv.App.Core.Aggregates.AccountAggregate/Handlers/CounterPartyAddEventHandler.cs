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

public class CounterPartyAddEventHandler : EntityAddEventHandler<CounterPartyAddEvent, CounterParty, CounterPartyDto>
{
  private readonly IReadRepositoryBase<CounterPartyType> counterPartyTypeRep;

  public CounterPartyAddEventHandler(IReadRepositoryBase<CounterPartyType> counterPartyTypeRep,
                                     IRepositoryBase<CounterParty> counterPartyRep,
                                     IEntityFactory<CounterPartyAddEvent, CounterParty> entityFactory) :
                                    base(counterPartyRep, entityFactory)
  {
    this.counterPartyTypeRep = counterPartyTypeRep;
  }
  protected override async Task<Result> CheckDependency(CounterPartyAddEvent request, CancellationToken cancellationToken)
  {
    var assetType = await counterPartyTypeRep.GetByIdAsync(request.CounterPartyTypeId, cancellationToken);
    if (assetType is null)
      return Result.NotFound("CounterPartyTypeId doesn't exist");
    return await base.CheckDependency(request, cancellationToken);
  }
}