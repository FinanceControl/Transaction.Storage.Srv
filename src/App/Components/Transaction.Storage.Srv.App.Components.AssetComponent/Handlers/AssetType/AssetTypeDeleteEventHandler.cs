using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Ardalis.Result;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Specifications;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;


namespace Transaction.Storage.Srv.App.Components.AssetComponent.Handlers;
public class AssetTypeDeleteEventHandler : EntityDeleteEventHandler<AssetTypeDeleteEvent, AssetType, AssetTypeDto>
{
  private readonly IRepositoryBase<Asset> assetRep;

  public AssetTypeDeleteEventHandler(IRepositoryBase<AssetType> repository, IRepositoryBase<Asset> assetRep) : base(repository)
  {
    this.assetRep = assetRep;
  }
  protected override async Task<Result> CheckDependency(AssetTypeDeleteEvent request, CancellationToken cancellationToken)
  {
    var assets_exist = await assetRep.AnyAsync(new AssetOfTypeSpec(request.Id), cancellationToken);
    if (assets_exist)
      return Result.Conflict("Asset for this type exist");
    return await base.CheckDependency(request, cancellationToken);
  }
}