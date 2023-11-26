using Ardalis.Result;
using Transcation.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Shared.Events.Interfaces;

public interface IEntityFactory<TSource, TEntity> where TEntity : DomainEntity
{
  Task<Result<TEntity>> BuildAsync(TSource source, CancellationToken cancellationToken = default);
}