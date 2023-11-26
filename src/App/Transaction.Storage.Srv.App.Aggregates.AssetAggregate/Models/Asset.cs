using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Interfaces;
using Transcation.Storage.Srv.Shared.Database.Models;


namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public partial class Asset : DomainEntity,IAssetDto
{
  private const int NameMaxLenght = 50;
  [MaxLength(NameMaxLenght)]
  [Required]
  public string Name { get; set; }

  [Display(Name = "Lenght of decimal part in int amount value")]
  [Range(0, short.MaxValue)]
  public short DecimalSize { get; private set; }

  [Required]
  public int AssetTypeId { get; private set; }
  public AssetType AssetType { get; private set; }

}
