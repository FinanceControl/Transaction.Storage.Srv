using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Mappers;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;

public class AssetAddEventHandler : IRequestHandler<AssetAddEvent, Result<AssetDto>>
{
  private readonly IRepositoryBase<Asset> repository;
  private readonly IReadRepositoryBase<AssetType> assetTypeRep;

  public AssetAddEventHandler(IRepositoryBase<Asset> assetRep, IReadRepositoryBase<AssetType> assetTypeRep)
  {
    this.repository = assetRep;
    this.assetTypeRep = assetTypeRep;
  }

  public async Task<Result<AssetDto>> Handle(AssetAddEvent request, CancellationToken cancellationToken)
  {
    var assetType = await assetTypeRep.GetByIdAsync(request.AssetTypeId, cancellationToken);
    if (assetType is null)
      return Result.NotFound("AssetTypeId doesn't exist");

    var build_result = Asset.BuildNew(request);
    if (!build_result.IsSuccess)
      return build_result.Map<Asset, AssetDto>((at) => throw new ApplicationException("Unexpected result mapping"));

    var new_Asset = await repository.AddAsync(build_result.Value, cancellationToken);
    return Result.Success(new_Asset.ToDTO());
  }
}