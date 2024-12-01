using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Shared.Events.Handlers;
public class OldEntityDeleteEventHandler<TEvent, TEntity, TResult> : IRequestHandler<TEvent, Result<TResult>> where TEntity : OldDomainEntity where TEvent : EntityDeleteEvent<TResult>
{
  private readonly IRepositoryBase<TEntity> assetRep;

  public OldEntityDeleteEventHandler(IRepositoryBase<TEntity> repository)
  {
    assetRep = repository;
  }
  protected virtual Task<Result> CheckDependency(TEvent request, CancellationToken cancellationToken)
  {
    return Task.FromResult(Result.Success());
  }

  public async Task<Result<TResult>> Handle(TEvent request, CancellationToken cancellationToken)
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
    return Result.Success(assertType.Adapt<TResult>());
  }
}

public class EntityDeleteEventHandler<TEvent, TEntity, TResult> : IRequestHandler<TEvent, Result<TResult>> where TEntity : DomainEntity where TEvent : EntityDeleteEvent<TResult>
{
  private readonly IRepositoryBase<TEntity> assetRep;

  public EntityDeleteEventHandler(IRepositoryBase<TEntity> repository)
  {
    assetRep = repository;
  }
  protected virtual Task<Result> CheckDependency(TEvent request, CancellationToken cancellationToken)
  {
    return Task.FromResult(Result.Success());
  }

  public async Task<Result<TResult>> Handle(TEvent request, CancellationToken cancellationToken)
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
    return Result.Success(assertType.Adapt<TResult>());
  }
}