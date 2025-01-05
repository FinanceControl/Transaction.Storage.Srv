using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Events.CounterPartyEvents;


public class CounterPartyAddEvent : IRequest<Result<CounterPartyDto>>, ICounterPartyBody 
{
  public string Name { get; set; }
  public int CounterPartyTypeId { get; set; }
}