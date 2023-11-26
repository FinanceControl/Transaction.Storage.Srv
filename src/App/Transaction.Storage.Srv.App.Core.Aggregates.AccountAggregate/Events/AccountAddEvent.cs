using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;


public class AccountAddEvent : IRequest<Result<AccountDto>>
{
  public string Name { get; set; }
  public string Description { get; set; }
  public int CounterPartyId { get; set; }
  public bool IsUnderManagement { get; set; }
}