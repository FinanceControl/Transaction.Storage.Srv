using Ardalis.Result;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Handlers.AccountHandlers;

public class AccountDeleteEventHandler : EntityDeleteEventHandler<AccountDeleteEvent, Account, AccountDto>
{
  //private readonly IMediator mediator;

  public AccountDeleteEventHandler(IRepositoryBase<Account> AccountRep) : base(AccountRep)
  {
    //this.mediator = mediator;
  }
  protected override async Task<Result> CheckDependency(AccountDeleteEvent request,
                                                        CancellationToken cancellationToken)
  {
    //var checkEv = new AccountCheckDependencyEvent() { Id = request.Id };

    //var result = await mediator.Send(checkEv, cancellationToken);
    //if (!result.IsSuccess)
    // if (result.Status == ResultStatus.Conflict)
    //    return Result.Conflict(new[] { "Account has dependency" }.Concat(result.Errors).ToArray());
    //  else
    //    return Result.CriticalError("Unexpected error");

    return await base.CheckDependency(request, cancellationToken);
  }
}