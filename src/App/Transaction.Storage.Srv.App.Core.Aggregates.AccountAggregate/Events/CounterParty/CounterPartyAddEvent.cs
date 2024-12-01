using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.CounterPartyEvents;


public class CounterPartyAddEvent : IRequest<Result<CounterPartyDto>>, ICounterPartyBody 
{
  public string Name { get; set; }
  public int CounterPartyTypeId { get; set; }
}