using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

public partial class Account : DomainEntity, IAccountDto
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
}