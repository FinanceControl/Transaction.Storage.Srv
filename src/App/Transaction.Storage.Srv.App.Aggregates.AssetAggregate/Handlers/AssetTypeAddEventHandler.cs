using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Ardalis.Result;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Mapster;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;

public class AssetTypeAddEventHandler : IRequestHandler<AssetTypeAddEvent, Result<AssetTypeDto>>
{
  private readonly IRepositoryBase<AssetType> repository;

  public AssetTypeAddEventHandler(IRepositoryBase<AssetType> repository)
  {
    this.repository = repository;
  }

  public async Task<Result<AssetTypeDto>> Handle(AssetTypeAddEvent request, CancellationToken cancellationToken)
  {
    var build_result = AssetType.TryBuild(request);
    if (!build_result.IsSuccess)
      return build_result.Map<AssetType, AssetTypeDto>((at) => throw new ApplicationException("Unexpected result mapping"));

    var new_assetType = await repository.AddAsync(build_result.Value, cancellationToken);
    return Result.Success(new_assetType.Adapt<AssetTypeDto>());
  }
}