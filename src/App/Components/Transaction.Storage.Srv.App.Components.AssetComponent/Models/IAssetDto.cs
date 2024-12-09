using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Models;

public interface IAssetDto : IAssetBodyDto, IConstantDomainModelId
{
}
public interface IAssetBodyDto
{
  public string Name { get; }
  public int AssetTypeId { get; }
}