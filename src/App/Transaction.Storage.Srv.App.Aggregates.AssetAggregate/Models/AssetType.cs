using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public partial class AssetType : DomainEntity, IAssetTypeDto
{
  private const int NameMaxLenght = 50;
  [MaxLength(NameMaxLenght)]
  public string Name { get; private set; }

  [Display(Name = "Are assets protected against inflation")]
  public bool IsInflationProtected { get; private set; }

  [Display(Name = "Are assets controlled automatically")]
  public bool IsUnderManagement { get; private set; }

  public ICollection<Asset> Assets { get; }
}