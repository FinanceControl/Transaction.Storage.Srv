using System.ComponentModel.DataAnnotations;
using InsonusK.Shared.Validation;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;
public class AssetDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public short DecimalSize { get; set; }
  public int AssetTypeId { get; set; }
}
