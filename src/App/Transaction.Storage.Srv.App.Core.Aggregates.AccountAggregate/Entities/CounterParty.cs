using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model.CounterParty;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;

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