using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Ardalis.Result;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Mapster;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;

public class AssetTypeAddEventHandler : EntityAddEventHandler<AssetTypeAddEvent, AssetType, AssetTypeDto>
{

  public AssetTypeAddEventHandler(IRepositoryBase<AssetType> repository, IEntityFactory<AssetTypeAddEvent, AssetType> entityFactory) : base(repository, entityFactory)
  {
  }
}