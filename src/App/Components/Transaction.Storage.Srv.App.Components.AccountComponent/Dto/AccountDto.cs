using Transaction.Storage.Srv.App.Components.AccountComponent.Model;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
public class AccountDto : IAccount
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }

  public int CounterPartyId { get; set; }

  public bool IsUnderManagement { get; set; }

  public DateTimeOffset UpdatedDateTime { get; set; }

  public DateTimeOffset CreatedDateTime { get; set; }

  public Guid Guid { get; set; }

  public DateOnly CloseDate { get; set; }

  public DateOnly LastSyncDate { get; set; }

  public string KeepassId { get; set; }
}