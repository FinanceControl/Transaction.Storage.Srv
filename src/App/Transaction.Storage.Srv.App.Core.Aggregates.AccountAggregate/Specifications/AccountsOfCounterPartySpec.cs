using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Specifications;
public class AccountsOfCounterPartySpec : Specification<Account>
{
  public AccountsOfCounterPartySpec(int counterPartyId)
  {
    Query.Where(c => c.CounterPartyId == counterPartyId);
  }
}