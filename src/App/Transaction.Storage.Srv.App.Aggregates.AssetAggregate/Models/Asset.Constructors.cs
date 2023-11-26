
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public partial class Asset
{
  public class Factory : IEntityFactory<AssetAddEvent, Asset>
  {
    public Result<Asset> Build(AssetAddEvent source)
    {
      var new_assertType = new Asset(source);
      var result = new Validator().Validate(new_assertType);
      if (result.IsValid)
        return Result.Success(new_assertType);
      else
        return Result.Invalid(result.AsErrors());
    }
  }


  protected Asset()
  {
  }

  protected Asset(AssetAddEvent assetAddEventDto)
  {
    Name = assetAddEventDto.Name;
    AssetTypeId = assetAddEventDto.AssetTypeId;
    DecimalSize = assetAddEventDto.DecimalSize;
  }
}