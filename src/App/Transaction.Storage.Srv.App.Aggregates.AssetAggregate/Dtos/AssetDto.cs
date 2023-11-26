using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
public class AssetDto : IAssetDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public short DecimalSize { get; set; }
  public int AssetTypeId { get; set; }

  public DateTimeOffset CreatedDateTime { get; set; }

  public DateTimeOffset UpdatedDateTime { get; set; }
}
