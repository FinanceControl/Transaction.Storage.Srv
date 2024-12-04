using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Models;

public interface IOperation: IDomainModel,IOperationBody{

}
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

public interface IExternalIdData{
    public string ExternalId {get;}
    public int AccountId {get;}
}