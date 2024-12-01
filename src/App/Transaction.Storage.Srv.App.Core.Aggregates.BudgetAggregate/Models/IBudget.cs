using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Models;
public interface IBudget:IDomainModel, INewBudget{

}   
public interface INewBudget {
    public string Name {get;}
}