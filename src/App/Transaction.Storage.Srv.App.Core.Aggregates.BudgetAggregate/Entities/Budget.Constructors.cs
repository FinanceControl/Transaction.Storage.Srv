using Ardalis.Result;
using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Models;
using Transaction.Storage.Srv.Shared.Database.Models;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Entity;

public partial class Budget : DomainEntity, IBudget
{
    public class Factory : IEntityFactory<BudgetAddEvent, Budget>
    {
        public async Task<Result<Budget>> BuildAsync(BudgetAddEvent source, CancellationToken cancellationToken = default)
        {
            var new_Budget = new Budget(source);
            return Result.Success(new_Budget);
        }

    }
    
    protected Budget()
    {
    }

    public Budget(BudgetAddEvent addEventDto) : base()
    {
        Name = addEventDto.Name;
    }
}