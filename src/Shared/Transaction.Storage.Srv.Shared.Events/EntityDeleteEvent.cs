using Ardalis.Result;
using MediatR;

namespace Transaction.Storage.Srv.Shared.Events;

public class EntityDeleteEvent<TResult> : IRequest<Result<TResult>>
{
  public int Id { get; set; }
  public bool IsForced { get; set; }
}