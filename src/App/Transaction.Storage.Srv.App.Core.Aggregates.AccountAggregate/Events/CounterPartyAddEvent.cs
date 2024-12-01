using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model.CounterParty;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;


public class CounterPartyAddEvent : IRequest<Result<CounterPartyDto>>,IBody 
{
  public string Name { get; set; }
  public int CounterPartyTypeId { get; set; }
}