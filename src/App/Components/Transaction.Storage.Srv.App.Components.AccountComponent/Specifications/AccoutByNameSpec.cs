using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.Shared.Database.Specification;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Specifications;
public class AccountByNameSpec :Specification<Account>
{
    public class Factory : UIXSpecificationFactory<Account,IAccountBody>
    {
        public Factory() : base([nameof(Account.Name)], true)
        {
        }

        public override Specification<Account> Build(IAccountBody entity)
        {
            return new AccountByNameSpec(entity);
        }
    }

    public AccountByNameSpec(string name)
    {
        Query.Where(e => e.Name == name).Take(1);
    }

    public AccountByNameSpec(IAccountBody dto) : this(dto.Name)
    {
    }
}