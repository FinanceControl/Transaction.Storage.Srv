using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;

public class TransactionAddEvent : IRequest<Result<TransactionDto>>
{
  public NewHeaderDto Header { get; set; }
  public List<NewPositionDto> Positions { get; set; }
}