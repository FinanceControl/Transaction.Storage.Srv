using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Events;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Models;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Specifications;
using Transaction.Storage.Srv.Shared.Database.Models;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;

namespace Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;

public partial class Budget : DomainEntity, IBudget
{
    public class Factory : IEntityFactory<BudgetAddEvent, Budget>
    {
        private readonly IReadRepositoryBase<Budget> entityRep;

        public Factory (IReadRepositoryBase<Budget> entityRep){
            this.entityRep = entityRep;
        }
        public async Task<Result<Budget>> BuildAsync(BudgetAddEvent source, CancellationToken cancellationToken = default)
        {                 
            var source_result = await new UIXValidator<Budget,IBudgetBody>(entityRep,[new BudgetByNameSpec.Factory()]).ValidateAsync(source);
            
            if (!source_result.IsValid)
                return Result.Conflict(source_result.Errors.Select(e=>e.ErrorMessage).ToArray());

            var new_Budget = new Budget(source);
            
            return Result.Success(new_Budget);
        }

    }
    
    protected Budget()
    {
    }

    public Budget(BudgetAddEvent addEventDto) : base()
    {
        addEventDto.Adapt(this);
    }
}