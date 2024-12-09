using Ardalis.Result;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Shared.Events.Interfaces;

public interface IOldEntityFactory<TSource, TEntity> where TEntity : OldDomainEntity
{
  Task<Result<TEntity>> BuildAsync(TSource source, CancellationToken cancellationToken = default);
}
public interface IEntityFactory<TSource, TEntity> where TEntity : ConstantDomainEntity
{
  Task<Result<TEntity>> BuildAsync(TSource source, CancellationToken cancellationToken = default);
}
