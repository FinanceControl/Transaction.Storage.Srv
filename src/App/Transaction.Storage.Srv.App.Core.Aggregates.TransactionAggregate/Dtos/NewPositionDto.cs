using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

public class NewPositionDto : IPositionBodyDto
{
  public int? AccountId { get; set; }

  public decimal Amount { get; set; }

  public int AssetId { get; set; }
}