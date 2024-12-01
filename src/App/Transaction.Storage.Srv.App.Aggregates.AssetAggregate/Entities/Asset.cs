using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.Shared.Database.Models;


namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;

public partial class Asset : OldDomainEntity,IAssetDto
{
  private const int NameMaxLenght = 50;
  [MaxLength(NameMaxLenght)]
  [Required]
  public string Name { get; set; }

  [Display(Name = "Lenght of decimal part in int amount value")]
  public short DecimalSize { get; private set; }

  [Required]
  public int AssetTypeId { get; private set; }
  public AssetType AssetType { get; private set; }



  public Result checkDecimalLenght(decimal amount)
  {
    var amount_prepared = amount * (long)Math.Pow(10, DecimalSize);

    if (amount_prepared != Math.Truncate(amount_prepared))
    {
      return Result.Invalid(new ValidationError("Decimal part is too long"));
    }
    return Result.Success();
  }
}
