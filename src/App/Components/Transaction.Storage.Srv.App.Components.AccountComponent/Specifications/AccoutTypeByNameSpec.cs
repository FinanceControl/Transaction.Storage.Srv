using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.Shared.Database.Specification;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Specifications;
public class CounterPartyByNameSpec :Specification<CounterParty>
{
    public class Factory : UIXSpecificationFactory<CounterParty,ICounterPartyBody>
    {
        public Factory() : base([nameof(Account.Name)], true)
        {
        }

        public override Specification<CounterParty> Build(ICounterPartyBody entity)
        {
            return new CounterPartyByNameSpec(entity);
        }
    }

    public CounterPartyByNameSpec(string name)
    {
        Query.Where(e => e.Name == name).Take(1);
    }

    public CounterPartyByNameSpec(ICounterPartyBody dto) : this(dto.Name)
    {
    }
}