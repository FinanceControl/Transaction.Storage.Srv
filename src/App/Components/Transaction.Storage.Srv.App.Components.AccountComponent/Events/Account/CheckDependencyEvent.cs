using Ardalis.Result;
using MediatR;
namespace Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;

public class CheckDependencyEvent : IRequest<Result>
{
  public int Id { get; set; }
}