using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.AccountEvents;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Microsoft.Extensions.Logging;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers.AccountHandlers;

public class AccountAddEventHandler : EntityAddEventHandler<AccountAddEvent, Account, AccountDto>
{

  public AccountAddEventHandler(
      IRepositoryBase<Account> AccountRep, 
      IEntityFactory<AccountAddEvent, Account> entityFactory,
      ILogger<AccountAddEventHandler> logger) 
      : base(AccountRep, entityFactory,logger)
  {
    
  }
}