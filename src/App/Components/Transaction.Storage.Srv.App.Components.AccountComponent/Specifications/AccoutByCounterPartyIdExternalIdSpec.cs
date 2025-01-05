using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.Shared.Database.Specification;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Specifications;
public class AccountByCounterPartyIdExternalIdSpec :Specification<Account>
{
    public class Factory : UIXSpecificationFactory<Account,IAccountBody>
    {
        public Factory() : base([nameof(Account.Name)], true)
        {
        }

        public override Specification<Account> Build(IAccountBody entity)
        {
            return new AccountByCounterPartyIdExternalIdSpec(entity);
        }
    }

    public AccountByCounterPartyIdExternalIdSpec(int counterPartyId, string externalId)
    {
        Query.Where(e => e.CounterPartyId == counterPartyId && e.ExternalId == externalId).Take(1);
    }

    public AccountByCounterPartyIdExternalIdSpec(IAccountBody dto) : this(dto.CounterPartyId, dto.ExternalId)
    {
    }
}