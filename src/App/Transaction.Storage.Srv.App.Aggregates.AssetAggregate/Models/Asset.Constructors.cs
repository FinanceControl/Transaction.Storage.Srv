
using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

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
      var assetType = await assetTypeRep.GetByIdAsync(source.AssetTypeId, cancellationToken);
      Guard.Against.Null(assetType);

      var new_assertType = new Asset(source)
      {
        AssetType = assetType
      };

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

  public static implicit operator List<object>(Asset v)
  {
    throw new NotImplementedException();
  }
}