using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

public class NewHeaderDto : INewHeaderDto
{
  public string Description { get; set; }

  public DateTimeOffset? CommitDateTime { get; set; }
}