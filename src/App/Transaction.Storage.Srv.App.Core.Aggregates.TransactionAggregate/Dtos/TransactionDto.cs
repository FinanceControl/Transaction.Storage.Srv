namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

public class TransactionDto
{
  public HeaderDto Header { get; set; }
  public List<PositionDto> Positions { get; set; }
}
