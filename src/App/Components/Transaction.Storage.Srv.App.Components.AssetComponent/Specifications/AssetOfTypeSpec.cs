using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Specifications;
public class AssetOfTypeSpec : Specification<Asset>
{
  public AssetOfTypeSpec(int AssetTypeId)
  {
    Query.Where(c => c.AssetTypeId == AssetTypeId);
  }
}