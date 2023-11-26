using System.ComponentModel.DataAnnotations;
using Transcation.Storage.Srv.Shared.Database;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public partial class AssetType : DomainEntity
{
  [MaxLength(50)]
  public string Name { get; private set; }

  [Display(Name = "Are assets protected against inflation")]
  public bool IsInflationProtected { get; private set; }

  [Display(Name = "Are assets controlled automatically")]
  public bool IsUnderManagement { get; private set; }

  public ICollection<Asset> Assets { get; }
}