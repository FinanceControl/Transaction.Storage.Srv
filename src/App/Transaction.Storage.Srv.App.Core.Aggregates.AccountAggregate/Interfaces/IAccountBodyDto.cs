namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;

public interface IAccountBodyDto
{
  public string Name { get; }

  public string Description { get; }

  public int CounterPartyId { get; }
  public bool IsUnderManagement { get; }
}
