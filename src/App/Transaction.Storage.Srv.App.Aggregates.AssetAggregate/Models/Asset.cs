using System.ComponentModel.DataAnnotations;
using InsonusK.Shared.Validation;
using Microsoft.EntityFrameworkCore;
using Transcation.Storage.Srv.Shared.Database;


namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public partial class Asset : DomainEntity
{

  [MaxLength(50)]
  [Required]
  public string Name { get; private set; }

  [Display(Name = "Lenght of decimal part in int amount value")]
  [Range(0, short.MaxValue)]
  public short DecimalSize { get; private set; }

  [Required]
  public int AssetTypeId { get; private set; }
  public AssetType AssetType { get; private set; }

}
