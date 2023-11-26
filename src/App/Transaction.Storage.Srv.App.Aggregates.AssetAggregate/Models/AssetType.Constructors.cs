using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
public partial class AssetType
{
  public class Factory : IEntityFactory<AssetTypeAddEvent, AssetType>
  {
    public Result<AssetType> Build(AssetTypeAddEvent source)
    {
      var new_assertType = new AssetType(source);
      var result = new Validator().Validate(new_assertType);
      if (result.IsValid)
        return Result.Success(new_assertType);
      else
        return Result.Invalid(result.AsErrors());
    }
  }
  protected AssetType()
  {
  }
  protected AssetType(AssetTypeAddEvent newAssetTypeAddEvent)
  {
    this.Name = newAssetTypeAddEvent.Name;
    this.IsInflationProtected = newAssetTypeAddEvent.IsInflationProtected;
    this.IsUnderManagement = newAssetTypeAddEvent.IsUnderManagement;
  }
}