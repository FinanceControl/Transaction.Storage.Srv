using Transaction.Storage.Srv.App.Components.AssetComponent.Models;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
public class AssetDto : IAssetDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public short DecimalSize { get; set; }
  public int AssetTypeId { get; set; }
}
