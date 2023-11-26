using Ardalis.Result;
using MediatR;
namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;

public class AccountCheckDependencyEvent : IRequest<Result>
{
  public int Id { get; set; }
}