using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public interface IAssetDto : IAssetBodyDto, IConstantDomainModel
{
}
public interface IAssetBodyDto
{
  public string Name { get; }
  public short DecimalSize { get; }
  public int AssetTypeId { get; }
}