using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Entity;

public partial class Account : DomainEntity, IAccount
{
  private const int NameMaxLenght = 50;

  [MaxLength(NameMaxLenght)]
  public string Name { get; private set; }

  private const int DescriptionMaxLenght = 255;
  [MaxLength(DescriptionMaxLenght)]
  public string Description { get; private set; }

  [Required]
  public int CounterPartyId { get; private set; }
  public CounterParty CounterParty { get; private set; }

  [Display(Name = "Are account controlled automatically")]
  public bool IsUnderManagement { get; private set; }

  public DateOnly CloseDate { get; private set; }
  
  public DateOnly LastSyncDate { get; private set; }

  public string KeepassId { get; private set; }
}