using Ardalis.Result;
using MediatR;
namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

public class AssetCheckDependencyEvent : IRequest<Result>
{
  public int Id { get; set; }
}