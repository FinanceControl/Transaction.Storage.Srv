using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Shared.Events.Handlers;
public class EntityWithRelationDeleteEventHandler<TEvent, TEntity> : IRequestHandler<TEvent, Result> where TEntity : DomainEntity where TEvent : EntityWithRelationDeleteEvent
{
  private readonly IRepositoryBase<TEntity> assetRep;

  public EntityWithRelationDeleteEventHandler(IRepositoryBase<TEntity> repository)
  {
    assetRep = repository;
  }
  protected virtual Task<Result> CheckDependency(TEvent request, CancellationToken cancellationToken)
  {
    return Task.FromResult(Result.Success());
  }

  public async Task<Result> Handle(TEvent request, CancellationToken cancellationToken)
  {
    var assertType = await assetRep.GetByIdAsync(request.Id, cancellationToken);
    if (assertType is null)
      return Result.NotFound();

    if (request.IsForced == false)
    {
      var checkDependencyResult = await CheckDependency(request, cancellationToken);
      if (!checkDependencyResult.IsSuccess)
        return checkDependencyResult;
    }

    await assetRep.DeleteAsync(assertType, cancellationToken);
    return Result.Success();
  }
}