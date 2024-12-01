using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;

public class AssetTypeAddEventHandler : OldEntityAddEventHandler<AssetTypeAddEvent, AssetType, AssetTypeDto>
{

  public AssetTypeAddEventHandler(IRepositoryBase<AssetType> repository, IOldEntityFactory<AssetTypeAddEvent, AssetType> entityFactory) : base(repository, entityFactory)
  {
  }
}