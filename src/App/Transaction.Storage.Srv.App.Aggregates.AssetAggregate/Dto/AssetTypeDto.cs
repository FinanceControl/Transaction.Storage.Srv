using System.ComponentModel.DataAnnotations;
using InsonusK.Shared.Validation;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;
public class AssetTypeDto
{
  public int Id { get; set; }

  public string Name { get; set; }

  public bool IsInflationProtected { get; set; }

  public bool IsUnderManagement { get; set; }


}
