using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Mapster;
using Transaction.Storage.Srv.Shared.Validators;
using Transaction.Storage.Srv.App.Components.AssetComponent.Models;
using Transaction.Storage.Srv.App.Components.AssetComponent.Specifications;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Entity;

public partial class Asset
{
  public class Factory : IEntityFactory<AssetAddEvent, Asset>
  {
    private readonly IReadRepositoryBase<AssetType> assetTypeRep;
    private readonly IReadRepositoryBase<Asset> entityRep;

    public Factory(IReadRepositoryBase<AssetType> assetTypeRep, IReadRepositoryBase<Asset> entityRep)
    {
      this.assetTypeRep = assetTypeRep;
      this.entityRep = entityRep;
    }
    public async Task<Result<Asset>> BuildAsync(AssetAddEvent source, CancellationToken cancellationToken = default)
    {
      var source_result = await new Validator(assetTypeRep).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

      source_result = await new UIXValidator<Asset, IAssetBodyDto>(entityRep, [new AssetByNameSpec.Factory()]).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Conflict(source_result.Errors.Select(e => e.ErrorMessage).ToArray());

      var new_assertType = new Asset(source);

      return Result.Success(new_assertType);
    }
  }


  protected Asset()
  {
  }

#if DEBUG
  public Asset(string name = "123", int assertTypeId = 0)
  {
    Name = name;
    AssetTypeId = assertTypeId;
  }
#endif
  protected Asset(AssetAddEvent addEventDto)
  {
    addEventDto.Adapt(this);
  }

  public static implicit operator List<object>(Asset v)
  {
    throw new NotImplementedException();
  }
}