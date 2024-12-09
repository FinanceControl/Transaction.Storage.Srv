using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Models;

/// <summary>
/// Operation Model
/// </summary>
public interface IOperation: IDomainModel,IOperationUpdate{

}

/// <summary>
/// Operation Update Model
/// </summary>
public interface IOperationUpdate: IDomainModelId, IOperationBody{

}

/// <summary>
/// Operation Content 
/// </summary>
public interface IOperationBody:IExternalIdData{
    public DateTime PlanDatetime {get;}
    public DateTime CommitDateTime{get;}
    public string Description {get;}
    public int BudgetId {get;}
    public int CategoryId {get;}
    public int AssetId {get;}	
    public decimal Amount{get;}
    public string Source{get;}
    public string Notes {get;}
}

/// <summary>
/// Id of external ID
/// </summary>
public interface IExternalIdData{
    public string ExternalId {get;}
    public int AccountId {get;}
}
