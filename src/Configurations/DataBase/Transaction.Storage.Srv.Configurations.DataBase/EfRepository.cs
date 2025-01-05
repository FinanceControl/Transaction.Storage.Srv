using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Configurations.DataBase;

/// <summary>
/// Realisation of Aldaris.RepositoryBase
/// </summary>
/// <typeparam name="T">EntityBase</typeparam>
public class EfRepository<T> : RepositoryBase<T>, IRepositoryBase<T>, IReadRepositoryBase<T> where T : EntityBase
{
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
  }
}