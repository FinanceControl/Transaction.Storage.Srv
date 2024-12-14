using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Models;
using Transaction.Storage.Srv.Shared.Database.Specification;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Specifications;
public class AssetTypeByNameSpec :Specification<AssetType>
{
    public class Factory : UIXSpecificationFactory<AssetType,IAssetTypeBody>
    {
        public Factory() : base([nameof(Asset.Name)], true)
        {
        }

        public override Specification<AssetType> Build(IAssetTypeBody entity)
        {
            return new AssetTypeByNameSpec(entity);
        }
    }

    public AssetTypeByNameSpec(string name)
    {
        Query.Where(e => e.Name == name).Take(1);
    }

    public AssetTypeByNameSpec(IAssetTypeBody dto) : this(dto.Name)
    {
    }
}