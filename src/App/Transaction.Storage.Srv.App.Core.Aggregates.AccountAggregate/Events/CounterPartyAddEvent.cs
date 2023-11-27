using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;


public class CounterPartyAddEvent : IRequest<Result<CounterPartyDto>>,ICounterPartyBodyDto 
{
  public string Name { get; set; }
  public int CounterPartyTypeId { get; set; }
}