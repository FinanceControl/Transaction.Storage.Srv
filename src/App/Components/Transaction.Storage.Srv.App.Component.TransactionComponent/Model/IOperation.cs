using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

public interface IOperation: IDomainModel,IOperationBody{

}
public interface IOperationBody{
    public string ExternalId {get;}
    public DateTime PlanDatetime {get;}
    public DateTime CommitDateTime{get;}
    public string Description {get;}
    public int AccountId {get;}
    public int BudgetId {get;}
    public int CategoryId {get;}
    public int AssetId {get;}	
    public decimal Amount{get;}
    public string Source{get;}
    public string Notes {get;}

}