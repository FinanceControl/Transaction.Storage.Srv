using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;


public class CounterPartyAddEvent : IRequest<Result<CounterPartyDto>>
{
  public string Name { get; private set; }
  public int CounterPartyTypeId { get; private set; }
}