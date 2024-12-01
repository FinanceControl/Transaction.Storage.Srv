using Ardalis.Result;
using MediatR;
namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.AccountEvents;

public class CheckDependencyEvent : IRequest<Result>
{
  public int Id { get; set; }
}