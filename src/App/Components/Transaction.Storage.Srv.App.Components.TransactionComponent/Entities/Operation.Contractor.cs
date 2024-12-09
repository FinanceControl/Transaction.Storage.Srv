using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Events;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Models;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;

public partial class Operation
{

    public class Factory : IEntityFactory<OperationAddEvent, Operation>
    {
        private readonly IReadRepositoryBase<Account> accountRep;
        private readonly IReadRepositoryBase<Asset> assetRep;
        private readonly IReadRepositoryBase<Category> categoryRep;
        private readonly IReadRepositoryBase<Budget> budgetRep;
        private readonly IReadRepositoryBase<Operation> operationRep;

        public Factory(IReadRepositoryBase<Account> accountRep,
                         IReadRepositoryBase<Asset> assetRep,
                         IReadRepositoryBase<Category> categoryRep,
                         IReadRepositoryBase<Budget> budgetRep,
                         IReadRepositoryBase<Operation> operationRep)
        {
            this.accountRep = accountRep;
            this.assetRep = assetRep;
            this.categoryRep = categoryRep;
            this.budgetRep = budgetRep;
            this.operationRep = operationRep;
        }
        public async Task<Result<Operation>> BuildAsync(OperationAddEvent source, CancellationToken cancellationToken = default)
        {
            var source_result = await new Validator(accountRep,assetRep,categoryRep,budgetRep,operationRep)
                                    .ValidateAsync(source, cancellationToken);
            if (!source_result.IsValid)
                return Result.Invalid(source_result.AsErrors());

            var new_entity = new Operation(source);
            return Result.Success(new_entity);
        }

   
    }

    protected Operation()
    {
    }

    protected Operation(OperationAddEvent addEventDto) : base()
    {
        addEventDto.Adapt(this);
    }
}