using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Transaction.Storage.Srv.App.Components.AssetComponent.Models;
using Transaction.Storage.Srv.Shared.Database.Models;


namespace Transaction.Storage.Srv.App.Components.AssetComponent.Entity;

public partial class Asset : ConstantDomainEntity,IAssetDto
{
  private const int NameMaxLenght = 50;
  [MaxLength(NameMaxLenght)]
  [Required]
  public string Name { get; set; }

  [Required]
  public int AssetTypeId { get; private set; }
  public AssetType AssetType { get; private set; }
}
