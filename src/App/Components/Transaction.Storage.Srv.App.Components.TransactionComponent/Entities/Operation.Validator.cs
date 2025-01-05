using Ardalis.Specification;
using FluentValidation;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Models;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Validators;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;

public partial class Operation
{
    public class Validator : AbstractValidator<IOperationBody>
    {
        public Validator(IReadRepositoryBase<Account> accountRep,
                         IReadRepositoryBase<Asset> assetRep,
                         IReadRepositoryBase<Category> categoryRep,
                         IReadRepositoryBase<Budget> budgetRep,
                         IReadRepositoryBase<Operation> operationRep)
        {
            RuleFor(at => at.AccountId).SetValidator(new IdValidator<Account>(accountRep, nameof(AccountId)));
            RuleFor(at => at.AssetId).SetValidator(new IdValidator<Asset>(assetRep, nameof(AssetId)));
            RuleFor(at => at.CategoryId).SetValidator(new IdValidator<Category>(categoryRep, nameof(CategoryId)));
            RuleFor(at => at.BudgetId).SetValidator(new IdValidator<Budget>(budgetRep, nameof(BudgetId)));
        }
    }
}