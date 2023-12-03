using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Interfaces;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;

public class AssetUpdateEventHandler : IRequestHandler<AssetUpdateEvent, Result<AssetDto>>
{
  private readonly IEntityFactory<IAssetBodyDto, Asset> entityFactory;

  public AssetUpdateEventHandler(IRepositoryBase<Asset> assetRep, IEntityFactory<AssetAddEvent, Asset> entityFactory)
  {
    this.entityFactory = entityFactory;
  }

  public Task<Result<AssetDto>> Handle(AssetUpdateEvent request, CancellationToken cancellationToken)
  {
    var new_state = entityFactory.BuildAsync(request)
    var asset = asset
  }
}