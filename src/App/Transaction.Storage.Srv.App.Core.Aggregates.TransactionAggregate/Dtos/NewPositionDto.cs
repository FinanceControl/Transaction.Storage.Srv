using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

public class NewPositionDto : INewPositionDto
{
  public int? AccountId { get; set; }

  public int Amount { get; set; }

  public int AssetId { get; set; }
}