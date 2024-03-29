using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

[Index(nameof(CommitDateTime), IsUnique = false)]
public partial class Header : DomainEntity, IHeaderDto
{
  private const int DescriptionMaxLenght = 255;
  [MaxLength(DescriptionMaxLenght)]
  public string Description { get; private set; }

  public DateTimeOffset? CommitDateTime { get; private set; }

  public ICollection<Position> Positions { get; private set; }
}