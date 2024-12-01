using Ardalis.Result;
using MediatR;
using Transaction.Storage.App.Core.Aggregates.BudgetAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Models;

namespace Transaction.Storage.App.Core.Aggregates.BudgetAggregate.Events;
public class BudgetAddEvent : IRequest<Result<BudgetDto>>, INewBudget
{
    public string Name {get;set;}
}