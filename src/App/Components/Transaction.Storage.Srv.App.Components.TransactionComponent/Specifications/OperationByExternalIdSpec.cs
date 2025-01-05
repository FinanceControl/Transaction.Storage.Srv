
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;

namespace Transaction.Storage.Srv.App.Components.OperationComponent.Specifications;
public class OperationByExternalIdSpec : Specification<Operation>
{
  public OperationByExternalIdSpec(string externalId, int accountId)
  {
    Query.Where(c => c.ExternalId == externalId && c.AccountId == accountId);
  }
}