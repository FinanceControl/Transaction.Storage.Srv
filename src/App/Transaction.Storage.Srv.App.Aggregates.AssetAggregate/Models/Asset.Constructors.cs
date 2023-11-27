
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
      var source_result = await new Validator(assetTypeRep).ValidateAsync(source);
      if (!source_result.IsValid)
        return Result.Invalid(source_result.AsErrors());

      var assetType = await assetTypeRep.GetByIdAsync(source.AssetTypeId, cancellationToken);
      Guard.Against.Null(assetType);

      var new_assertType = new Asset(source)
      {
        AssetType = assetType
      };

      return Result.Success(new_assertType);
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