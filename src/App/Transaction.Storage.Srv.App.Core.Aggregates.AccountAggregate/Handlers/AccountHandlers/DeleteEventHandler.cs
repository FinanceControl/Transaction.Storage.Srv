using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers.AccountHandlers;

public class DeleteEventHandler : EntityDeleteEventHandler<AccountDeleteEvent, Account, AccountDto>
{
  private readonly IMediator mediator;

  public DeleteEventHandler(IRepositoryBase<Account> AccountRep, IMediator mediator) : base(AccountRep)
  {
    this.mediator = mediator;
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