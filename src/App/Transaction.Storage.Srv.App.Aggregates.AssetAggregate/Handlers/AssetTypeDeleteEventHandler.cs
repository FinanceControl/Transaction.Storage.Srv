using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Ardalis.Result;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Specifications;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;


namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;
public class AssetTypeDeleteEventHandler : EntityDeleteEventHandler<AssetTypeDeleteEvent, AssetType, AssetTypeDto>
{
  private readonly IRepositoryBase<Asset> assetRep;

  public AssetTypeDeleteEventHandler(IRepositoryBase<AssetType> repository, IRepositoryBase<Asset> assetRep) : base(repository)
  {
    this.assetRep = assetRep;
  }
  protected override async Task<Result> CheckDependency(AssetTypeDeleteEvent request, CancellationToken cancellationToken)
  {
    var assets_exist = await this.assetRep.AnyAsync(new AssetOfTypeSpec(request.Id), cancellationToken);
    if (assets_exist)
      return Result.Conflict("Asset for this type exist");
    return await base.CheckDependency(request, cancellationToken);
  }
}