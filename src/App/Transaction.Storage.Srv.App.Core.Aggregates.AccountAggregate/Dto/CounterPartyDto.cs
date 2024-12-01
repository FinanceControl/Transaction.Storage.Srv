using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
public class CounterPartyDto : ICounterParty
{
  public int Id { get; set; }
  
  public Guid Guid { get; set; }

  public string Name { get; set; }

  public int CounterPartyTypeId { get; set; }

  public DateTimeOffset CreatedDateTime { get; set; }

  public DateTimeOffset UpdatedDateTime { get; set; }
}