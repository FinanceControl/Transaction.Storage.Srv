using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

public class NewPositionDto : IPositionBodyDto
{
  public int? AccountId { get; set; }

  public int Amount { get; set; }

  public int AssetId { get; set; }
}