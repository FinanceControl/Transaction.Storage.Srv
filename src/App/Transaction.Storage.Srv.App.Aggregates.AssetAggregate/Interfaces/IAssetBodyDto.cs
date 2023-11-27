namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Interfaces;

public interface IAssetBodyDto
{
  public string Name { get; }
  public short DecimalSize { get; }
  public int AssetTypeId { get; }
}
