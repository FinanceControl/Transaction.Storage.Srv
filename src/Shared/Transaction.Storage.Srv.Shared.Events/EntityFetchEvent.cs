using Ardalis.Result;
using MediatR;

namespace Transaction.Storage.Srv.Shared.Events;

public class EntityFetchEvent<TResult> : IRequest<Result<IEnumerable<TResult>>>
{
}