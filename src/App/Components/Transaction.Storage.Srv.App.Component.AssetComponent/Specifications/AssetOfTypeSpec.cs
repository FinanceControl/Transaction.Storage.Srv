using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Specifications;
public class AssetOfTypeSpec : Specification<Asset>
{
  public AssetOfTypeSpec(int AssetTypeId)
  {
    Query.Where(c => c.AssetTypeId == AssetTypeId);
  }
}