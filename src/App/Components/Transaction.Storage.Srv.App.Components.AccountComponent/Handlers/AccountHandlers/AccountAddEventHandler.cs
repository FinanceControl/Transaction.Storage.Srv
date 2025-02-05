using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Handlers.AccountHandlers;

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