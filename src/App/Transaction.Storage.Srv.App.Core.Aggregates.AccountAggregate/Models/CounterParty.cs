using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model.CounterParty;


public interface ICounterParty : IDomainModel, IBody
{
}

public interface IBody
{
  public string Name { get; }
  public int CounterPartyTypeId { get; }
}
