using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Specifications;
using Transaction.Storage.Srv.Shared.Events.Handlers;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;

public class AccountDeleteEventHandler : EntityDeleteEventHandler<AccountDeleteEvent, Account>
{
  public AccountDeleteEventHandler(IRepositoryBase<Account> AccountRep) : base(AccountRep)
  {
  }
  //protected override async Task<Result> CheckDependency(AccountDeleteEvent request,
  //                                                     CancellationToken cancellationToken)
  //{
  //  var assets_exist = await _accountRep.AnyAsync(new AccountsOfAccountSpec(request.Id), cancellationToken);
  //  if (assets_exist)
  //    return Result.Conflict("Account for this Account exist");
  //  return Result.Success();
  //}
}