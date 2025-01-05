using Transaction.Storage.Srv.App.Components.BudgetComponent.Models;

namespace Transaction.Storage.Srv.App.Components.BudgetComponent.Dto;
public class NewBudgetDto : IBudgetBody
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