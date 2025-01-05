using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Models;
using Transaction.Storage.Srv.Shared.Database.Specification;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Specifications;
public class AssetByNameSpec :Specification<Asset>
{
    public class Factory : UIXSpecificationFactory<Asset,IAssetBodyDto>
    {
        public Factory() : base([nameof(Asset.Name)], true)
        {
        }

        public override Specification<Asset> Build(IAssetBodyDto entity)
        {
            return new AssetByNameSpec(entity);
        }
    }

    public AssetByNameSpec(string name)
    {
        Query.Where(e => e.Name == name).Take(1);
    }

    public AssetByNameSpec(IAssetBodyDto dto) : this(dto.Name)
    {
    }
}