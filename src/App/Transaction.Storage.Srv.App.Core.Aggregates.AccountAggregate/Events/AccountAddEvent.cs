using Ardalis.Result;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;


public class AccountAddEvent : IRequest<Result<AccountDto>>, IAccountBodyDto
{
  public string Name { get; set; }
  public string Description { get; set; }
  public int CounterPartyId { get; set; }
  public bool IsUnderManagement { get; set; }
}