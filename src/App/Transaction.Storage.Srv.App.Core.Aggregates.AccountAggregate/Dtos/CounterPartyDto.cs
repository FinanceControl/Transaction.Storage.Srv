using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
public class CounterPartyDto : ICounterPartyDto
{
  public int Id { get; set; }
  public string Name { get; set; }

  public int CounterPartyTypeId { get; set; }

  public DateTimeOffset CreatedDateTime { get; set; }

  public DateTimeOffset UpdatedDateTime { get; set; }
}