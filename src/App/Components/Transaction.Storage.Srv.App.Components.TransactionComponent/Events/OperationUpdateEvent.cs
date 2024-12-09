using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Dtos;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Models;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Events;

public class OperationUpdateEvent : IOperationUpdate, IRequest<Result<OperationDto>>
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