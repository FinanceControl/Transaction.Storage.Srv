using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Shared.Events.Handlers;
public class EntityDeleteEventHandler<TEvent, TEntity> : IRequestHandler<TEvent, Result> where TEntity : DomainEntity where TEvent : EntityDeleteEvent
{
  private readonly IRepositoryBase<TEntity> assetRep;

  public EntityDeleteEventHandler(IRepositoryBase<TEntity> repository)
  {
    assetRep = repository;
  }

  public async Task<Result> Handle(TEvent request, CancellationToken cancellationToken)
  {
    var assertType = await assetRep.GetByIdAsync(request.Id, cancellationToken);
    if (assertType is null)
      return Result.NotFound();

    await assetRep.DeleteAsync(assertType, cancellationToken);
    return Result.Success();
  }
}