using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Mapster;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
namespace Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
public partial class AssetType
{
  public class Factory : IEntityFactory<AssetTypeAddEvent, AssetType>
  {
    public Task<Result<AssetType>> BuildAsync(AssetTypeAddEvent source, CancellationToken cancellationToken = default)
    {
      var new_assertType = new AssetType(source);
      var result = new Validator().Validate(new_assertType);
      if (result.IsValid)
        return Task.FromResult(Result.Success(new_assertType));
      else
        return Task.FromResult((Result<AssetType>)Result.Invalid(result.AsErrors()));
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