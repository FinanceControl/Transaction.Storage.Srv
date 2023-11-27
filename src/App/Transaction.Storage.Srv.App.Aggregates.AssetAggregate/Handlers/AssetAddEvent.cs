using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;

public class AssetAddEventHandler : EntityAddEventHandler<AssetAddEvent, Asset, AssetDto>
{
  public AssetAddEventHandler(IRepositoryBase<Asset> assetRep, IEntityFactory<AssetAddEvent, Asset> entityFactory) : base(assetRep, entityFactory)
  {
  }
}