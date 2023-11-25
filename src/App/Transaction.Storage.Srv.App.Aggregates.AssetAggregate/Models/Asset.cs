using System.ComponentModel.DataAnnotations;
using InsonusK.Shared.Validation;
using Microsoft.EntityFrameworkCore;
using Transcation.Storage.Srv.Shared.Database;


namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

[Index(nameof(Name), IsUnique = true)]
public class Asset : EntityBase
{
  private string name;

  public static Asset BuildNew(string Name, short DecimalSize, int AssetTypeId)
  {
    return null;
  }
  protected Asset()
  {
  }
  [MaxLength(50)]
  [Required]
  public string Name
  {
    get => name; 
    private set {

      name = value;
    }
  }

  [Display(Name = "Lenght of decimal part in int amount value")]
  [Range(0, short.MaxValue)]
  public short DecimalSize { get; private set; }

  [Required]
  public int AssetTypeId { get; private set; }
  public AssetType AssetType { get; private set; }

}
