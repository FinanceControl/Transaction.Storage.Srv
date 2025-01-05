using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Entity;

public partial class CounterParty : DomainEntity, ICounterParty
{
  private const int NameMaxLenght = 50;

  [MaxLength(NameMaxLenght)]
  public string Name { get; set; }

  [Required]
  public int CounterPartyTypeId { get; private set; }
  public CounterPartyType CounterPartyType { get; private set; }

  public ICollection<Account> Accounts { get; }
}