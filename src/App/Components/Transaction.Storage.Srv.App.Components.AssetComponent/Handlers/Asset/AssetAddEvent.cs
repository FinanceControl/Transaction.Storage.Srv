using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Microsoft.Extensions.Logging;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Handlers;

public class AssetAddEventHandler : EntityAddEventHandler<AssetAddEvent, Asset, AssetDto>
{
  public AssetAddEventHandler(
      IRepositoryBase<Asset> assetRep, 
      IEntityFactory<AssetAddEvent, Asset> entityFactory,
      ILogger<AssetAddEventHandler> logger) 
      : base(assetRep, entityFactory,logger)
  {
  }
}