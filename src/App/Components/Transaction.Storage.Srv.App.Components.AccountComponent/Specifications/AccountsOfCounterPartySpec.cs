using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Specifications;
public class AccountsOfCounterPartySpec : Specification<Account>
{
  public AccountsOfCounterPartySpec(int counterPartyId)
  {
    Query.Where(c => c.CounterPartyId == counterPartyId);
  }
}