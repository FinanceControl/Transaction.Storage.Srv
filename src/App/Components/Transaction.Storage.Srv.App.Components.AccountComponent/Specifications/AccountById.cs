using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Specifications;
public class AccountsByIdSpec : Specification<Account>
{
  public AccountsByIdSpec(int accountId)
  {
    Query.Where(a=>a.Id == accountId);
  }
}