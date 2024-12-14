using Transaction.Storage.Srv.App.Components.TransactionComponent.Models;
using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Dtos;

public class OperationDto : NewOperationDto, IOperation
{
    public Guid Guid {get;set;}

    public DateTimeOffset CreatedDateTime {get;set;}

    public DateTimeOffset UpdatedDateTime {get;set;}

    public int Id {get;set;}
}
public class NewOperationDto:IOperationBody
{

    public string ExternalId {get;set;} 
    public DateTime PlanDatetime {get;set;}

    public DateTime CommitDateTime {get;set;}

    public string Description {get;set;} 

    public int AccountId {get;set;}

    public int BudgetId {get;set;}

    public int CategoryId {get;set;}

    public int AssetId {get;set;}

    public decimal Amount {get;set;}

    public string Source {get;set;}

    public string Notes {get;set;}
}

public class UpdateOperationDto : IDomainModelId, IOperationBody
{
    public Guid Guid {get;set;}

    public int Id {get;set;}

    public DateTime PlanDatetime {get;set;}

    public DateTime CommitDateTime {get;set;}

    public string Description {get;set;}

    public int BudgetId {get;set;}

    public int CategoryId {get;set;}

    public int AssetId {get;set;}

    public decimal Amount {get;set;}

    public string Source {get;set;}

    public string Notes {get;set;}

    public string ExternalId {get;set;}

    public int AccountId {get;set;}
}