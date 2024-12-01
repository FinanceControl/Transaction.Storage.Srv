using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;


public interface ICounterParty : IDomainModel, ICounterPartyBody
{
}

public interface ICounterPartyBody
{
  public string Name { get; }
  public int CounterPartyTypeId { get; }
}
