
using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;
public class AssetDeleteEventHandler : IRequestHandler<AssetDeleteEvent, Result>
{
  private readonly IRepositoryBase<Asset> assetRep;

  public AssetDeleteEventHandler(IRepositoryBase<Asset> repository)
  {
    this.assetRep = repository;
  }

  public async Task<Result> Handle(AssetDeleteEvent request, CancellationToken cancellationToken)
  {
    var assertType = await assetRep.GetByIdAsync(request.Id, cancellationToken);
    if (assertType is null)
      return Result.NotFound();

    await assetRep.DeleteAsync(assertType, cancellationToken);
    return Result.Success();
  }
}