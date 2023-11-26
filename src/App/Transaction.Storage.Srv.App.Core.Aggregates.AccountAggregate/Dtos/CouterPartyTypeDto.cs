using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
public class CounterPartyTypeDto : ICounterPartyTypeDto
{
  public int Id { get; set; }

  public string Name { get; set; }
}