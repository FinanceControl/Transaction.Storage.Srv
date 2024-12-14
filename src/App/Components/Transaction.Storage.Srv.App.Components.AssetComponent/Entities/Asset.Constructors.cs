using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Mapster;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Entity;

public partial class Asset
{
  public class Factory : IEntityFactory<AssetAddEvent, Asset>
  {
    private readonly IReadRepositoryBase<AssetType> assetTypeRep;

    public Factory(IReadRepositoryBase<AssetType> assetTypeRep)
    {
      this.assetTypeRep = assetTypeRep;
    }
    public async Task<Result<Asset>> BuildAsync(AssetAddEvent source, CancellationToken cancellationToken = default)
    {
      var source_result = await new Validator(assetTypeRep).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

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