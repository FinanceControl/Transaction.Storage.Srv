using Ardalis.Result;
using MediatR;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;

public class TransactionUploadEvent : IRequest<Result<Dictionary<int, int>>>
{
  public required List<TransactionAddEvent> records { get; set; }
}