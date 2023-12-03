namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

public class TransactionDto
{
  public required HeaderDto Header { get; set; }
  public required List<PositionDto> Positions { get; set; }
}
