using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Models;
using Transaction.Storage.Srv.App.Components.AssetComponent.Specifications;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;
namespace Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
public partial class AssetType
{
  public class Factory : IEntityFactory<AssetTypeAddEvent, AssetType>
  {
    private readonly IReadRepositoryBase<AssetType> entityRep;

    public Factory(IReadRepositoryBase<AssetType> entityRep)
    {
      this.entityRep = entityRep;
    }
    public async Task<Result<AssetType>> BuildAsync(AssetTypeAddEvent source, CancellationToken cancellationToken = default)
    {

      var source_result = await new Validator().ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

      source_result = await new UIXValidator<AssetType, IAssetTypeBody>(entityRep, [new AssetTypeByNameSpec.Factory()]).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Conflict(source_result.Errors.Select(e => e.ErrorMessage).ToArray());

      var new_assertType = new AssetType(source);

      return Result.Success(new_assertType);
    }
  }
  
  protected AssetType()
  {

  }
  protected AssetType(AssetTypeAddEvent addEventDto)
  {
    addEventDto.Adapt(this);
  }
}