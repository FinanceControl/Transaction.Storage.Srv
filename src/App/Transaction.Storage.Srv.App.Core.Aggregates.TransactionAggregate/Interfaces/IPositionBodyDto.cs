namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;

public interface IPositionBodyDto
{
  public int? AccountId { get; }

  public decimal Amount { get; }

  public int AssetId { get; }
}