using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Components.BudgetComponent.Models;
public interface IBudget:IDomainModel, IBudgetBody{

}   
public interface IBudgetBody {
    public string Name {get;}
}