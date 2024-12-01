using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;

public class AccountAddEventHandler : EntityAddEventHandler<AccountAddEvent, Account, AccountDto>
{

  public AccountAddEventHandler(IRepositoryBase<Account> AccountRep, IEntityFactory<AccountAddEvent, Account> entityFactory) : base(AccountRep, entityFactory)
  {
    
  }
}