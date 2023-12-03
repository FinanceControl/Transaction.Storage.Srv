using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

public class PositionDto : NewPositionDto, IPositionDto
{
  public int Id { get; set; }

  public DateTimeOffset CreatedDateTime { get; set; }

  public DateTimeOffset UpdatedDateTime { get; set; }

  public double AmountDouble { get; set; }
}
