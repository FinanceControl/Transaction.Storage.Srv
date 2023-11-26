using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Shared.Events.Handlers;
public class EntityAddEventHandler<TEvent, TEntity, TResult> : IRequestHandler<TEvent, Result<TResult>> where TEvent : IRequest<Result<TResult>> where TEntity : DomainEntity
{
  private readonly IRepositoryBase<TEntity> _repository;
  private readonly IEntityFactory<TEvent, TEntity> _entityFactory;

  public EntityAddEventHandler(IRepositoryBase<TEntity> entityRep, IEntityFactory<TEvent, TEntity> entityFactory)
  {
    this._repository = entityRep;
    this._entityFactory = entityFactory;
  }
  protected virtual Task<Result> CheckDependency(TEvent request, CancellationToken cancellationToken)
  {
    return Task.FromResult(Result.Success());
  }
  public async Task<Result<TResult>> Handle(TEvent request, CancellationToken cancellationToken)
  {
    var check_dependency_result = await CheckDependency(request, cancellationToken);
    if (!check_dependency_result.IsSuccess)
    {
      return check_dependency_result;
    }

    var build_result = _entityFactory.Build(request);
    if (!build_result.IsSuccess)
      return build_result.Map<TEntity, TResult>((at) => throw new ApplicationException("Unexpected result mapping"));

    var new_Asset = await _repository.AddAsync(build_result.Value, cancellationToken);
    return Result.Success(new_Asset.Adapt<TResult>());
  }
}