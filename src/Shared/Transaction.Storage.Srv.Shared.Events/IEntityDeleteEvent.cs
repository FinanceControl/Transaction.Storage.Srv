using Ardalis.Result;
using MediatR;

namespace Transaction.Storage.Srv.Shared.Events;

public class EntityDeleteEvent : IRequest<Result>
{
  public int Id { get; set; }
}