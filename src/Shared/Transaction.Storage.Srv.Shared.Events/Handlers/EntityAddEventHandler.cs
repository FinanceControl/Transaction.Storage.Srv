using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Shared.Database.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Transaction.Storage.Srv.Shared.Events.Handlers;
public class OldEntityAddEventHandler<TEvent, TEntity, TResult> :
  IRequestHandler<TEvent, Result<TResult>>
  where TEvent : IRequest<Result<TResult>>
  where TEntity : OldDomainEntity
{
  private readonly IRepositoryBase<TEntity> _repository;
  private readonly IOldEntityFactory<TEvent, TEntity> _entityFactory;
  private readonly ISpecification<TEntity, TResult>? specification;

  public OldEntityAddEventHandler(IRepositoryBase<TEntity> entityRep, IOldEntityFactory<TEvent, TEntity> entityFactory)
  {
    _repository = entityRep;
    _entityFactory = entityFactory;
    specification = specification;
  }
  //protected virtual Task<Result> CheckDependency(TEvent request, CancellationToken cancellationToken)
  //{
  //  return Task.FromResult(Result.Success());
  //}
  public async Task<Result<TResult>> Handle(TEvent request, CancellationToken cancellationToken)
  {
    //var check_dependency_result = await CheckDependency(request, cancellationToken);
    //if (!check_dependency_result.IsSuccess)
    //{
    //  return check_dependency_result;
    //}

    var build_result = await _entityFactory.BuildAsync(request, cancellationToken);
    if (!build_result.IsSuccess)
      return build_result.Map<TEntity, TResult>((at) => throw new ApplicationException("Unexpected result mapping for ok result"));

    var new_Asset = await _repository.AddAsync(build_result.Value, cancellationToken);

    return Result.Success(await ToResult(new_Asset, cancellationToken));
  }

  protected virtual Task<TResult> ToResult(TEntity entity, CancellationToken cancellationToken)
  {
    return Task.FromResult(entity.Adapt<TResult>());
  }

}

public class EntityAddEventHandler<TEvent, TEntity, TResult> :
  IRequestHandler<TEvent, Result<TResult>>
  where TEvent : IRequest<Result<TResult>>
  where TEntity : ConstantDomainEntity
{
  private readonly ILogger<EntityAddEventHandler<TEvent, TEntity, TResult>> _logger;
  private readonly IRepositoryBase<TEntity> _repository;
  private readonly IEntityFactory<TEvent, TEntity> _entityFactory;

  public EntityAddEventHandler(IRepositoryBase<TEntity> entityRep, 
                               IEntityFactory<TEvent, TEntity> entityFactory, 
                               ILogger<EntityAddEventHandler<TEvent, TEntity, TResult>> logger)
  {
    _logger = logger;
    _repository = entityRep;
    _entityFactory = entityFactory;
  }

  public async Task<Result<TResult>> Handle(TEvent request, CancellationToken cancellationToken)
  {
    _logger.LogInformation("Create and validate {TEventType}: {TEvent}", typeof(TEvent).Name, request);
    var build_result = await _entityFactory.BuildAsync(request, cancellationToken);

    if (!build_result.IsSuccess)
      return build_result.Map<TEntity, TResult>((at) => throw new ApplicationException("Unexpected result mapping for ok result"));

    _logger.LogInformation("Add entity {TEntityType}: {TEntity}", typeof(TEntity).Name, build_result.Value.ToString());
    
    TEntity new_Entity;
    try
    {
      new_Entity = await _repository.AddAsync(build_result.Value, cancellationToken);
    }
    catch (DbUpdateException ex)
    {
      _logger.LogWarning(ex,"Db raise exception on update.");
      return Result<TResult>.Conflict("Unexpected conflict when add entity to DataBase");
    }
    catch (Exception ex){
      _logger.LogError(ex,"Db raise exception on update.");
      return Result<TResult>.CriticalError("Cann't add entity to Database");
    }

    var result = await ToResult(new_Entity, cancellationToken);
    _logger.LogInformation("Add return resut {TResultType}: {TResut}", typeof(TResult).Name, result);
    return Result.Success(result);
  }

  protected virtual Task<TResult> ToResult(TEntity entity, CancellationToken cancellationToken)
  {
    return Task.FromResult(entity.Adapt<TResult>());
  }

}