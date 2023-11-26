namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;

public interface INewPositionDto
{
  public int? AccountId { get; }

  public int Amount { get; }

  public int AssetId { get; }
}