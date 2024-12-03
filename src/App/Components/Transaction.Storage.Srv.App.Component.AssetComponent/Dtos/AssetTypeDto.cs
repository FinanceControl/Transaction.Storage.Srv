using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
public class AssetTypeDto : IAssetTypeDto
{
  public int Id { get; set; }

  public string Name { get; set; }

  public bool IsInflationProtected { get; set; }

  public bool IsUnderManagement { get; set; }
}
