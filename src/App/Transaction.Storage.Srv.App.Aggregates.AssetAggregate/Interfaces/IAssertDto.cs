using Transcation.Storage.Srv.Shared.Database.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Interfaces;

public interface IAssetDto : IDomainEntityDto
{
  public string Name { get; }
  public short DecimalSize { get; }
  public int AssetTypeId { get; }
}