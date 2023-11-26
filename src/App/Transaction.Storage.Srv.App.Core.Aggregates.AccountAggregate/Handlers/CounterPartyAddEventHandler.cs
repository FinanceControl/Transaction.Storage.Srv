using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;

public class CounterPartyAddEventHandler : IRequestHandler<CounterPartyAddEvent, Result<CounterPartyDto>>
{
  private readonly IReadRepositoryBase<CounterPartyType> counterPartyTypeRep;
  private readonly IRepositoryBase<CounterParty> counterPartyRep;

  public CounterPartyAddEventHandler(IReadRepositoryBase<CounterPartyType> counterPartyTypeRep, IRepositoryBase<CounterParty> counterPartyRep)
  {
    this.counterPartyTypeRep = counterPartyTypeRep;
    this.counterPartyRep = counterPartyRep;
  }
  public async Task<Result<CounterPartyDto>> Handle(CounterPartyAddEvent request, CancellationToken cancellationToken)
  {
    var assetType = await counterPartyTypeRep.GetByIdAsync(request.CounterPartyTypeId, cancellationToken);
    if (assetType is null)
      return Result.NotFound("CounterPartyTypeId doesn't exist");

    var build_result = CounterParty.BuildNew(request);
    if (!build_result.IsSuccess)
      return build_result.Map<CounterParty, CounterPartyDto>((at) => throw new ApplicationException("Unexpected result mapping"));

    var new_Asset = await counterPartyRep.AddAsync(build_result.Value, cancellationToken);
    return Result.Success(new_Asset.Adapt<CounterPartyDto>());
  }
}