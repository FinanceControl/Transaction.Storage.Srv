using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Mappers;

public static class AssetMapper
{
  public static AssetDto ToDTO(this Asset asset)
  {
    return new AssetDto()
    {
      Id = asset.Id,
      Name = asset.Name,
      DecimalSize = asset.DecimalSize,
      AssetTypeId = asset.AssetTypeId
    };
  }
}