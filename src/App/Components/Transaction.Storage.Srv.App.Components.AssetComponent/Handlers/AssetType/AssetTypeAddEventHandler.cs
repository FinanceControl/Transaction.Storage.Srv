using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Microsoft.Extensions.Logging;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Handlers;

public class AssetTypeAddEventHandler : EntityAddEventHandler<AssetTypeAddEvent, AssetType, AssetTypeDto>
{

  public AssetTypeAddEventHandler(
        IRepositoryBase<AssetType> repository,
        IEntityFactory<AssetTypeAddEvent, AssetType> entityFactory,
        ILogger<AssetTypeAddEventHandler> logger)
      : base(repository, entityFactory, logger)
  {
  }
}