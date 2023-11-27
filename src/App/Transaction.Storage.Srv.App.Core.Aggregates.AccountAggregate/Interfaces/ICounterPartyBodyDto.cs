namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;

public interface ICounterPartyBodyDto
{
  public string Name { get; }
  public int CounterPartyTypeId { get; }
}
