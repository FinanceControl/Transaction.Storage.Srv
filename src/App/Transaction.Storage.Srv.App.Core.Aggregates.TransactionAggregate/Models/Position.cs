using System.ComponentModel.DataAnnotations;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public partial class Position : DomainEntity, IPositionDto
{
  public int? AccountId { get; private set; }
  public Account? Account { get; private set; }

  public int Amount { get; set; }

  [Required]
  public int AssetId { get; private set; }
  public Asset Asset { get; private set; }

  [Required]
  public int HeaderId { get; private set; }
  public Header Header { get; private set; }
}