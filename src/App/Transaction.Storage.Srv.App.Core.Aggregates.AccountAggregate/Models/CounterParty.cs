using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class CounterParty : DomainEntity, ICounterPartyDto
{
  private const int NameMaxLenght = 50;

  [MaxLength(NameMaxLenght)]
  public string Name { get; set; }

  [Required]
  public int CounterPartyTypeId { get; private set; }
  public CounterPartyType CounterPartyType { get; private set; }

  public ICollection<Account> Accounts { get; }
}