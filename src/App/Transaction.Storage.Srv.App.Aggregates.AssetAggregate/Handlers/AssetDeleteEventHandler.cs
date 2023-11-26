
using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;
public class AssetDeleteEventHandler : EntityDeleteEventHandler<AssetDeleteEvent, Asset>
{
  public AssetDeleteEventHandler(IRepositoryBase<Asset> repository) : base(repository)
  {
  }
}