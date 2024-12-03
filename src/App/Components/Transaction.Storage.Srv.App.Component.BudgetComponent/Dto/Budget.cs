using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Dto;
public class NewBudgetDto : INewBudget
{
    public string Name {get;set;}
}
public class BudgetDto : NewBudgetDto, IBudget
{
    public Guid Guid {get;set;}

    public DateTimeOffset CreatedDateTime {get;set;}

    public DateTimeOffset UpdatedDateTime {get;set;}

    public int Id {get;set;}
}