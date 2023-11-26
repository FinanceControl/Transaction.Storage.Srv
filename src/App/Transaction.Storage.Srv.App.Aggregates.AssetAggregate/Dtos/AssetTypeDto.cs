using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
public class AssetTypeDto : IAssetTypeDto
{
  public int Id { get; set; }

  public string Name { get; set; }

  public bool IsInflationProtected { get; set; }

  public bool IsUnderManagement { get; set; }

  public DateTimeOffset CreatedDateTime { get; set; }

  public DateTimeOffset UpdatedDateTime { get; set; }
}
