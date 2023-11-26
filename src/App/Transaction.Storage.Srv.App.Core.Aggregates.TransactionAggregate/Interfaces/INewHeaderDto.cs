namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;

public interface INewHeaderDto
{
  public string Description { get; }

  public DateTimeOffset CommitDateTime { get; }
}