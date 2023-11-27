using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;
public class AssetDeleteEventHandler : EntityDeleteEventHandler<AssetDeleteEvent, Asset, AssetDto>
{
  private readonly IMediator mediator;

  public AssetDeleteEventHandler(IRepositoryBase<Asset> repository, IMediator mediator) : base(repository)
  {
    this.mediator = mediator;
  }

  protected override async Task<Result> CheckDependency(AssetDeleteEvent request, CancellationToken cancellationToken)
  {
    var checkEv = new AssetCheckDependencyEvent() { Id = request.Id };

    var result = await mediator.Send(checkEv, cancellationToken);
    if (!result.IsSuccess)
      if (result.Status == ResultStatus.Conflict)
        return Result.Conflict(new[] { "Asset has dependency" }.Concat(result.Errors).ToArray());
      else
        return Result.CriticalError("Unexpected error");

    return await base.CheckDependency(request, cancellationToken);
  }
}