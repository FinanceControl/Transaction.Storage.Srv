using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;

public class TransactionUploadEvent : IRequest<Result<Dictionary<int, int>>>
{
  public required List<TransactionAddEvent> records { get; set; }
}