using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Mappers;

public static class AssetTypeMapper
{
  public static AssetTypeDto ToDTO(this AssetType assetType)
  {
    return new AssetTypeDto()
    {
      Id = assetType.Id,
      Name = assetType.Name,
      IsInflationProtected = assetType.IsInflationProtected,
      IsUnderManagement = assetType.IsUnderManagement
    };
  }
}
