using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Position : OldDomainEntity, IPositionDto
{
  public int? AccountId { get; private set; }
  public Account? Account { get; private set; }

  [Column(TypeName = "decimal(30, 15)")]
  public decimal Amount { get; set; }

  [Required]
  public int AssetId { get; private set; }
  public Asset Asset { get; private set; }

  [Required]
  public int HeaderId { get; private set; }
  public Header Header { get; private set; }
}