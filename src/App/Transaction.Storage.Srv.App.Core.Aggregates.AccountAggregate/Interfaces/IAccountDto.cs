using Transcation.Storage.Srv.Shared.Database.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;

public interface IAccountDto : IDomainEntityDto
{

  public string Name { get; }

  public string Description { get; }

  public int CounterPartyId { get; }
  public bool IsUnderManagement { get; }
}