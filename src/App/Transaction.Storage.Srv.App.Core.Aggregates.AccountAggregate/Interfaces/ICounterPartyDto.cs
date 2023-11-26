using Transcation.Storage.Srv.Shared.Database.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;

public interface ICounterPartyDto : IDomainEntityDto
{
  public string Name { get; }
  public int CounterPartyTypeId { get; }
}