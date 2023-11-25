using System.ComponentModel.DataAnnotations;
using InsonusK.Shared.Validation;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;
public class AssetTypeDto
{
  public int Id { get; set; }

  public string Name { get; set; }

  public bool IsInflationProtected { get; set; }

  public bool IsUnderManagement { get; set; }


}
public static class Mapper
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
