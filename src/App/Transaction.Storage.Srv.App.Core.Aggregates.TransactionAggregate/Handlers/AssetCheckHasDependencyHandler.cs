using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Specifications;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Handlers;

public class AssetCheckDependencyEventHandler : IRequestHandler<AssetCheckDependencyEvent, Result>
{
  private readonly IReadRepositoryBase<Position> positionRep;

  public AssetCheckDependencyEventHandler(IReadRepositoryBase<Position> positionRep)
  {
    this.positionRep = positionRep;
  }
  public async Task<Result> Handle(AssetCheckDependencyEvent request, CancellationToken cancellationToken)
  {
    var pos_with_acc =  await positionRep.ListAsync(new PositionsByAssetIdSpec(request.Id), cancellationToken);
    if (pos_with_acc.Count > 0)
      return Result.Conflict(pos_with_acc.Select(e=>$"Transation Header id {e.HeaderId} Position id {e.Id} related to Asset").ToArray());
      
    return Result.Success();      
  }
}