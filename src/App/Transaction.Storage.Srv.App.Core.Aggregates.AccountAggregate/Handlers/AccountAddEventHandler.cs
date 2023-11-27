using Ardalis.Result;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;

public class AccountAddEventHandler : EntityAddEventHandler<AccountAddEvent, Account, AccountDto>
{

  public AccountAddEventHandler(IRepositoryBase<Account> AccountRep, IEntityFactory<AccountAddEvent, Account> entityFactory) : base(AccountRep, entityFactory)
  {
    
  }
}