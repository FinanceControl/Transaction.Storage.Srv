using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;

public class AssetAddEventHandler : EntityAddEventHandler<AssetAddEvent, Asset, AssetDto>
{
  private readonly IReadRepositoryBase<AssetType> assetTypeRep;

  public AssetAddEventHandler(IRepositoryBase<Asset> assetRep, IReadRepositoryBase<AssetType> assetTypeRep, IEntityFactory<AssetAddEvent, Asset> entityFactory) : base(assetRep, entityFactory)
  {
    this.assetTypeRep = assetTypeRep;
  }
  protected override async Task<Result> CheckDependency(AssetAddEvent request, CancellationToken cancellationToken)
  {
    var assetType = await assetTypeRep.GetByIdAsync(request.AssetTypeId, cancellationToken);
    if (assetType is null)
      return Result.NotFound("AssetTypeId doesn't exist");
    return await base.CheckDependency(request, cancellationToken);
  }
}