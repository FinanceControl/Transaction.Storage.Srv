using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Ardalis.Result;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Specifications;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;
public class AssetTypeDeleteEventHandler : IRequestHandler<AssetTypeDeleteEvent, Result>
{
  private readonly IRepositoryBase<AssetType> assetTypeRep;
  private readonly IRepositoryBase<Asset> assetRep;

  public AssetTypeDeleteEventHandler(IRepositoryBase<AssetType> repository, IRepositoryBase<Asset> assetRep)
  {
    this.assetTypeRep = repository;
    this.assetRep = assetRep;
  }

  public async Task<Result> Handle(AssetTypeDeleteEvent request, CancellationToken cancellationToken)
  {
    var assertType = await assetTypeRep.GetByIdAsync(request.Id, cancellationToken);
    if (assertType is null)
      return Result.NotFound();

    if (request.IsForced == false)
    {
      var assets_exist = await this.assetRep.AnyAsync(new AssetOfTypeSpec(request.Id), cancellationToken);
      if (assets_exist)
        return Result.Conflict("Asset for this type exist");
    }
    
    await assetTypeRep.DeleteAsync(assertType, cancellationToken);
    return Result.Success();
  }
}