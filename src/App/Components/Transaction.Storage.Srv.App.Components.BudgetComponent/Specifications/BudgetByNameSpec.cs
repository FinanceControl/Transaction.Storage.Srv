using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Models;
using Transaction.Storage.Srv.Shared.Database.Specification;

namespace Transaction.Storage.Srv.App.Components.BudgetComponent.Specifications;
public class BudgetByNameSpec :Specification<Budget>
{
    public class Factory : UIXSpecificationFactory<Budget,IBudgetBody>
    {
        public Factory() : base([nameof(Budget.Name)], true)
        {
        }

        public override Specification<Budget> Build(IBudgetBody entity)
        {
            return new BudgetByNameSpec(entity);
        }
    }

    public BudgetByNameSpec(string name)
    {
        Query.Where(e => e.Name == name).Take(1);
    }

    public BudgetByNameSpec(IBudgetBody budget) : this(budget.Name)
    {
    }
}