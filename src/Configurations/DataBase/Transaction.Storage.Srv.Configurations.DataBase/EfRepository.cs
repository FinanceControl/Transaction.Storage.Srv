using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Transcation.Storage.Srv.Shared.Database;

namespace Transaction.Storage.Srv.Configurations.DataBase;

public class EfRepository<T> : RepositoryBase<T>, IRepositoryBase<T>, IReadRepositoryBase<T> where T : EntityBase
{
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
  }
}