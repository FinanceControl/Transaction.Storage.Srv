using Ardalis.Result;
using MediatR;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

public class AssetTypeDeleteEvent : IRequest<Result>
{
  public int Id { get; set; }
  public bool IsForced { get; set; }
}