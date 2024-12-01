using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;
public partial class AssetType
{
  public class Factory : IOldEntityFactory<AssetTypeAddEvent, AssetType>
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
  protected AssetType(AssetTypeAddEvent newAssetTypeAddEvent)
  {
    this.Name = newAssetTypeAddEvent.Name;
    this.IsInflationProtected = newAssetTypeAddEvent.IsInflationProtected;
    this.IsUnderManagement = newAssetTypeAddEvent.IsUnderManagement;
  }
}