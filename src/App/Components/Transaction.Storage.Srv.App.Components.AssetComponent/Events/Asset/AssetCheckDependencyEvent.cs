using Ardalis.Result;
using MediatR;
namespace Transaction.Storage.Srv.App.Components.AssetComponent.Events;

public class AssetCheckDependencyEvent : IRequest<Result>
{
  public int Id { get; set; }
}