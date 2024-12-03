using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Dto;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Models;

namespace Transaction.Storage.Srv.App.Components.BudgetComponent.Events;
public class BudgetAddEvent : IRequest<Result<BudgetDto>>, INewBudget
{
    public string Name {get;set;}
}