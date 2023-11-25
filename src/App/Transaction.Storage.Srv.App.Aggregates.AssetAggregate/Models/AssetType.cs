using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transcation.Storage.Srv.Shared.Database;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

public partial class AssetType : EntityBase
{
  public static Result<AssetType> TryBuild(NewAssetTypeAddEvent newAssetTypeAddEvent){
    var new_assertType = new AssetType(newAssetTypeAddEvent);
    var result = new Validator().Validate(new_assertType);
    if (result.IsValid)
      return Result.Success(new_assertType);
    else
      return Result.Invalid(result.AsErrors());    
  }
  
  protected AssetType(NewAssetTypeAddEvent newAssetTypeAddEvent){
    this.Name = newAssetTypeAddEvent.Name;
    this.IsInflationProtected = newAssetTypeAddEvent.IsInflationProtected;
    this.IsUnderManagement = newAssetTypeAddEvent.IsUnderManagement;    
  }

  [MaxLength(50)]
  public string Name { get; private set; }

  [Display(Name = "Are assets protected against inflation")]
  public bool IsInflationProtected { get; private set; }

  [Display(Name = "Are assets controlled automatically")]
  public bool IsUnderManagement {get;private set;}

  public ICollection<Asset> Assets { get; }
}