using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
public class AccountDto : IAccountDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }

  public int CounterPartyId { get; set; }

  public bool IsUnderManagement { get; set; }

  public DateTimeOffset UpdatedDateTime { get; set; }

  public DateTimeOffset CreatedDateTime { get; set; }
}