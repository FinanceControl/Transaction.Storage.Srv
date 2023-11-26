using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Specifications;
public class PositionsByAccountIdSpec : SingleResultSpecification<Position>
{
  public PositionsByAccountIdSpec(int accountId)
  {
    Query.Where(e => e.AccountId == accountId);
  }
}