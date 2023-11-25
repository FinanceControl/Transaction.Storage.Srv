using System.ComponentModel.DataAnnotations;
using InsonusK.Shared.Validation;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dto;
public class AssetDto
{
  public int Id { get; set; }
  public string Name { get; private set; }
  public short DecimalSize { get; private set; }
  public int AssetTypeId { get; private set; }
}