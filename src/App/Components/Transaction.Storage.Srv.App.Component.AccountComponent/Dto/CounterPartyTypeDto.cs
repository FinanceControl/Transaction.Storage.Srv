using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;

public class CounterPartyTypeDto : ICounterPartyType
{
  public int Id { get; set; }

  public string Name { get; set; }
}